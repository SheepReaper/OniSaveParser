using SheepReaper.GameSaves.Extensions;
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
                MemoryStream?.Dispose();
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

        public BufferBinaryReader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream.ToExpandable(stream.CanWrite), encoding, leaveOpen)
        {
        }

        public Memory<byte> Buffer => BaseStream.GetMemory();
        public MemoryStream MemoryStream => (MemoryStream)BaseStream;

        public long Position
        {
            get => BaseStream.Position;
            set => BaseStream.Position = value;
        }

        public int PositionInt { get => (int)BaseStream.Position; set => BaseStream.Position = value; }

        public Span<byte> GetBufferSpan()
        {
            return Buffer.Span;
        }

        public byte[] ReadAllBytes() => ReadBytes((int)(BaseStream.Length - BaseStream.Position));

        public Quaternion ReadQuaternion()
        {
            return new Quaternion
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle(),
                W = ReadSingle()
            };
        }

        public Vector2 ReadVector2()
        {
            return new Vector2
            {
                X = ReadSingle(),
                Y = ReadSingle()
            };
        }

        public Vector2I ReadVector2I()
        {
            return new Vector2I
            {
                X = ReadInt32(),
                Y = ReadInt32()
            };
        }

        public Vector3 ReadVector3()
        {
            return new Vector3
            {
                X = ReadSingle(),
                Y = ReadSingle(),
                Z = ReadSingle()
            };
        }

        public void SkipBytes(int length)
        {
            BaseStream.Position += length;
        }
    }
}