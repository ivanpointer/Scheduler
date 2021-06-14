/*
 * © 2014-2021 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014, 06/14/2021
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// Parses a cron style schedule string into a Schedule so that the engine
    /// can determine the next value in the schedule.
    /// </summary>
    /// <seealso cref="PointerPlace.Scheduler.IScheduleParser" />
    public class ScheduleParser : IScheduleParser
    {
        // A map for converting a string value of the day of week, into its associated integer value
        private static readonly IDictionary<string, int> DayOfWeekMap = new Dictionary<string, int>
        {
            { "SUN", 1 },
            { "MON", 2 },
            { "TUE", 3 },
            { "WED", 4 },
            { "THU", 5 },
            { "FRI", 6 },
            { "SAT", 7 }
        };

        /// <summary>
        /// Parses the provided schedule string into a Schedule
        /// </summary>
        /// <param name="scheduleString">The schedule string to parse into a Schedule</param>
        /// <returns>The Schedule that was parsed from the provided schedule string</returns>
        public Schedule ParseSchedule(string scheduleString)
        {
            if (String.IsNullOrEmpty(scheduleString) == false)
            {
                var scheduleParts = Regex.Split(scheduleString.Trim().ToUpper(), "\\s");

                if (scheduleParts.Length == 5)
                {
                    return new Schedule
                    {
                        Minute = ParseScheduleEntry(scheduleParts[0]),
                        Hour = ParseScheduleEntry(scheduleParts[1]),
                        DayOfMonth = ParseScheduleEntry(scheduleParts[2]),
                        Month = ParseScheduleEntry(scheduleParts[3]),
                        DayOfWeek = ParseScheduleEntry(scheduleParts[4])
                    };
                }
                else
                {
                    throw new ImpossibleScheduleException("Illegal schedule string");
                }
            }
            else
            {
                throw new ImpossibleScheduleException("Cannot parse empty schedule string");
            }
        }

        // Parses the given schedule entry
        private static ScheduleEntry ParseScheduleEntry(string scheduleEntry)
        {
            // Look for and handle an interval
            var slashIndex = scheduleEntry.IndexOf("/");
            string valuesPart;
            string intervalPart;
            if (slashIndex != -1)
            {
                valuesPart = scheduleEntry.Substring(0, slashIndex);
                intervalPart = scheduleEntry.Substring(slashIndex + 1);
            }
            else
            {
                valuesPart = scheduleEntry;
                intervalPart = null;
            }

            int interval;
            if (intervalPart != null)
            {
                if (IsNumeric(intervalPart))
                {
                    interval = Convert.ToInt32(intervalPart);
                }
                else
                {
                    throw new ImpossibleScheduleException("Illegal schedule string");
                }
            }
            else
            {
                interval = 0;
            }

            // Look for and handle a list of values
            ScheduleEntryValue[] scheduleEntryValues;
            string[] values = valuesPart.Split(',');
            if (values.Length != 0)
            {
                scheduleEntryValues = new ScheduleEntryValue[values.Length];
                for (int lp = 0; lp < values.Length; lp++)
                    scheduleEntryValues[lp] = ParseScheduleEntryValue(values[lp]);
            }
            else
            {
                throw new ImpossibleScheduleException("Illegal schedule string");
            }

            // compile the values and interval together and return them
            return new ScheduleEntry
            {
                Values = scheduleEntryValues,
                Interval = interval
            };
        }

        // Parses out a single schedule entry value
        private static ScheduleEntryValue ParseScheduleEntryValue(string scheduleEntryValue)
        {
            // Check to see if the value is a wildcard entry
            if (scheduleEntryValue == "*")
            {
                return new ScheduleEntryValue();
            }
            // Check to see if the schedule entry is a range value
            else
            {
                var dashIndex = scheduleEntryValue.IndexOf("-");

                // This is just a single entry
                if (dashIndex == -1)
                {
                    var value = ParseScheduleEntryValueInner(scheduleEntryValue);
                    return new ScheduleEntryValue
                    {
                        Value = value,
                        Threshold = value
                    };
                }
                // This is a range
                else
                {
                    var lowerValueString = scheduleEntryValue.Substring(0, dashIndex);
                    var upperValueString = scheduleEntryValue.Substring(dashIndex + 1);

                    var lowerValue = ParseScheduleEntryValueInner(lowerValueString);
                    var upperValue = ParseScheduleEntryValueInner(upperValueString);

                    if (lowerValue <= upperValue)
                    {
                        return new ScheduleEntryValue
                        {
                            Value = lowerValue,
                            Threshold = upperValue
                        };
                    }
                    else
                    {
                        throw new ArgumentException("Illegal schedule string");
                    }
                }
            }
        }

        // Internal helper for converting a singluar value of a schedule entry value to its integer value, this includes handling days of week in string format "SAT" - "SUN"
        private static int ParseScheduleEntryValueInner(string scheduleEntryValueInner)
        {
            if (IsNumeric(scheduleEntryValueInner))
            {
                return Convert.ToInt32(scheduleEntryValueInner);
            }
            else
            {
                var key = scheduleEntryValueInner.ToUpper();
                if (DayOfWeekMap.ContainsKey(key))
                {
                    return DayOfWeekMap[key];
                }
                else
                {
                    throw new ArgumentException("Illegal schedule string");
                }
            }
        }

        // Checks if the given value is numeric, forces a bit of a strict format
        private static bool IsNumeric(string check)
        {
            return Regex.IsMatch(check, "^\\d*$");
        }
    }
}