namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Parses a cron style schedule string into a Schedule so that the engine
    /// can determine the next value in the schedule.
    /// </summary>
    public interface IScheduleParser
    {
        /// <summary>
        /// Parses the provided schedule string into a Schedule
        /// </summary>
        /// <param name="scheduleString">The schedule string to parse into a Schedule</param>
        /// <returns>The Schedule that was parsed from the provided schedule string</returns>
        Schedule ParseSchedule(string scheduleString);
    }
}