using Newtonsoft.Json;
using SheepReaper.GameSaves;
using SheepReaper.GameSaves.Klei;
using System;
using System.IO;
using System.Text;

namespace TesterConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string saveFileLocation = "F:\\Documents\\Klei\\OxygenNotIncluded\\save_files\\plucky.sav";
            const string jsonOutputLocation = "C:\\temp\\output.json";

            var deserializer = new Deserializer(saveFileLocation);
            var gameSave = deserializer.GameSave;

            // have fun with gameSave

            // write to json
            using (var writer = new FileStream(jsonOutputLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                writer.SetLength(0);
                var serialized = JsonConvert.SerializeObject(gameSave, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new IgnoreEmptyEnumerablesResolver() });
                var buffer = new Span<byte>(new byte[serialized.Length]);
                Encoding.UTF8.GetBytes(serialized.AsSpan(), buffer);

                writer.Write(buffer);
            }

            // write to console
            //Console.WriteLine(JsonConvert.SerializeObject(gameSave));

            Console.WriteLine("\nDemo Finished, press ANY key to exit...");
            Console.ReadKey();
        }
    }
}