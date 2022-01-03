using System;

namespace Simulation
{
    /// <summary>
    /// Class that holds data about a step that has been taken
    /// </summary>
    public class SteppedEventArgs : EventArgs
    {
        /// <summary>
        /// A clone of the world after the step was taken
        /// </summary>
        public World WorldSnapshot { get; set; }
        /// <summary>
        /// A since the world has been created in the world time
        /// </summary>
        public TimeSpan TimeSinceEpoch { get; set; }
    }
}
