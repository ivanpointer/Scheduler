/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using System.Collections.Generic;

namespace PointerPlace.Scheduler.Matchers
{
    /// <summary>
    /// Defines a matcher and the common functionality/helpers for the other matchers
    /// </summary>
    public abstract class MatcherBase
    {
        /// <summary>
        /// Determines whether the given PointInTime matches this matcher
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <returns>A boolean indicating whether or not the given PointInTime matches this matcher</returns>
        public abstract bool Matches(PointInTime point);

        /// <summary>
        /// Determins whether the point in time assigned to this matcher, matches this matcher
        /// </summary>
        /// <returns>A boolean indicating whether or not the PointInTime assigned to this matcher, matches this matcher</returns>
        public abstract bool Matches();

        /// <summary>
        /// Determines the next matching value for this matcher, starting from the PointInTime assigned to this matcher.  If there
        /// is no valid next value, -1 is returned.
        /// </summary>
        /// <returns>The next matching value for this matcher, starting from the PointInTime assigned to this matcher.  If there
        /// is no valid next value, -1 is returned.</returns>
        public abstract int NextMatch();

        /// <summary>
        /// Tries to increment the internal PointInTime to its next valid value, returns true if successfull, false otherwise.
        /// </summary>
        /// <returns>A boolean indicating whether or not the internal PointInTime was incremented to the next valid value; true on success, false on failure.</returns>
        public abstract bool Increment();

        /// <summary>
        /// Retrieves the next match from the list of valid values, starting at the current value
        /// </summary>
        /// <param name="validValues">The list of valid values to search</param>
        /// <param name="currentValue">The current value from which to search</param>
        /// <returns>The next match from the valid values list, or -1 if none</returns>
        public static int NextMatch(int[] validValues, int currentValue)
        {
            return NextMatch(validValues, currentValue, validValues[validValues.Length - 1]);
        }

        /// <summary>
        /// Retrieves the next match from the list of valid values, starting at the current value, using the provided maximum
        /// </summary>
        /// <param name="validValues">The list of valid values to search</param>
        /// <param name="currentValue">The current value from which to search</param>
        /// <param name="actualMax">The actual maximum of the sequence</param>
        /// <returns>The next match from the valid values list, or -1 if none</returns>
        public static int NextMatch(int[] validValues, int currentValue, int actualMax)
        {
            int index = 0;

            int nextMatch = -1;
            while (nextMatch <= currentValue && index < validValues.Length)
                nextMatch = validValues[index++];

            return (index == validValues.Length && nextMatch <= currentValue) || actualMax < nextMatch
                ? -1
                : nextMatch;
        }

        /// <summary>
        /// Generates a simple list of valid values from the given schedule, within the parameters of lower - upper, where upper is a count, not an actual value
        /// </summary>
        /// <param name="schedule">The schedule to use to build out the values</param>
        /// <param name="lower">The low end of the range</param>
        /// <param name="upper">The number of values to generate, the top end of the rage</param>
        /// <returns>A list of valid values from the given schedule, within the provided lower - upper range</returns>
        protected static int[] GenerateSimpleList(ScheduleEntry schedule, int lower, int upper)
        {
            var numbers = new List<int>();
            for (int number = lower; number < (upper + lower); number++)
                if (MatchesSchedule(schedule, number))
                    numbers.Add(number);

            return numbers.ToArray();
        }

        // Determines if the given value matches the given schedule
        private static bool MatchesSchedule(ScheduleEntry schedule, int value)
        {
            return ContainsValue(schedule, value) &&
                MatchesInterval(schedule, value);
        }

        // Determines whether or not the given schedule contains the given value
        private static bool ContainsValue(ScheduleEntry schedule, int value)
        {
            var values = schedule.Values;

            bool valid = false;
            int index = 0;
            ScheduleEntryValue entryValue;
            while (valid == false && index < values.Length)
            {
                entryValue = values[index++];
                valid |= entryValue.Value <= value && value <= entryValue.Threshold;
            }

            return valid;
        }

        // Determines whether or not the given value matches the interval of the schedule
        private static bool MatchesInterval(ScheduleEntry schedule, int value)
        {
            return schedule.Interval == 0 || value % schedule.Interval == 0;
        }

    }
}

 