using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public interface IExtraDataParser
    {
        List<object> Parse(IDataReader reader, List<Template> templates);
    }
}