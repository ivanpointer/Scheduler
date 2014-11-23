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
    /// Indicates that a schedule is impossible.  In other-words, either the schedule cannot be parsed,
    /// or there is no possible "next date" for the schedule.
    /// </summary>
    class ImpossibleScheduleException : Exception
    {
        public ImpossibleScheduleException()
            : base() { }

        public ImpossibleScheduleException(string message)
            : base(message) { }
    }
}
