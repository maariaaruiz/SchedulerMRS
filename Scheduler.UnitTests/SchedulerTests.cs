using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerNegocio;
using System;
using System.Text;

namespace Scheduler.UnitTests
{
    [TestClass]
    public class SchedulerTests
    {
        [TestMethod]
        public void GetNextDate_NotActive_ReturnException()
        {
            String MessageException = "Configuration must be active";
            ScheduleConfiguration Config = new() { Active = false };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetResult());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_TypeNotRecognized_ReturnException()
        {
            String MessageException = "The type is not recognized";
            ScheduleConfiguration Config = new() { Active = true, Type = -1 };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetResult());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Once_01_10_2021_Returns_01_10_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 10, 1, 15, 30, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }

        [TestMethod]
        public void GetNextDate_Once_ConfigurationDateEmpty_ReturnException()
        {
            String MessageException = "Configuration date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_date = null,
                Start_date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckConfiguration());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_StarDateEmpty_ReturnException()
        {
            String MessageException = "Start date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = null
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckConfiguration());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Once_OutOfLimitStarDate_ReturnException()
        {
            String MessageException = "Next execution time exceeds date limits";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 10, 5, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetResult());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Once_OutOfLimitsEndDate_ReturnException()
        {
            String MessageException = "Next execution time exceeds date limits";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 09, 1, 15, 30, 00),
                End_date = new DateTime(2021, 09, 30, 15, 30, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(17, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetResult());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Recurring_CurrentDateEmpty_ReturnException()
        {
            String MessageException = "Current date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = null
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckConfiguration());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Recurring_01_10_2021_Frecuency_2_Return_02_10_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 10, 2, 17, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(17, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }


        [TestMethod]
        public void GetNextDate_Recurring_Frecuency_NotDailyOrWeekly_ReturnExeption()
        {
            String MessageException = "The frequency is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Frecuency = "Monthly"
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Recurring_NotTimeOnce_NotTimeEvery_ReturnException()
        {
            String MessageException = "Must to select a daily frequency type 'Once' or 'Every'";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Time_type = TimeTypes.Hours.ToString(),
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void GetNextDate_Recurring_Weekly_FrecuencyNegative_ReturnException()
        {
            String MessageException = "The weekly frequency must be greater than 0";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Frecuency_weeks = -6,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = TimeTypes.Hours.ToString(),
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void GetNextDate_Recurring_Weekly_TimeTypeNotRecognized_ReturnException()
        {
            String MessageException = "Must indicate the type of frecuency Hours, Minutes o Seconds correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Frecuency_weeks = 1,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = "Miles",
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday }

            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void GetNextDate_Recurring_Weekly_TimeFrecuencyZero_ReturnException()
        {
            String MessageException = "The hourly frequency must be greater than 0";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Frecuency_weeks = 1,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = TimeTypes.Minutes.ToString(),
                Time_frecuency = 0,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Days_active_week= new DayOfWeek[1] {DayOfWeek.Monday}
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void GetNextDate_Recurring_Daily_NotEndTime_ReturnException()
        {
            String MessageException = "The Horary Range is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = TimeTypes.Minutes.ToString(),
                Time_frecuency = 10,
                Star_time = new TimeSpan(00, 00, 00)

            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDescription_Once_ReturnDescription()
        {
            String ExpectedDescription = "Ocurrs once. Schedule will be" +
                " used on 01/10/2021 at 15:30:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }

        [TestMethod]
        public void GetNextDescription_RecurringDaily_ReturnDesciption()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used on 02/10/2021 every 1 hours between 00:00:00 and 04:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = TimeTypes.Hours.ToString(),
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }

        [TestMethod]
        public void GetNextDate_Recurring_OneWeek_01_01_2021_Check_Sunday_Monday_Return_03_01_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 01, 3, 01, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(01,00,00),
                Days_active_week = new DayOfWeek[2] { DayOfWeek.Monday, DayOfWeek.Sunday },
                Frecuency_weeks=1
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }
        [TestMethod]
        public void GetNextDate_Recurring_TwoWeeks_01_01_2021_Check_Tuesday_Saturday_Sunday_Return_02_01_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 01, 2, 01, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[3] { DayOfWeek.Tuesday,DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }
        [TestMethod]
        public void GetNextDate_Recurring_TwoWeeks_01_01_2021_ActualDay_Friday_Check_Friday_Return_15_01_2021_Friday()
        {
            DateTime? DateExpected = new DateTime(2021, 01, 15, 01, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday},
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }
        [TestMethod]
        public void GetNextDate_Recurring_TwoWeeks_01_01_2021_Check_Monday_Return_11_01_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 01, 11, 01, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }

        [TestMethod]
        public void GetNextDescription_Recurring_Daily_EveryTime()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used on 02/10/2021 every 2 hours between 00:00:00 and 04:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Time_type = TimeTypes.Hours.ToString(),
                Time_frecuency = 2,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }
        [TestMethod]
        public void GetNextDescription_Recurring_Daily_OnceTime()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used on 02/10/2021 at 0:50:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
               Time_once_frecuency = new TimeSpan(00,50,00),
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }

        [TestMethod]
        public void GetNextDescription_Recurring_Weekly_OnceTime()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                " used on 15/01/2021 at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Daily_frecuency = DailyFrencuencys.OnceTime.ToString(),
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }
        [TestMethod]
        public void GetNextDescription_Recurring_Weekly_EveryTime()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                " used on 15/01/2021 every 2 hours between 00:00:00 and 04:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly.ToString(),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime.ToString(),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Hours.ToString(),
                Time_frecuency = 2,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetResult();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }
    }
}
