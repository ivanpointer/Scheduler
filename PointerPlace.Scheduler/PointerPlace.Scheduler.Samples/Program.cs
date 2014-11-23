using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointerPlace.Scheduler.Samples
{
    class Program
    {
        static void Main(string[] args)
        {

            // Example 1
            Schedule schedule = ScheduleParser.ParseSchedule("*/15 0-6/2 * * MON-FRI");
            DateTime example1 = Scheduler.GetNext(schedule);
            Console.WriteLine(String.Format("Example 1: {0}", example1));

            // Example 2
            DateTime example2 = Scheduler.GetNext("*/7 * * * *");
            Console.WriteLine(String.Format("Example 2: {0}", example2));

            // Pause and exit
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

        }
    }
}
