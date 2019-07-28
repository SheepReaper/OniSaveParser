namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    //[DataContract]
    public class ConduitFlow
    {
        //[Contract]
        public ConduitContents[] serializedContents;

        public int[] SerializedIdx;

        public SerializedContents[] VersionedSerializedContents;

        public class ConduitContents
        {
        }

        public class SerializedContents
        {
        }
    }
}