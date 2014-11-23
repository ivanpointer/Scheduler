/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using PointerPlace.Scheduler.Matchers;
using System;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Responsible for taking a parsed schedule, and a DateTime starting point, and
    /// determining the next valid DateTime for the given schedule and starting point
    /// </summary>
    public class Scheduler
    {
        // The gregorian calendar repeats itself every 28 years
        private const int MaxYears = 28;

        /// <summary>
        /// Returns the next DateTime for a given Schedule, from the given DateTime starting point.
        /// If no starting point is provided DateTime.Now is used.
        /// </summary>
        /// <param name="schedule">The schedule to use for determining the next DateTime</param>
        /// <param name="startingPoint">The DateTime from which to start looking.  Defaults to DateTime.Now.</param>
        /// <returns>The next DateTime for the given Schedule from the given DateTime starting point, null if no valid next date is found, the schedule is impossible, and a ImpossibleScheduleException is thrown.</returns>
        public static DateTime GetNext(Schedule schedule, DateTime? startingPoint = null)
        {
            // Determine our starting point
            if (startingPoint.HasValue == false)
                startingPoint = DateTime.Now;

            // Figure out our maximum year
            var baseYear = startingPoint.Value.Year;
            var maxYear = baseYear + MaxYears;

            // Set to the top of the next minute
            var point = new PointInTime(startingPoint.Value);
            point.AdvanceMinute(point.Minute + 1);

            // Build out our matchers into our container
            var container = new ScheduleMatcher
            {
                Minute = new MinuteMatcher(point, schedule),
                Hour = new HourMatcher(point, schedule),
                DayOfMonth = new DayOfMonthMatcher(point, schedule),
                DayOfWeek = new DayOfWeekMatcher(point, schedule),
                Month = new MonthMatcher(point, schedule),
                Year = new YearMatcher(point, maxYear)
            };

            // Start off the recursive process, first, check the year
            MatchYear(container, point);
            return point.ToDateTime();
        }

        /// <summary>
        /// Parses the given schedule string into a schedule and determines the next DateTime from the starting point.
        /// If no starting point is provided DateTime.Now is used.
        /// </summary>
        /// <param name="scheduleString">The schedule string to parse</param>
        /// <param name="startingPoint">The DateTime from which to start looking.  Defaults to DateTime.Now.</param>
        /// <returns>The next DateTime for the given Schedule from the given DateTime starting point, null if no valid next date is found, the schedule is impossible, and a ImpossibleScheduleException is thrown.</returns>
        public static DateTime GetNext(string scheduleString, DateTime? startingPoint = null)
        {
            var schedule = ScheduleParser.ParseSchedule(scheduleString);

            return GetNext(schedule, startingPoint);
        }

        // Increments and matches the year
        private static bool MatchYear(ScheduleMatcher container, PointInTime point, bool increment = false)
        {
            if (increment)
                if (!container.Year.Increment())
                    return false;

            if (!container.Year.Matches())
                return MatchYear(container, point, true);

            return MatchMonth(container, point);
        }

        // Increments and matches the month
        private static bool MatchMonth(ScheduleMatcher container, PointInTime point, bool increment = false)
        {
            if (increment)
                if (!container.Month.Increment())
                    return MatchYear(container, point, true);

            if (!container.Month.Matches())
                return MatchMonth(container, point, true);

            return MatchDay(container, point);
        }

        // Increments and matches the day
        private static bool MatchDay(ScheduleMatcher container, PointInTime point, bool increment = false)
        {
            if (increment)
                if (!container.DayOfMonth.Increment())
                    return MatchMonth(container, point, true);

            if (!container.DayOfMonth.Matches() || !container.DayOfWeek.Matches())
                return MatchDay(container, point, true);

            return MatchHour(container, point);
        }

        // Increments and matches the hour
        private static bool MatchHour(ScheduleMatcher container, PointInTime point, bool increment = false)
        {
            if (increment)
                if (!container.Hour.Increment())
                    return MatchDay(container, point, true);

            if(!container.Hour.Matches())
                return MatchHour(container, point, true);

            return MatchMinute(container, point);
        }

        // Increments and matches the minute
        private static bool MatchMinute(ScheduleMatcher container, PointInTime point, bool increment = false)
        {
            if (increment)
                if (!container.Minute.Increment())
                    return MatchHour(container, point, true);

            if (!container.Minute.Matches())
                return MatchMinute(container, point, true);

            return true;
        }
    }
}
