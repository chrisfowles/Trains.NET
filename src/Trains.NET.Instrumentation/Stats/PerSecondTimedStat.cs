﻿using System.Diagnostics;

namespace Trains.NET.Instrumentation
{
    public class PerSecondTimedStat : AveragedStat
    {
        private readonly Stopwatch _sw;
        public PerSecondTimedStat() 
            : base(60) // Average over 60 samples
        {
            _sw = new Stopwatch();
        }

        public void Update()
        {
            if(_sw.IsRunning)
            {
                SetValue(1000.0 / _sw.ElapsedMilliseconds);
            }
            _sw.Restart();
        }

        public override string GetDescription() => this.Value?.ToString("G4") ?? string.Empty;
    }
}
