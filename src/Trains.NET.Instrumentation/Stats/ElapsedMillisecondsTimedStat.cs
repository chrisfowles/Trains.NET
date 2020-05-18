﻿using System.Diagnostics;

namespace Trains.NET.Instrumentation
{
    public class ElapsedMillisecondsTimedStat : AveragedStat
    {
        private readonly Stopwatch _sw;
        public ElapsedMillisecondsTimedStat()
            : base(10) // Average over 10 samples
        {
            _sw = new Stopwatch();
        }
        public void Start() => _sw.Restart();
        public void Stop()
        {
            _sw.Stop();
            SetValue(_sw.ElapsedMilliseconds);
        }
        public override string GetDescription() => (this.Value?.ToString("G4") ?? string.Empty) + "ms";
    }
}
