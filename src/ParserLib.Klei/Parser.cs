using Ionic.Zlib;
using SheepReaper.GameSaves.Model;
using System;
using System.IO;

namespace SheepReaper.GameSaves
{
    public class SaveGameParser
    {
        private KleiDataReader _dr;
        private bool _isParsed;
        private SaveGameBody _sfb;
        private SaveFileHeadPart _sfh;

        private void DecompressBody()
        {
            var bodyStartPosition = _dr.Position;
            var uncompressedBodyBytes = ZlibStream.UncompressBuffer(_dr.GetBuffer().Slice(bodyStartPosition).ToArray());
            var uncompressedLength = bodyStartPosition + uncompressedBodyBytes.Length;
            var uncompressedStream = new MemoryStream(new byte[uncompressedLength], 0, uncompressedLength, true, true);
            uncompressedStream.Write(_dr.GetBuffer().Slice(0, bodyStartPosition));
            uncompressedStream.Write(uncompressedBodyBytes);
            _dr = new KleiDataReader(uncompressedStream)
            {
                Position = bodyStartPosition,
                Templates = _dr.Templates
            };
            _sfh.BodyIsCompressed = false;
        }

        private void Parse(bool isConfigured)
        {
            IsConfigured = isConfigured;
            Parse();
        }

        private void ThrowIfNotParsed()
        {
            if (!_isParsed) throw new InvalidOperationException("SaveFile has not been parsed yet");
        }

        public SaveGameParser()
        {
        }

        public SaveGameParser(Span<byte> buffer)
        {
            _dr = new KleiDataReader(buffer);
            Parse(true);
        }

        public SaveGameParser(Stream stream)
        {
            _dr = new KleiDataReader(stream);
            Parse(true);
        }

        public SaveGameParser(string pathToSaveFile)
        {
            _dr = new KleiDataReader(new FileStream(pathToSaveFile, FileMode.Open));
            Parse(true);
        }

        public bool IsConfigured { get; private set; }

        public SaveGameBody SaveFileBody
        {
            get
            {
                ThrowIfNotParsed();
                return _sfb;
            }
        }

        public SaveFileHeadPart SaveFileHeader
        {
            get
            {
                ThrowIfNotParsed();
                return _sfh;
            }
        }

        public void Parse()
        {
            if (!IsConfigured) throw new InvalidOperationException("Cannot call 'Parse' on a Parser that is not configured.");
            _sfh = _dr.GetSaveFileHeader();
            if (_sfh.BodyIsCompressed) DecompressBody();
            _sfb = _dr.ParseSaveFileBody();

            _isParsed = true;
        }
    }
}