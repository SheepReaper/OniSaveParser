using System.Runtime.Serialization;

namespace SheepReaper.GameSaves.Klei.Schema.Oni
{
    //[DataContract]
    public class ConduitFlow
    {
        //[Contract]
        public object SerializedContents { get; set; }
        public object SerializedIdx { get; set; }
        public object VersionedSerializedContents { get; set; }
    }
}