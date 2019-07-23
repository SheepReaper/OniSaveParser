using SheepReaper.GameSaves.Model.SaveFile.Schema;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;

namespace SheepReaper.GameSaves.Interfaces
{
    public interface IKleiDataReader : IBinaryReader
    {
        object Parse(System.Collections.Generic.List<Template> templates, TypeInfoElement type);
        object Parse(System.Collections.Generic.List<Template> templates, string templateName);
        GameObject ParseGameObject();
        string ReadKleiString();
        string ValidateDotNetIdentifierName(string name);
    }
}