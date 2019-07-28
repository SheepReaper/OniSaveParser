using SheepReaper.GameSaves.Klei.Schema.Oni;
using System.Numerics;

namespace SheepReaper.GameSaves.Klei
{
    public interface IDataReader
    {
        int PositionInt { get; set; }

        object Parse(System.Collections.Generic.List<Template> templates, TypeInfo type);

        object Parse(System.Collections.Generic.List<Template> templates, string templateName);

        GameSave Parse();

        GameObject ParseGameObject();

        bool ReadBoolean();

        byte[] ReadBytes(int length);

        int ReadInt32();

        float ReadSingle();

        string ReadString();

        Vector2 ReadVector2();

        Vector2I ReadVector2I();

        Vector3 ReadVector3();

        string ValidateDotNetIdentifierName(string name);
    }
}