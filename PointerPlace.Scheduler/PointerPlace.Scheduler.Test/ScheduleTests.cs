using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace PointerPlace.Scheduler.Test
{
    [TestClass]
    public class ScheduleTests
    {

        [TestMethod]
        public void TestHourRollover()
        {
            var point = new DateTime(2014, 11, 22, 13, 52, 37);

            point = Scheduler.GetNext("*/15 * * * *", point);

            Assert.AreEqual(2014, point.Year);
            Assert.AreEqual(11, point.Month);
            Assert.AreEqual(22, point.Day);
            Assert.AreEqual(14, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

        [TestMethod]
        public void TestDayRollover()
        {
            var point = new DateTime(2014, 11, 22, 20, 52, 37);

            point = Scheduler.GetNext("* */8 * * *", point);

            Assert.AreEqual(2014, point.Year);
            Assert.AreEqual(11, point.Month);
            Assert.AreEqual(23, point.Day);
            Assert.AreEqual(0, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

        [TestMethod]
        public void TestMonthRollover()
        {
            var point = new DateTime(2014, 11, 29, 20, 52, 37);

            point = Scheduler.GetNext("* * */7 * *", point);

            Assert.AreEqual(2014, point.Year);
            Assert.AreEqual(12, point.Month);
            Assert.AreEqual(7, point.Day);
            Assert.AreEqual(0, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

        [TestMethod]
        public void TestYearRollover()
        {
            var point = new DateTime(2014, 11, 29, 20, 52, 37);

            point = Scheduler.GetNext("* * * */5 *", point);

            Assert.AreEqual(2015, point.Year);
            Assert.AreEqual(5, point.Month);
            Assert.AreEqual(1, point.Day);
            Assert.AreEqual(0, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

        [TestMethod]
        public void TestLeapYear()
        {
            var point = new DateTime(2017, 11, 29, 20, 52, 37);

            point = Scheduler.GetNext("0 0 29 2 *", point);

            Assert.AreEqual(2020, point.Year);
            Assert.AreEqual(2, point.Month);
            Assert.AreEqual(29, point.Day);
            Assert.AreEqual(0, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

        [TestMethod]
        public void TestDayOfWeekNomenclature()
        {
            var point = new DateTime(2014, 11, 29, 20, 52, 37);

            point = Scheduler.GetNext("0 0 * * WED", point);

            Assert.AreEqual(2014, point.Year);
            Assert.AreEqual(12, point.Month);
            Assert.AreEqual(3, point.Day);
            Assert.AreEqual(0, point.Hour);
            Assert.AreEqual(0, point.Minute);
            Assert.AreEqual(0, point.Second);
            Assert.AreEqual(0, point.Millisecond);
        }

    }
}
