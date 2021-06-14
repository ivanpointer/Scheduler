using System;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Responsible for taking a parsed schedule, and a DateTime starting point, and
    /// determining the next valid DateTime for the given schedule and starting point.
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        /// Returns the next DateTime for a given Schedule, from the given DateTime starting point.
        /// If no starting point is provided DateTime.Now is used.
        /// </summary>
        /// <param name="schedule">The schedule to use for determining the next DateTime</param>
        /// <param name="startingPoint">The DateTime from which to start looking.  Defaults to DateTime.Now.</param>
        /// <returns>The next DateTime for the given Schedule from the given DateTime starting point, null if no valid next date is found, the schedule is impossible, and a ImpossibleScheduleException is thrown.</returns>
        DateTime GetNext(Schedule schedule, DateTime? startingPoint = default);

        /// <summary>
        /// Parses the given schedule string into a schedule and determines the next DateTime from the starting point.
        /// If no starting point is provided DateTime.Now is used.
        /// </summary>
        /// <param name="scheduleString">The schedule string to parse</param>
        /// <param name="startingPoint">The DateTime from which to start looking.  Defaults to DateTime.Now.</param>
        /// <returns>The next DateTime for the given Schedule from the given DateTime starting point, null if no valid next date is found, the schedule is impossible, and a ImpossibleScheduleException is thrown.</returns>
        DateTime GetNext(string scheduleString, DateTime? startingPoint = default);
    }
}