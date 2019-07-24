using System.Collections.Generic;

namespace SheepReaper.GameSaves.Klei
{
    public interface IParser
    {
        object Parse(IDataReader reader, TypeInfo info, List<Template> templates);
    }

    public interface IParser<out T> : IParser
    {
        new T Parse(IDataReader reader, TypeInfo info, List<Template> templates);
    }
}