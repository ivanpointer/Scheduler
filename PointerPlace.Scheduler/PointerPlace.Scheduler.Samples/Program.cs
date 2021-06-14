using System;

namespace PointerPlace.Scheduler.Samples
{
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            // Setup
            var scheduler = new Scheduler();
            var scheduleParser = new ScheduleParser();

            // Example 1
            Schedule schedule = scheduleParser.ParseSchedule("*/15 0-6/2 * * MON-FRI");
            DateTime example1 = scheduler.GetNext(schedule);
            Console.WriteLine(string.Format("Example 1: {0}", example1));

            // Example 2
            DateTime example2 = scheduler.GetNext("*/7 * * * *");
            Console.WriteLine(string.Format("Example 2: {0}", example2));

            // Pause and exit
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();
        }
    }
}