using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace SheepReaper.GameSaves
{
    public class BufferBinaryReader : BinaryReader, IBinaryReader
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stream?.Dispose();
            }

            base.Dispose(disposing);
        }

        public BufferBinaryReader(Memory<byte> memoryBuffer) : this(new MemoryStream(memoryBuffer.GetBuffer(), 0, memoryBuffer.Length, true, true), Encoding.UTF8, false)
        {
        }

        public BufferBinaryReader(Memory<byte> memoryBuffer, Encoding encoding) : this(new MemoryStream(memoryBuffer.GetBuffer(), 0, memoryBuffer.Length, true, true), encoding, false)
        {
        }

        public BufferBinaryReader(Memory<byte> memoryBuffer, Encoding encoding, bool leaveOpen) : this(new MemoryStream(memoryBuffer.GetBuffer(), 0, memoryBuffer.Length, true, true), encoding, leaveOpen)
        {
        }

        public BufferBinaryReader(Stream stream) : this(stream, Encoding.UTF8, false)
        {
        }

        public BufferBinaryReader(Stream stream, Encoding encoding) : this(stream, encoding, false)
        {
        }

        public BufferBinaryReader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream.ToStatic(stream.CanWrite).TeeAs<MemoryStream>(out var teeStream), encoding, leaveOpen)
        {
            Stream = teeStream;
            teeStream.TryGetBuffer(out var buffer);
            Buffer = new Memory<byte>(buffer.Array);
        }

        public Memory<byte> Buffer { get; set; }

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public int PositionInt { get => (int)BaseStream.Position; set => BaseStream.Position = value; }

        public MemoryStream Stream { get; set; }

        public Span<byte> GetBufferSpan()
        {
            return Buffer.Span;
        }

        public byte[] ReadAllBytes() => ReadBytes((int)(BaseStream.Length - BaseStream.Position));

        //public override int ReadInt32()
        //{
        //    Console.WriteLine($"Call to read int: Position(base stream): {PositionInt}({BaseStream.Position}) of {BaseStream.Length}(base stream)");
        //    return base.ReadInt32();
        //}

        public Quaternion ReadQuaternion()
        {
            return new Quaternion
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle(),
                W = ReadSingle(),
            };
        }

        public Vector2 ReadVector2()
        {
            return new Vector2
            {
                X = ReadSingle(),
                Y = ReadSingle(),
            };
        }

        public Vector2I ReadVector2I()
        {
            return new Vector2I
            {
                X = ReadInt32(),
                Y = ReadInt32(),
            };
        }

        public Vector3 ReadVector3()
        {
            return new Vector3
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle(),
            };
        }

        public void SkipBytes(int length)
        {
            BaseStream.Position += length;
        }

        public byte[] ViewAllBytes() => ReadAllBytes();

        public Array ViewBytes(int length) => ReadBytes(length);
    }
}