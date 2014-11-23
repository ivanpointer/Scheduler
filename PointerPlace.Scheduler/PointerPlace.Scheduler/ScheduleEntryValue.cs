/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using System;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Reperesents a single entry value in a schedule entry
    /// </summary>
    public class ScheduleEntryValue
    {
        /// <summary>
        /// The value of the schedule entry value
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// The threshold of the schedule entry value (the top-end value of the range)
        /// </summary>
        public int Threshold { get; set; }

        /// <summary>
        /// Builds a schedule entry value with a default range of 0 to Int32.MaxValue
        /// </summary>
        /// <param name="value">The lower value to assign to the entry</param>
        /// <param name="threshold">The upper threshold of the schedule entry</param>
        public ScheduleEntryValue(int value = 0, int threshold = Int32.MaxValue)
        {
            Value = value;
            Threshold = threshold;
        }
    }
}
