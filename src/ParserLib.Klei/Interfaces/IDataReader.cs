using SheepReaper.GameSaves.Klei.Schema.Oni;

namespace SheepReaper.GameSaves.Klei
{
    public interface IDataReader : IBinaryReader
    {
        object Parse(System.Collections.Generic.List<Template> templates, TypeInfo type);

        object Parse(System.Collections.Generic.List<Template> templates, string templateName);

        GameObject ParseGameObject();

        string ValidateDotNetIdentifierName(string name);
    }
}