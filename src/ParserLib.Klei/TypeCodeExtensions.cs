namespace SheepReaper.GameSaves.Klei
{
    public static class TypeCodeExtensions
    {
        public static SerializationTypeCode GetImpliedType(this SerializationTypeCode code)
        {
            const int bitmask = 0x3f;

            return (SerializationTypeCode)((byte)code & bitmask);
        }

        public static bool IsValueType(this SerializationTypeCode code)
        {
            const byte bitmask = (byte)SerializationTypeFlags.IsValueType;

            return ((byte)code & bitmask) > 0;
        }
    }
}