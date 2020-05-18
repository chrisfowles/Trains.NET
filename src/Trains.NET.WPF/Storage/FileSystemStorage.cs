﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trains.NET.Engine;

namespace Trains.NET.WPF
{
    internal class FileSystemStorage : IGameStorage
    {
        private const string Filename = "Trains.NET.tracks";

        public readonly string FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Trains.NET", Filename);
        private readonly ITrackSerializer _serializer;

        public FileSystemStorage(ITrackSerializer serializer)
        {
            _serializer = serializer;
        }

        public IEnumerable<Track> ReadTracks()
        {
            if (!File.Exists(FilePath))
            {
                return Enumerable.Empty<Track>();
            }

            return _serializer.Deserialize(File.ReadAllLines(FilePath));
        }

        public void WriteTracks(IEnumerable<Track> tracks)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            File.WriteAllText(FilePath, _serializer.Serialize(tracks));
        }
    }
}

