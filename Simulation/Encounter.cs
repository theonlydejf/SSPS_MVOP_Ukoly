using System;

namespace Simulation
{
    /// <summary>
    /// Struct that is used to describe encounter of two objects
    /// </summary>
    public struct Encounter
    {
        /// <summary>
        /// Instance of <see cref="Encounter"/> used to describe no encounter
        /// </summary>
        public static Encounter NULL => new Encounter()
        {
            Distance = float.PositiveInfinity,
            Timestamp = TimeSpan.FromSeconds(-1),
            WorldSnapshot = null
        };

        /// <summary>
        /// Distance between the two objects during the encounter
        /// </summary>
        public float Distance { get; set; }
        /// <summary>
        /// Timestamp at which the encounter occured
        /// </summary>
        public TimeSpan Timestamp { get; set; }
        /// <summary>
        /// A snapshot of the word at the time the encounter happened
        /// </summary>
        public World WorldSnapshot { get; set; }
    }
}
