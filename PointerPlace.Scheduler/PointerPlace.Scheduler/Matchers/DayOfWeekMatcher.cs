/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */


using System.Linq;

namespace PointerPlace.Scheduler.Matchers
{
    /// <summary>
    /// A matcher which is used to match day of week on a PointInTime.  No incrementing is done
    /// for day of week, the incrementing is done through DayOfMonth.
    /// </summary>
    public class DayOfWeekMatcher : MatcherBase
    {

        #region Members

        private PointInTime Point { get; set; }

        private bool Any { get; set;}
        private int[] ValidDaysOfWeek { get; set; }

        #endregion

        /// <summary>
        /// Constructs a DayOfWeekMatcher for the given PointInTime and Schedule
        /// </summary>
        /// <param name="point">The PointInTime for which to build this matcher</param>
        /// <param name="schedule">The Schedule for which to build this matcher</param>
        public DayOfWeekMatcher(PointInTime point, Schedule schedule)
        {
            Point = point;

            ValidDaysOfWeek = GenerateSimpleList(schedule.DayOfWeek, 1, 7);

            var length = ValidDaysOfWeek.Length;
            if (length == 7)
            {
                Any = true;
            }
            else if (length != 0)
            {
                Any = false;
            }
            else
            {
                throw new ImpossibleScheduleException("Schedule has no possible valid days of week");
            }
        }

        // Determines whether the given PointInTime matches this matcher
        public override bool Matches(PointInTime point)
        {
            return Any || ValidDaysOfWeek.Contains(point.WeekDay);
        }

        // Determins whether the point in time assigned to this matcher, matches this matcher
        public override bool Matches()
        {
            return Matches(Point);
        }

        // Determines the next matching value for this matcher, starting from the PointInTime assigned to this matcher.  If there
        //  is no valid next value, -1 is returned.
        public override int NextMatch()
        {
            return NextMatch(ValidDaysOfWeek, Point.WeekDay);
        }

        // Tries to increment the internal PointInTime to its next valid value, returns true if successfull, false otherwise.
        public override bool Increment()
        {
            return false;
        }

    }
}
