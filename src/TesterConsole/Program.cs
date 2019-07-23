using Newtonsoft.Json;
using SheepReaper.GameSaves;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SheepReaper.GameSaves.Model;
using SheepReaper.GameSaves.Model.SaveFile.Schema;
using SheepReaper.GameSaves.Model.SaveFile.TypeTemplates;
using ZeroFormatter;

namespace TesterConsole
{
    internal class Program
    {
        private static object CreateContext(SaveGameHeader header, List<Template> templates, KleiDataReader dataReader)
        {
            throw new NotImplementedException();
        }

        private static void Main2(string[] args)
        {
            var root = new List<object>();
            root.Add(new SaveFileHeadPart());
            var list = new List<object>();
            list.Add(new World());
            list.Add(new World());

            root.Add(list);
            
            var bf = new BinaryFormatter();
            using (var stream = new FileStream("C:\\temp\\output2.sav", FileMode.OpenOrCreate))
            {
                ZeroFormatterSerializer.Serialize(stream, root);
                //bf.Serialize(stream, root);
            }
        }

        private static void Main(string[] args)
        {
            SaveGameHeader header;
            List<Template> templates;
            Span<byte> buffer;
            SaveFileHeadPart saveFileHeader;
            SaveGameBody saveGameBody;
            //SaveGameParser parser;

            using (var fileStream = new FileStream("F:\\Documents\\Klei\\OxygenNotIncluded\\save_files\\plucky.sav", FileMode.Open))
            {
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer);
            }

            //var parser = new SaveGameParser(buffer);
            var parser = new SaveGameParser("F:\\Documents\\Klei\\OxygenNotIncluded\\save_files\\plucky.sav");
            saveFileHeader = parser.SaveFileHeader;
            saveGameBody = parser.SaveFileBody;

            // MY Stuff here
            string metaJson = JsonConvert.SerializeObject(new
            {
                saveFileHeader,
                saveGameBody
            },Formatting.Indented);

            Console.WriteLine($"meta (jsonized):\n\n{metaJson}\n");

            //Console.WriteLine(thisother);

            //while (true)
            //{
            //    Console.ReadKey();
            //    Console.WriteLine(dataReader.ReadByte());
            //}

            //var ms = new MemoryStream();
            //var bytes = new byte[100]

            Console.ReadKey();
        }
    }
}