using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SheepReaper.GameSaves.Klei
{
    public class Deserializer : IDisposable
    {
        private IDataReader _dr;
        private GameSave _gameSave;
        private bool _isParsed;
        private string _pathToSaveFile;

        private void Parse(bool isConfigured)
        {
            IsConfigured = isConfigured;
            Parse();
        }

        private void ThrowIfConfigured()
        {
            if (IsConfigured) throw new InvalidOperationException("This serializer instance is already configured.");
        }

        private void ThrowIfNotParsed()
        {
            if (!_isParsed) throw new InvalidOperationException("SaveFile has not been parsed yet");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dr is DataReader dataReader)
            {
                dataReader.BaseStream.Dispose();
            }
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public Deserializer()
        {
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public Deserializer(IDataReader dataReader)
        {
            _dr = dataReader;
            IsConfigured = true;
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public Deserializer(Memory<byte> buffer)
        {
            _dr = new DataReader(buffer);
            Parse(true);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public Deserializer(Stream stream)
        {
            _dr = new DataReader(stream);
            Parse(true);
        }

        public Deserializer(string pathToSaveFile)
        {
            _pathToSaveFile = pathToSaveFile;
            _dr = new DataReader(new FileStream(pathToSaveFile, FileMode.Open));
            Parse(true);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public IDataReader DataReader
        {
            get => _dr;
            set
            {
                ThrowIfConfigured();
                _dr = value;
                IsConfigured = true;
            }
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
        
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public string SaveFilePath
        {
            get => _pathToSaveFile;
            set
            {
                ThrowIfConfigured();
                _pathToSaveFile = value;
                _dr = new DataReader(new FileStream(_pathToSaveFile, FileMode.Open));
                IsConfigured = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public GameSave Parse()
        {
            if (_isParsed) return _gameSave;

            if (!IsConfigured)
                throw new InvalidOperationException("Cannot call 'Parse' on a Parser that is not configured.");

            _gameSave = _dr.Parse();
            _isParsed = true;

            return _gameSave;
        }
    }
}