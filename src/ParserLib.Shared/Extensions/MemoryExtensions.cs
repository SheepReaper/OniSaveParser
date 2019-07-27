using System;
using System.Runtime.InteropServices;

namespace SheepReaper.GameSaves
{
    public static class MemoryExtensions
    {
        public static byte[] GetBuffer(this Memory<byte> input)
        {
            MemoryMarshal.TryGetArray<byte>(input, out var buffer);
            return buffer.Array;
        }
    }
}