namespace Simulation
{
    /// <summary>
    /// Settings that define how a <see cref="ClosestEncounterWatcher"/> acts
    /// </summary>
    public struct ClosestEncounterWatcherSettings
    {
        /// <summary>
        /// True, if the watcher should stop the simulation when the objects start to move away
        /// </summary>
        public bool AutoStopSimulation { get; set; }
    }
}
