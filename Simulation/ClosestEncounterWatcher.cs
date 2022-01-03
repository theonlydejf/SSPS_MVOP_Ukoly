using System;

namespace Simulation
{
    /// <summary>
    /// A class that looks for a closest encounter of two objects in a simulation
    /// </summary>
    public class ClosestEncounterWatcher : SimulationWatcher
    {
        /// <summary>
        /// The default settings
        /// </summary>
        public static readonly ClosestEncounterWatcherSettings DEFAULT_SETTINGS = new ClosestEncounterWatcherSettings()
        {
            AutoStopSimulation = false
        };

        /// <summary>
        /// Creates an instance of <see cref="ClosestEncounterWatcher"/>
        /// </summary>
        /// <param name="simulation">The simulation on which the watcher should work</param>
        /// <param name="obj1">The first object which the watcher should watch</param>
        /// <param name="obj2">The second object which the watcher should watch</param>
        public ClosestEncounterWatcher(Simulation simulation, World.Object obj1, World.Object obj2) : this(simulation, obj1, obj2, DEFAULT_SETTINGS)
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="ClosestEncounterWatcher"/>
        /// </summary>
        /// <param name="simulation">The simulation on which the watcher should work</param>
        /// <param name="obj1">The first object which the watcher should watch</param>
        /// <param name="obj2">The second object which the watcher should watch</param>
        /// <param name="settings">Settings defining how the watcher should act</param>
        public ClosestEncounterWatcher(Simulation simulation, World.Object obj1, World.Object obj2, ClosestEncounterWatcherSettings settings) : base(simulation)
        {
            this.settings = settings;
            this.obj1 = obj1;
            this.obj2 = obj2;
        }

        /// <summary>
        /// Latest recorded closest encounter
        /// </summary>
        public Encounter CurrentClosestEncounter => closestEncounter;
        private Encounter closestEncounter = Encounter.NULL;

        private ClosestEncounterWatcherSettings settings;
        private float lastDistance = float.PositiveInfinity;
        private readonly World.Object obj1;
        private readonly World.Object obj2;

        /// <summary>
        /// Discards the current closest encounter and starts looking for a new one
        /// </summary>
        public void ResetClosestEncounter() => closestEncounter = Encounter.NULL;

        protected override void Simulation_Stepped(object sender, SteppedEventArgs e)
        {
            float distance = (obj1.Location - obj2.Location).Length();

            // If the distance between objects is closer than anytime before -> record the encounter
            if (distance < closestEncounter.Distance)
                closestEncounter = new Encounter()
                {
                    Distance = distance,
                    Timestamp = e.TimeSinceEpoch,
                    WorldSnapshot = e.WorldSnapshot
                };

            // When objects are starting to move away and settings allow it –> stop the simulation
            if (settings.AutoStopSimulation && distance > lastDistance)
                simulation.StopSimulation();

            lastDistance = distance;
        }
    }
}
