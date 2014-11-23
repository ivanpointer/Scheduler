/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// A struct for wrapping the five ScheduleEntry instances representing the full schedule
    /// </summary>
    public struct Schedule
    {
        public ScheduleEntry Minute, Hour, DayOfMonth, Month, DayOfWeek;
    }
}
