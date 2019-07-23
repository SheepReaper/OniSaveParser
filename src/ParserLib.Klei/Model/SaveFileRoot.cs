using ZeroFormatter;

namespace SheepReaper.GameSaves.Model
{
    [ZeroFormattable]
    public class SaveFileRoot
    {
        [Index(0)]
        public virtual int WidthInCells { get; set; }
    }
}