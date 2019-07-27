using System;
using System.Numerics;

namespace SheepReaper.GameSaves
{
    public interface IBinaryReader
    {
        //Memory<byte> Buffer { get; }
        int PositionInt { get; }

        byte[] ReadAllBytes();

        bool ReadBoolean();

        byte ReadByte();

        byte[] ReadBytes(int length);

        char[] ReadChars(int length);

        double ReadDouble();

        short ReadInt16();

        int ReadInt32();

        long ReadInt64();

        Quaternion ReadQuaternion();

        sbyte ReadSByte();

        float ReadSingle();

        string ReadString();

        ushort ReadUInt16();

        uint ReadUInt32();

        ulong ReadUInt64();

        Vector2 ReadVector2();

        Vector2I ReadVector2I();

        Vector3 ReadVector3();

        void SkipBytes(int length);

        byte[] ViewAllBytes();

        Array ViewBytes(int length);
    }
}