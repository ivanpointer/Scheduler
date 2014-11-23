/*
 * (C)2014 Ivan Andrew Pointer (ivan@pointerplace.us)
 * Date: 11/24/2014
 * License: Apache License 2 (https://github.com/ivanpointer/Scheduler/blob/master/LICENSE)
 * GitHub: https://github.com/ivanpointer/Scheduler
 */

using PointerPlace.Scheduler.Matchers;

namespace PointerPlace.Scheduler
{
    /// <summary>
    /// A struct for containing all of the matchers for the six matchers for the engine
    /// </summary>
    public struct ScheduleMatcher
    {
        public MatcherBase Minute, Hour, DayOfMonth, DayOfWeek, Month, Year;
    }
}
