# OniSaveParser
This library deserializes and (in the future will) serialize save data from Oxygen Not Included. It is a .NET standard library and will work with anything .NET.

This is a utility library for reading saves. If you are looking for a way to edit your save files, you can do so using Duplicity: https://robophred.github.io/oni-duplicity/#/. I do not know of any projects implementing this library yet

## Game Compatibility
This library currently supports save version 7.11.

This library intends to parse the most recent version of the save file (even test build)

## API
SheepReaper.GameSaves.Klei.Deserializer is the only class you need at the moment.
It has 4 constructors:
- Deserializer(): This default contructor allows you to allocate the object without triggering the deserialization automatically. You must call .Parse() explicitely if you use this constructor.
- Deserializer(string): Accepts a path to an ONI save file and will automatically deserialize.
- Deserializer(Stream): Accepts a Stream and will automatically deserialize. (Usually a FileStream or a MemoryStream)
- Deserializer(Memory<byte>): Accepts a Memory<byte> (or byte[]) and will automatically deserialize.
In all 4 cases, the deserialized object is accessible through the .GameSave Property. (and the Parse() method returns it as well, if you went that way.)

## Design Philosophy
The original project that this work is based on used the "Idempotent load-save cycle" (ie a re-serialization without any edits should produce identical byte-code). However, for this version of the library, I intend to write lots of unit tests and plug in some CI/CD. I do not yet know how important the actual element ordering affects the compatibility of the save file. I looked to use as much native .NET serialization support as was available thinking that ONI on Unity makes use of .NET and that Klei would not be insane when building serialization/deserialization. If anyone knows how they actually serialize the thing, I'd appreciate it.

## Example usage (There is also a complete console app that demoes this)
```csharp
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string saveFileLocation = "F:\\Documents\\Klei\\OxygenNotIncluded\\save_files\\plucky.sav";
            const string jsonOutputLocation = "C:\\temp\\output.json";

            GameSave gameSave;

            using (var deserializer = new Deserializer(saveFileLocation))
            {
                gameSave = deserializer.GameSave;
            }

            // have fun with gameSave

            // write to json
            using (var writer = new FileStream(jsonOutputLocation, FileMode.OpenOrCreate))
            {
                var serialized = JsonConvert.SerializeObject(gameSave, Formatting.Indented);
                var buffer = new Span<byte>(new byte[serialized.Length]);
                Encoding.UTF8.GetBytes(serialized.AsSpan(), buffer);

                writer.Write(buffer);
            }

            // write to console
            Console.WriteLine(JsonConvert.SerializeObject(gameSave));

            Console.WriteLine("\nDemo Finished, press ANY key to exit...");
            Console.ReadKey();
        }
    }
```

## Still to do
The Serializer (Duh) Can only Deserialize at the moment.
Handle the special-case manual-parse data used by a few of the game object types.

## Special Thanks
To RoboPhred - without whose work I would never have gotten this far. This started as a "How come no one has done this before" to "How come there isn't a .NET version of this?" to me learning a bunch of new stuff about Serialization/Deserialization and byte by byte analysis and lots and lots of gray hairs.
