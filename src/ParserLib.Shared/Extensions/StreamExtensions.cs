using System;
using System.IO;
using System.Reflection;

namespace SheepReaper.GameSaves.Extensions
{
    public static class StreamExtensions
    {
        private static MemoryStream CopyTo(Stream source, Stream destination, bool preservePosition)
        {
            var sourcePosition = source.Position;
            using (source)
            {
                source.CopyTo(destination);
            }
            destination.Position = preservePosition ? sourcePosition : 0;
            return destination.AsMemoryStream();
        }

        private static MemoryStream CreateExpandable(Stream stream, bool preservePosition)
        {
            var newStream = new MemoryStream();
            using (stream)
            {
                return CopyTo(stream, newStream, preservePosition);
            }
        }

        private static MemoryStream CreateStatic(Stream stream, bool preservePosition)
        {
            var newStream = new MemoryStream(new byte[stream.Length], 0, (int)stream.Length, true, true);
            using (stream)
            {
                return CopyTo(stream, newStream, preservePosition);
            }

        }

        public static MemoryStream AsMemoryStream(this Stream stream, bool preservePosition = true)
        {
            if (stream is MemoryStream asMemoryStream) return asMemoryStream;

            using (stream)
            {
                if (stream.CheckCanGetBuffer()) return CreateStatic(stream, preservePosition);

                return stream.CheckIsExpandable()
                    ? CreateExpandable(stream, preservePosition)
                    : CopyTo(stream, new MemoryStream(new byte[stream.Length], true), preservePosition);
            }
        }

        public static bool CheckCanGetBuffer(this Stream stream)
        {
            return stream is MemoryStream asMemoryStream && asMemoryStream.TryGetBuffer(out _);
        }

        public static bool CheckIsExpandable(this Stream stream)
        {
            return stream is MemoryStream asMemoryStream
                   && (bool)(typeof(MemoryStream).GetField("_expandable", BindingFlags.Instance | BindingFlags.NonPublic)
                       ?.GetValue(asMemoryStream) ?? false);
        }

        public static T TeeAs<T>(this Stream input, out T stream) where T : Stream
        {
            stream = input as T ?? throw new NotSupportedException(
                         $"The input Stream is not a {typeof(T)}. Requested Tee Cast is not possible.");
            return stream;
        }

        public static Memory<byte> GetMemory(this Stream stream)
        {
            var asMs = stream as MemoryStream ??
                       throw new NotSupportedException("This Stream does not support direct Memory access.");
            asMs.TryGetBuffer(out var segment);
            return segment;
        }

        public static MemoryStream ToExpandable(this Stream stream, bool writable = true, bool preservePosition = true)
        {
            if (stream.CheckIsExpandable() && stream.CanWrite == writable)
            {
                return stream.AsMemoryStream();
            }

            using (stream)
            {
                return CreateExpandable(stream, preservePosition);
            }
        }

        public static MemoryStream ToStatic(this Stream stream, bool writable = false, bool preservePosition = true)
        {
            if (stream.CheckCanGetBuffer() && stream.CanWrite == writable)
                return stream.AsMemoryStream();

            using(stream)
            {
                return CreateStatic(stream, preservePosition);
            }
        }

        public static MemoryStream ToWritableStatic(this Stream stream, bool preservePosition = true) => ToStatic(stream, true, preservePosition);
    }
}