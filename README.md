## Scheduler
Scheduler is a small and fast utility that takes a schedule and determines the next DateTime that matches the schedule, from a given starting DateTime.  The current release only includes a cron style schedule parser, but the engine is flexible enough to allow for other ways of defining these schedules. The strategy used by this scheduler is such:

 * Schedules are broken into a series of schedule entries.  These entries can be defined as a series of values, or ranges, and an interval.  The series of values are further refined by the interval - I.E. the value must be evenly divisible by the interval to be included.
 * Schedules consist of schedule entries for minute, hour, day of month, month and day of week.
 * The engine searches on the largest values first, then checking each smaller value, only checking valid values for each schedule entry.  For example, the engine first iterates over the months until it finds a valid month, then over the days, hours and minutes in that order, allowing for a very fast resolution of the schedule.

## Cron Schedules
Cron schedules allow for defining advanced schedules down to the minute.  This style of scheduling is used predominately in Linux environments, but is a very powerful and simple method of specifying a schedule.  The format for a cron schedule is shown here:

                    *    *    *    *    *
                    │    │    │    │    │
                    │    │    │    │    │
                    │    │    │    │    │
                    │    │    │    │    └───── Day of Week (1 - 7) (Sunday = 1)
                    │    │    │    └────────── Month (1 - 12)
                    │    │    └─────────────── Day of Month (1 - 31)
                    │    └──────────────────── Hour (0 - 23)
                    └───────────────────────── Minute (0 - 59)
  
### Special Characters
The special characters allowed in the schedule entries for the schedule include:  
* Asterisk ( * )
     The asterisk indicates that the expression matches for all values of the
     field, for example, using an asterisk in the 4th field (month) indicates
     every month.
* Slash ( / )
     Slashes describe increments of ranges.  For example 0/15 in the 1st field
     (minutes) indicates the 0th (top) minute of the hour and every fifteen
     minutes thereafter.
* Comma ( , )
     Commas are used to separate items of a list.  For example, using
     "MON,WED,FRI" in the 5th field (day of week) means Mondays, Wednesdays and
     Fridays.
* Hyphen ( - )
     Hyphens define ranges.  For example, 8-10 indicates every month between
     August and October, inclusive.

### Sample Schedules
`30 0 1 1,6,12 *` - 00:30 on 1st of Jan, Jun & Dec  
`0 20 * 10 1-5` - 20:00 every weekday in Oct  
`0 0 1,10,15 * *` - Midnight on 1st, 10th & 15th of month  
`5,10 0 10 * 1` - At 00:05 and 00:10 every 10th of the month that is also a Monday  
`*/15 0-6/2 * MON-FRI` - Every 15 minutes on even hours between 12 and 6 AM on weekdays  

## Usage
Using the Scheduler is quite simple.  Let's just dig into some samples:

### Example 1
Here we parse a schedule ahead of time.  This is helpful if you are reusing a schedule.  This is the suggested way:

    Schedule schedule = ScheduleParser.ParseSchedule("*/15 0-6/2 * * MON-FRI");
    DateTime example1 = Scheduler.GetNext(schedule);
    Console.WriteLine(String.Format("Example 1: {0}", example1));

The result:

    Example 1: 11/24/2014 12:00:00 AM

### Example 2
If you are in a bit more of a hurry, a shortcut is included on the Scheduler:

    DateTime example2 = Scheduler.GetNext("*/7 * * * *");
    Console.WriteLine(String.Format("Example 2: {0}", example2));

The result:

    Example 2: 11/22/2014 4:56:00 PM

## NuGet
The package will be released to NuGet as "PointerPlace.Scheduler"

**Enjoy!**
