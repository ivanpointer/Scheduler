/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

namespace PointerPlace.Scheduler.Matchers
{
    /// <summary>
    /// Matches and increments the year fild on a PointInTime.
    /// The gregorian calendar repeats itself exactly every 28 years,
    /// so the limit on this for matching is that the year be within
    /// 28 years
    /// </summary>
    public class YearMatcher : MatcherBase
    {

        #region Members

        private PointInTime Point { get; set; }
        private int MaxYear { get; set; }

        #endregion

        /// <summary>
        /// Constructs a YearMatcher against a given PointInTime and the max year to check
        /// </summary>
        /// <param name="point"></param>
        /// <param name="maxYear"></param>
        public YearMatcher(PointInTime point, int maxYear)
        {
            Point = point;
            MaxYear = maxYear;
        }

        // Always returns true, the cron schedule doesn't discriminate against year
        public override bool Matches(PointInTime point)
        {
            return true;
        }

        // Always returns true, the cron schedule doesn't discriminate against year
        public override bool Matches()
        {
            return true;
        }

        // Returns the next year, the cron schedule doesn't discriminate against year
        public override int NextMatch()
        {
            return Point.Year + 1;
        }

        // Tries to increment the year, returns true if it can increment, I.E. within 28 years of
        //  the original point in time
        public override bool Increment()
        {
            if (Point.Year <= MaxYear)
            {
                Point.AdvanceYear(1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
