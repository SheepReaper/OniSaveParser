namespace SheepReaper.GameSaves
{
    public static class TypeCodeExtensions
    {
        public static SerializationTypeCode GetImpliedType(this SerializationTypeCode code)
        {
            //if ((byte)code == 0x80) return SerializationTypeCode.UserDefined;

            var bitmask = 0x3f;

            return (SerializationTypeCode)((byte)code & bitmask);
        }

        public static bool IsValueType(this SerializationTypeCode code)
        {
            var bitmask = (byte)SerializationTypeFlags.IS_VALUE_TYPE;

            return ((byte)code & bitmask) > 0;
        }
    }
}