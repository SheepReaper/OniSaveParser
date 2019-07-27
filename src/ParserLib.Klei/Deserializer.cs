using System;
using System.IO;

namespace SheepReaper.GameSaves.Klei
{
    public class Deserializer : IDisposable
    {
        private DataReader _dr;
        private GameSave _gameSave;
        private bool _isParsed;

        private void Parse(bool isConfigured)
        {
            IsConfigured = isConfigured;
            Parse();
        }

        private void ThrowIfNotParsed()
        {
            if (!_isParsed) throw new InvalidOperationException("SaveFile has not been parsed yet");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dr?.Dispose();
            }
        }

        public Deserializer()
        {
        }

        public Deserializer(Memory<byte> buffer)
        {
            _dr = new DataReader(buffer);
            Parse(true);
        }

        public Deserializer(Stream stream)
        {
            _dr = new DataReader(stream);
            Parse(true);
        }

        public Deserializer(string pathToSaveFile)
        {
            _dr = new DataReader(new FileStream(pathToSaveFile, FileMode.Open));
            Parse(true);
        }

        public GameSave GameSave
        {
            get
            {
                ThrowIfNotParsed();
                return _gameSave;
            }
        }

        public bool IsConfigured { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public GameSave Parse()
        {
            if (!IsConfigured) throw new InvalidOperationException("Cannot call 'Parse' on a Parser that is not configured.");

            _gameSave = _dr.ParseHeaderAndTemplates();

            if (_gameSave.BodyIsCompressed)
            {
                _dr.DecompressBody();
                _gameSave.BodyIsCompressed = false;
            }

            _gameSave.Body = _dr.ParseSaveFileBody();
            _isParsed = true;

            return _gameSave;
        }
    }
}