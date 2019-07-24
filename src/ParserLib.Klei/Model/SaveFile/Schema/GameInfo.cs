namespace SheepReaper.GameSaves.Model.SaveFile.Schema
{
    public class GameInfo
    {
        public string BaseName { get; set; }
        public bool IsAutoSave { get; set; }
        public int NumberOfCycles { get; set; }
        public int NumberOfDuplicants { get; set; }
        public string OriginalSaveName { get; set; }
        public int SaveMajorVersion { get; set; }
        public int SaveMinorVersion { get; set; }
    }
}