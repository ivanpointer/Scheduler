/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using System;
using System.Collections.Generic;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Represents a point in time, being built from a DateTime, ratining the timezone of the original DateTime
    /// Converts DayOfWeek to an integer value for processing purposes.
    /// 
    /// This allows a "ByRef" relationship for the engine.
    /// </summary>
    public class PointInTime
    {
        // A map for converting the DayOfWeek enum values into integers
        private static readonly IDictionary<DayOfWeek, int> DayOfWeekValue = new Dictionary<DayOfWeek, int>
        {
            { DayOfWeek.Sunday, 1 },
            { DayOfWeek.Monday, 2 },
            { DayOfWeek.Tuesday, 3 },
            { DayOfWeek.Wednesday, 4 },
            { DayOfWeek.Thursday, 5 },
            { DayOfWeek.Friday, 6 },
            { DayOfWeek.Saturday, 7 }
        };

        /// <summary>
        /// The year of this PointInTime
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// The Month of this PointInTime
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// The Day of this PointInTime
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// The int value of the DayOfWeek of this PointInTime
        /// </summary>
        public int WeekDay
        {
            get
            {
                return DayOfWeekValue[new DateTime(Year, Month, Day).DayOfWeek];
            }
        }
        /// <summary>
        /// The Hour of this PointInTime
        /// </summary>
        public int Hour { get; set; }
        /// <summary>
        /// The Minute of this PointInTime
        /// </summary>
        public int Minute { get; set; }

        // The DateTime that this PointInTime was built from
        //  This is retained for TimeZone purposes
        private DateTime DateTime { get; set; }

        /// <summary>
        /// Creates a new PointInTime from the given DateTime
        /// </summary>
        /// <param name="dateTime">The DateTime from which to create this PointInTime</param>
        public PointInTime(DateTime dateTime)
        {
            DateTime = dateTime;

            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
        }

        /// <summary>
        /// Converts this PointInTime back into a DateTime, retaining the timezone information from the
        /// original DateTime this PointInTime was built from
        /// </summary>
        /// <returns>A DateTime representation of this PointInTime</returns>
        public DateTime ToDateTime()
        {
            return new DateTime(Year, Month, Day, Hour, Minute, 0, 0, DateTime.Kind);
        }

        /// <summary>
        /// Advances the year to the given year, resetting all lower values to their earliest value
        /// </summary>
        /// <param name="year">The year to advance to</param>
        public void AdvanceYear(int year)
        {
            Year = year;
            Month = 1;
            Day = 1;
            Hour = 0;
            Minute = 0;
        }

        /// <summary>
        /// Advances the month to the given month, resetting all lower values to their earliest value
        /// </summary>
        /// <param name="month">The month to advance to</param>
        public void AdvanceMonth(int month)
        {
            Month = month;
            Day = 1;
            Hour = 0;
            Minute = 0;
        }

        /// <summary>
        /// Advances the day to the given day, resetting all lower values to their earliest value
        /// </summary>
        /// <param name="day">The day to advance to</param>
        public void AdvanceDay(int day)
        {
            Day = day;
            Hour = 0;
            Minute = 0;
        }

        /// <summary>
        /// Advances the hour to the given hour, resetting all lower values to their earliest value
        /// </summary>
        /// <param name="hour">The hour to advance to</param>
        public void AdvanceHour(int hour)
        {
            Hour += hour;
            Minute = 0;
        }

        /// <summary>
        /// Advances the minute to the given minute
        /// </summary>
        /// <param name="minute">The minute to advance to</param>
        public void AdvanceMinute(int minute)
        {
            Minute += minute;
        }
    }
}
