using System.Diagnostics;

namespace Chronometer
{
    public class Chronometer : IChronometer
    {
        private readonly Stopwatch _stopWatch;
        private readonly List<string> _laps;
        public Chronometer()
        {
            this._stopWatch = new Stopwatch();
            this._laps = new List<string>();
        }
        public string GetTime => this._stopWatch.Elapsed.ToString(@"mm\:ss\.ffff");

        public List<string> Laps => this._laps;

        public string Lap()
        {
            string result = this.GetTime;
            this._laps.Add(result);
            return result;
        }

        public void Reset()
        {
            this._stopWatch.Reset();
            this._laps.Clear();
        }

        public void Start()
        {
            this._stopWatch.Start();
        }

        public void Stop()
        {
            this._stopWatch.Stop();
        }
    }
}
