using Ionic.Zlib;
using System;
using System.IO;
using SheepReaper.GameSaves.Model;

namespace SheepReaper.GameSaves
{
    public class SaveGameParser
    {
        private KleiDataReader dr;
        private bool IsParsed;
        private SaveGameBody sfb;
        private SaveFileHeadPart sfh;

        private void DecompressBody()
        {
            var bodyStartPosition = dr.Position;
            var uncompressedBodyBytes = ZlibStream.UncompressBuffer(dr.GetBuffer().Slice(bodyStartPosition).ToArray());
            var uncompressedLength = bodyStartPosition + uncompressedBodyBytes.Length;
            var uncompressedStream = new MemoryStream(new byte[uncompressedLength], 0, uncompressedLength, true, true);
            uncompressedStream.Write(dr.GetBuffer().Slice(0, bodyStartPosition));
            uncompressedStream.Write(uncompressedBodyBytes);
            dr = new KleiDataReader(uncompressedStream)
            {
                Position = bodyStartPosition,
                Templates = dr.Templates
            };
            sfh.BodyIsCompressed = false;
        }

        private void Parse(bool isConfigured)
        {
            IsConfigured = isConfigured;
            Parse();
        }

        private void ThrowIfNotParsed()
        {
            if (!IsParsed) throw new InvalidOperationException("SaveFile has not been parsed yet");
        }

        public SaveGameParser()
        {
        }

        public SaveGameParser(Span<byte> buffer)
        {
            dr = new KleiDataReader(buffer);
            Parse(true);
        }

        public SaveGameParser(Stream stream)
        {
            dr = new KleiDataReader(stream);
            Parse(true);
        }

        public SaveGameParser(string pathToSaveFile)
        {
            dr = new KleiDataReader(new FileStream(pathToSaveFile, FileMode.Open));
            Parse(true);
        }

        public bool IsConfigured { get; private set; } = false;

        public SaveGameBody SaveFileBody
        {
            get
            {
                ThrowIfNotParsed();
                return sfb;
            }
        }

        public SaveFileHeadPart SaveFileHeader
        {
            get
            {
                ThrowIfNotParsed();
                return sfh;
            }
        }

        public void Parse()
        {
            if (!IsConfigured) throw new InvalidOperationException("Cannot call 'Parse' on a Parser that is not configured.");
            sfh = dr.GetSaveFileHeader();
            if (sfh.BodyIsCompressed) DecompressBody();
            sfb = dr.ParseSaveFileBody();

            IsParsed = true;
        }
    }
}