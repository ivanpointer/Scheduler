/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using System;
using System.Linq;

namespace PointerPlace.Scheduler.Matchers
{
    /// <summary>
    /// Matches and increments the minute field on a PointInTime
    /// </summary>
    public class MinuteMatcher : MatcherBase
    {

        #region Members

        private PointInTime Point { get; set; }

        private bool Any { get; set; }
        private int[] ValidMinutes { get; set; }

        #endregion

        /// <summary>
        /// Constructs a MinuteMatcher for the given PointInTime and Schedule
        /// </summary>
        /// <param name="point">The PointInTime for which to build this matcher</param>
        /// <param name="schedule">The Schedule for which to build this matcher</param>
        public MinuteMatcher(PointInTime point, Schedule schedule)
        {
            Point = point;

            ValidMinutes = GenerateSimpleList(schedule.Minute, 0, 60);

            var length = ValidMinutes.Length;
            if (length == 60)
            {
                Any = true;
            }
            else if (length != 0)
            {
                Any = false;
            }
            else
            {
                throw new ImpossibleScheduleException("Schedule has no possible valid minutes");
            }
        }

        // Determines whether the given PointInTime matches this matcher
        public override bool Matches(PointInTime point)
        {
            return Any || ValidMinutes.Contains(point.Minute);
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
            return NextMatch(ValidMinutes, Point.Minute);
        }

        // Tries to increment the internal PointInTime to its next valid value, returns true if successfull, false otherwise.
        public override bool Increment()
        {
            var nextMatch = NextMatch();
            if (nextMatch != -1)
            {
                Point.AdvanceMinute(nextMatch);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
