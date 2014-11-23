/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// A struct representing a single schedule entry in the schedule
    /// </summary>
    public struct ScheduleEntry
    {
        /// <summary>
        /// A list of values for the schedule entry
        /// </summary>
        public ScheduleEntryValue[] Values;
        /// <summary>
        /// The interval assocaited with the schedule entry
        /// </summary>
        public int Interval;
    }
}
