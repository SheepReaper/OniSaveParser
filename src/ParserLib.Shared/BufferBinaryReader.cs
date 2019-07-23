using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace SheepReaper.GameSaves
{
    public class BufferBinaryReader : BinaryReader, IBinaryReader
    {
        public BufferBinaryReader(Span<byte> buffer) : this(new MemoryStream(buffer.ToArray(), 0, buffer.Length, true, true), Encoding.UTF8, false)
        {
        }

        public BufferBinaryReader(Span<byte> buffer, Encoding encoding) : this(new MemoryStream(buffer.ToArray(), 0, buffer.Length, true, true), encoding, false)
        {
        }

        public BufferBinaryReader(Span<byte> buffer, Encoding encoding, bool leaveOpen) : this(new MemoryStream(buffer.ToArray(), 0, buffer.Length, true, true), encoding, leaveOpen)
        {
        }

        public BufferBinaryReader(Stream stream) : this(stream.ToStatic(stream.CanWrite), Encoding.UTF8, false)
        {
        }

        public BufferBinaryReader(Stream stream, Encoding encoding) : this(stream.ToStatic(stream.CanWrite), encoding, false)
        {
        }

        public BufferBinaryReader(Stream stream, Encoding encoding, bool leaveOpen) : base(stream.ToStatic(stream.CanWrite), encoding, leaveOpen)
        {
        }

        public bool CanGetBuffer { get; } = true;

        public int Position { get => (int)BaseStream.Position; set => BaseStream.Position = value; }

        public Span<byte> GetBuffer()
        {
            if (CanGetBuffer)
            {
                ((MemoryStream)BaseStream).TryGetBuffer(out var buffer);
                return buffer;
            }
            else
            {
                throw new NotSupportedException("The underlying Stream to this ArrayBinaryReader instance is not a MemoryStream or otherwise does not support Getting the underlying buffer ");
            }
        }

        public byte[] ReadAllBytes() => ReadBytes((int)(BaseStream.Length - BaseStream.Position));

        //public override int ReadInt32()
        //{
        //    Console.WriteLine($"Call to read int: Position(base stream): {Position}({BaseStream.Position}) of {BaseStream.Length}(base stream)");
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