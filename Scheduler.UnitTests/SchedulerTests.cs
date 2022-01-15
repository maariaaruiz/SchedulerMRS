using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerNegocio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.UnitTests
{
    [TestClass]
    public class SchedulerTests
    {
        #region Test Exception General
        [TestMethod]
        public void Scheduler_Exception_Not_Active_Return_Exception()
        {
            String MessageException = "Configuration must be active";
            ScheduleConfiguration Config = new() { Active = false };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Null_Configuration_Return_Exception()
        {
            String MessageException = "Scheduler configuration can not be empty";

            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.ValidateConfiguration(null));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Exception_Type_Not_Recognized_Return_Exception()
        {
            String MessageException = "The type is not recognized";
            ScheduleConfiguration Config = new()
            {
                Active = true
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        #endregion 


        #region Test Ocurrs Once
        [TestMethod]
        public void Scheduler_Once_01_10_2021_Returns_01_10_2021()
        {
            string expectedDescription = "Ocurrs once. Schedule will be" +
                " used at 15:30:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 10, 1, 15, 30, 00), actual.Value);
            Assert.AreEqual(expectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Once_Configuration_Date_Empty_Return_Exception()
        {
            String MessageException = "Configuration date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = (int)Types.Once,
                Configuration_date = null,
                Start_date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Once_Start_Date_Empty_Return_Exception()
        {
            String MessageException = "Start date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Ocurrs_type = Types.Once,
                Start_date = null
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }


        [TestMethod]
        public void Scheduler_Once_Out_Of_Limits_EndDate_ReturnException()
        {
            String MessageException = "Next execution time exceeds date limits";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = (int)Types.Once,
                Configuration_date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_date = new DateTime(2021, 09, 1, 15, 30, 00),
                End_date = new DateTime(2021, 09, 30, 15, 30, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(17, 00, 00)
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        #endregion


        #region Test Daily Exception

        [TestMethod]
        public void Scheduler_Exception_Daily_Recurring_Current_Date_Empty_Return_Exception()
        {
            String MessageException = "Current date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Frecuency = Frecuencys.Daily,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Current_date = null,
                Start_date = new DateTime(2021, 01, 01)
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Exception_Not_Daily_Not_Weekly_Recurring_Return_Exception()
        {
            String MessageException = "The frequency is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_frecuency = 2,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(05, 00, 00),
                Time_type = TimeTypes.Hours,
                Frecuency = null
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Exception_Daily_Recurring_Not_Time_Once_Not_Time_Every_Return_Exception()
        {
            String MessageException = "Must to select a daily frequency type 'Once' or 'Every'";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Exception_Daily_Recurring_Not_Start_Time_Return_Exception()
        {
            String MessageException = "The Horary Range is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Minutes,
                Time_frecuency = 10
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        #endregion


        #region Test Daily Recurring Once Time

        [TestMethod]
        public void Scheduler_Daily_Recurring_OnceTime_01_10_2021_Return_NextTime_01_10_2021()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be used" +
               " at 17:30:20 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(17, 30, 20)
            };
            ScheduleCalculator Calculator = new();

            var actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 10, 1, 17, 30, 20), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_OnceTime_CalculateSerie_Not_End_date()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be used" +
                " at 17:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(17, 00, 00)
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 1, 17, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 2, 17, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 3, 17, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 4, 17, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 5, 17, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Daily_Recurring_OnceTime_CalculateSerie_With_End_Time()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be used" +
                " at 17:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 4, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(17, 00, 00)
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 1, 17, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 2, 17, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 3, 17, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        #endregion


        #region Test Daily Recurring Every Time

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 2 hours between 00:00:00 and 04:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 2,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            DateTime? result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 10, 01, 00, 00, 00), result);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_Hours_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 1 hours between 00:00:00 and 04:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 01, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_Minutes_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 40 minutes between 12:10:00 and 14:00:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Minutes,
                Time_frecuency = 40,
                Star_time = new TimeSpan(12, 10, 00),
                End_time = new TimeSpan(14, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);
            Assert.AreEqual(result.Count, 5);
            Assert.AreEqual(new DateTime(2021, 10, 01, 12, 10, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 12, 50, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 13, 30, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 12, 10, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_Seconds_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 20 seconds between 12:00:00 and 12:00:59 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Seconds,
                Time_frecuency = 20,
                Star_time = new TimeSpan(12, 00, 00),
                End_time = new TimeSpan(12, 00, 59)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 01, 12, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 12, 00, 20), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 12, 00, 40), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 12, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 12, 00, 20), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 12, 00, 40), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_CalculateSerie_With_EndDate_In_Same_Day()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 1, 1, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 01, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Daily_Recurring_EveryTime_CalculateSerie_With_EndDate_In_Other_Day()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 2, 1, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 01, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 00, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        #endregion


        #region Test Weekly Exception

        [TestMethod]
        public void Scheduler_Exception_Weekly_Current_Date_Empty_Return_Exception()
        {
            String MessageException = "Current date can not be empty";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Current_date = null
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void Scheduler_Exception_Weekly_Frecuency_Negative_Return_Exception()
        {
            String MessageException = "The weekly frequency must be greater than 0";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Weekly_Time_Type_Not_Recognized_Return_Exception()
        {
            String MessageException = "Must indicate the type of frecuency Hours, Minutes o Seconds correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 1,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday }
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Weekly_Time_Frecuency_Zero_Return_Exception()
        {
            String MessageException = "The hourly frequency must be greater than 0";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 1,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Minutes,
                Time_frecuency = 0,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday }
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Weekly_Empty_TimeOnceExecution_Return_Exception()
        {
            String MessageException = "Must be indicate the daily time";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 2,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday }

            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Weekly_Empty_DaysActiveInWeek_Return_Exception()
        {
            String MessageException = "You must select at least one day of the week";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 2,
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(04, 00, 00)


            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void Scheduler_Exception_Weekly_EndTime_Less_StarTime_ReturnException()
        {
            String MessageException = "The Horary Range is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Star_time = new TimeSpan(05, 00, 00),
                End_time = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                           .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }
        [TestMethod]
        public void SdAmvia1qwwkc2ssfu0sy7c6qhr8e4curh64j8vglc0pz0mUSGUkQ6P1erDturnException()
        {
            String MessageException = "End date cant not be equal start date";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 2, 00, 00, 00),
                End_date = new DateTime(2021, 01, 2, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Star_time = new TimeSpan(01, 00, 00),
                End_time = new TimeSpan(08, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                           .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }


        #endregion


        #region Test Weekly Once Time
        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Get_Description_Without_ExcecutionDate_Return_EmptyDescription()
        {
            String ExpectedDescription = string.Empty;
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            string actualDescription = Calculator.GetDescriptionExecution(Config);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Monday_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Monday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 11, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Tuesday_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Tuesday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Tuesday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 12, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Wednesday_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Wednesday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Wednesday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 13, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Thursdays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Thursday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Thursday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 14, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Fridays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 15, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Saturdays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Saturday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Saturday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 02, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Thursdays_Start_Saturday_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Thursday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 2, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Thursday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 14, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Sundays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Saturday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2020, 12, 31, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Saturday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            DateTime? actual = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 02, 15, 00, 00), actual.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Sundays_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Sunday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2020, 12, 31, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Sunday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 03, 15, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 15, 00, 00), result[1]);
            Assert.AreEqual(actualDescription, ExpectedDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_One_Week_Sundays_Mondays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 1 weeks on Monday - Sunday. Schedule will be" +
                  " used at 1:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[2] { DayOfWeek.Monday, DayOfWeek.Sunday },
                Frecuency_weeks = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 03, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 01, 00, 00), result[1]);
            Assert.AreEqual(actualDescription, ExpectedDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_TwoWeeks_Mondays_Sundays_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Monday - Sunday. Schedule will be" +
                  " used at 1:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[2] { DayOfWeek.Monday, DayOfWeek.Sunday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 03, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 11, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 17, 01, 00, 00), result[2]);
            Assert.AreEqual(actualDescription, ExpectedDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Tuesday - Saturday - Sunday. Schedule will be" +
                 " used at 1:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[3] { DayOfWeek.Tuesday, DayOfWeek.Saturday, DayOfWeek.Sunday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new();

            DateTime? result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 2, 01, 00, 00), result.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_TwoWeeks_Fridays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                 " used at 1:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new();

            DateTime? result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(new DateTime(2021, 01, 15, 01, 00, 00), result.Value);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Mondays_Return_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Monday. Schedule will be" +
                  " used at 1:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 11, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 25, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 08, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 22, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_OnceTime_Two_Weeks_Wednesdays_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Wednesday. Schedule will be" +
                " used at 15:00:00 starting on 01/01/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(15, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Wednesday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 13, 15, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 27, 15, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        #endregion


        #region Test Weekly Every Time
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_GetDescription_Without_ExecutionDate_Return_Empty_Description()
        {
            String ExpectedDescription = string.Empty;
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00),
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2

            };
            ScheduleCalculator Calculator = new();

            string actualDescription = Calculator.GetDescriptionExecution(Config);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Fridays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                 " used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 2,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);


            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 00, 00), result);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Minutes_Wednesdays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Wednesday. Schedule will be" +
                 " used every 20 minutes between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Wednesday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Minutes,
                Time_frecuency = 20,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);


            Assert.AreEqual(new DateTime(2021, 01, 13, 02, 00, 00), result);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Seconds_Fridays_Return_NextDate()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Thursday. Schedule will be" +
                 " used every 20 seconds between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Thursday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Seconds,
                Time_frecuency = 20,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.GetResult(Config);
            string actualDescription = Calculator.GetDescriptionExecution(Config);


            Assert.AreEqual(new DateTime(2021, 01, 14, 02, 00, 00), result);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Mondays_Return_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Monday. Schedule will be" +
                 " used every 2 hours between 02:00:00 and 03:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Monday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 2,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(03, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 11, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 25, 02, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 11, 08, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Thursdays_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Thursday. Schedule will be" +
                 " used every 2 hours between 02:00:00 and 03:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Thursday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 2,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(03, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 14, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 28, 02, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 11, 11, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Fridays_Return_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                 " used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 2,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Minutes_Fridays_Return_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                 " used every 20 minutes between 02:00:00 and 03:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Minutes,
                Time_frecuency = 20,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(03, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 20, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 40, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Seconds_Fridays_Return_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Friday. Schedule will be" +
                 " used every 20 seconds between 02:00:00 and 02:00:20 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Friday },
                Frecuency_weeks = 2,
                Time_type = TimeTypes.Seconds,
                Time_frecuency = 20,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(02, 00, 20)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 01, 02, 00, 20), result[1]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 01, 15, 02, 00, 20), result[3]);
            Assert.AreEqual(new DateTime(2021, 01, 29, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Saturdays_With_EndDate_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 1 weeks on Saturday. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 1,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Saturday },
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 2, 01, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 02, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 01, 00, 00), result[1]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_StartTime_Before_CurrentDate()
        {
            String ExpectedDescription = "Ocurrs every 1 weeks on Saturday. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 29/09/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 09, 30, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 1,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Saturday },
                Start_date = new DateTime(2021, 09, 29, 00, 00, 00),
                End_date = new DateTime(2021, 10, 2, 01, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 02, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 01, 00, 00), result[1]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Tuesdays_With_End_Date_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 1 weeks on Tuesday. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 1,
                Days_active_week = new DayOfWeek[1] { DayOfWeek.Tuesday },
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 20, 01, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 05, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 05, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 00, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 19, 00, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 19, 01, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_Tuesdays_Wednesdays_Sundays_With_End_Date_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 3 weeks on Monday - Tuesday - Wednesday - Sunday. Schedule will be" +
                " used every 1 hours between 00:00:00 and 01:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 3,
                Days_active_week = new DayOfWeek[4] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Sunday },
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                End_date = new DateTime(2021, 10, 19, 0, 0, 0),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(00, 00, 00),
                End_time = new TimeSpan(01, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 03, 00, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 18, 00, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 18, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 19, 00, 00, 00), result[4]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Weekly_EveryTime_Hours_AllDaysInWeekActive_CalculateSerie()
        {
            String ExpectedDescription = "Ocurrs every 2 weeks on Monday - Tuesday - Wednesday " +
                "- Thursday - Friday - Saturday - Sunday. Schedule will be" +
                " used every 1 hours between 08:00:00 and 09:00:00 starting on 01/10/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Weekly,
                Frecuency_weeks = 2,
                Days_active_week = new DayOfWeek[7] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
                    DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday, DayOfWeek.Sunday },
                Start_date = new DateTime(2021, 10, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Time_type = TimeTypes.Hours,
                Time_frecuency = 1,
                Star_time = new TimeSpan(08, 00, 00),
                End_time = new TimeSpan(09, 00, 00)

            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 21);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(21, result.Count);
            Assert.AreEqual(new DateTime(2021, 10, 01, 08, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 10, 01, 09, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 08, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 09, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 08, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 09, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2021, 10, 11, 08, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2021, 10, 11, 09, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 08, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2021, 10, 12, 09, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2021, 10, 13, 08, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2021, 10, 13, 09, 00, 00), result[11]);
            Assert.AreEqual(new DateTime(2021, 10, 14, 08, 00, 00), result[12]);
            Assert.AreEqual(new DateTime(2021, 10, 14, 09, 00, 00), result[13]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 08, 00, 00), result[14]);
            Assert.AreEqual(new DateTime(2021, 10, 15, 09, 00, 00), result[15]);
            Assert.AreEqual(new DateTime(2021, 10, 16, 08, 00, 00), result[16]);
            Assert.AreEqual(new DateTime(2021, 10, 16, 09, 00, 00), result[17]);
            Assert.AreEqual(new DateTime(2021, 10, 17, 08, 00, 00), result[18]);
            Assert.AreEqual(new DateTime(2021, 10, 17, 09, 00, 00), result[19]);
            Assert.AreEqual(new DateTime(2021, 10, 25, 08, 00, 00), result[20]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        #endregion


        #region Test Monthly Exeptions

        [TestMethod]
        public void Scheduler_Monthly_Return_Exception_When_ActiveDays_Null()
        {
            String MessageException = "You must select at least one day of the week";       

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),             
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);

        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Exception_When_Actual_Week_Null()
        {
            String MessageException = "You must select First, Second, Third or Last";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,              
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            InvalidOperationException ex = Assert
                .ThrowsException<InvalidOperationException>(() => Calculator.GetResult(Config));

            Assert.AreEqual(MessageException, ex.Message);
        }

        #endregion


        #region Test Monthly Once Time Monday Tuesday Wednesday Thursday Friday Saturday or Sunday

        [TestMethod]
        public void Scheduler_Monthly_Return_First_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Monday of every 1 months." +
                " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 04, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 04, 05, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 05, 03, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Start_FirstMonday_Return_First_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Monday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 11, 01, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 1);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(2021, 11, 01, 01, 00, 00), result[0]); 
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Tuesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Tuesday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 1);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_First_Wednesda_OnceTimey()
        {
            string ExpectedDescription = "Ocurrs the First Wednesday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 02, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 03, 02, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 04, 06, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 05, 04, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 01, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Thursday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Thursday of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 06, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 03, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 07, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 05, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Friday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Friday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 07, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 04, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 01, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 06, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthky_Return_First_Saturday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Saturday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 05, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 03, 05, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 04, 02, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 05, 07, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 04, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Sunday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First Sunday of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 06, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 03, 06, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 04, 03, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 05, 01, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 05, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_LastMonday_OnceTime_Current_Start_In_Same_Month()
        {
            string ExpectedDescription = "Ocurrs the Last Monday of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_LastMonday_OnceTime_Current_Date_Start_Final_Of_Month()
        {
            string ExpectedDescription = "Ocurrs the Last Monday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 03, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2022, 04, 25, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 05, 30, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }



        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Monday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 10, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 14, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 11, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 09, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Tuesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Tuesday of every 1 months." +
            " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 11, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 08, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 08, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 12, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 10, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Wednesday()
        {
            string ExpectedDescription = "Ocurrs the Second Wednesday of every 1 months." +
          " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 12, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 09, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 13, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 11, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Thursday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Thursday of every 1 months." +
          " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 13, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 10, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 14, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 12, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Friday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Friday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 14, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 11, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 08, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 13, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Saturdar_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Saturday of every 1 months." +
          " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 08, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 12, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 09, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 14, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Sunday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second Sunday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 09, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 13, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 10, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Monday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 17, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 21, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 21, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 18, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 16, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Tuesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Tuesday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 18, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 15, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 19, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 17, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Wednesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Wednesday of every 1 months." +
        " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 19, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 16, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 16, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 20, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 18, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Thursday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Thursday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 20, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 17, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 17, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 21, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 19, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Friday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Friday of every 1 months." +
         " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 21, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 18, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 18, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 15, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 20, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Saturday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Saturday of every 1 months." +
        " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 15, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 19, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 16, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 21, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Sunday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third Sunday of every 1 months." +
        " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 16, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 20, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 17, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 15, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Monday of every 1 months." +
        " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 24, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 25, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 23, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Tuesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Tuesday of every 1 months." +
       " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 25, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 22, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 26, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 24, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Wednesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Wednesday of every 1 months." +
       " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 26, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 23, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 27, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 25, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Thursday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Thursday of every 1 months." +
       " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 27, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 24, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 28, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Friday_Once_Time()
        {
            string ExpectedDescription = "Ocurrs the Fourth Friday of every 1 months." +
       " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 28, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 25, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 22, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 27, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Saturday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Saturday of every 1 months." +
        " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 22, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 23, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 28, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Sunday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Sunday of every 1 months." +
       " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 23, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 24, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 22, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Last_Monday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Monday of every 1 months." +
            " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 25, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 30, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Tuesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Tuesday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 25, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 29, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 26, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 31, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Wednesday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Wednesday of every 1 months." +
           " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 26, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 30, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 27, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 25, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Thursday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Thursday of every 1 months." +
           " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 27, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 31, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 28, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Friday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Friday of every 1 months." +
           " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 28, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 25, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 29, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 27, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Saturday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Saturday of every 1 months." +
           " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 29, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 30, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 28, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Sunday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last Sunday of every 1 months." +
           " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 30, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 24, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 29, 01, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion


        #region Test Monthly Every Time  Monday Tuesday Wednesday Thursday Friday Saturday or Sunday
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Monday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Monday of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 04, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 02, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 04, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_First_Monday_With_End_date_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Monday of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021,01,15,00,00,00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 04, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 04, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Tuesday of every 1 months." +
             " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 01, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 01, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 03, 01, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 04, 05, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Wednesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Wednesday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 02, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 02, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 02, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 03, 02, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 04, 06, 02, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 04, 06, 04, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 05, 04, 02, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 05, 04, 04, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 06, 01, 02, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 06, 01, 04, 00, 00), result[9]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Thursday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Thursday of every 1 months." +
            " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 06, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 06, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 03, 02, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 03, 04, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 07, 02, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 04, 07, 04, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 05, 05, 02, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 05, 05, 04, 00, 00), result[9]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Friday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Friday of every 1 months." +
            " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 07, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 07, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 04, 02, 00, 00), result[4]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_First_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Saturday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 05, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 05, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 05, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 03, 05, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 04, 02, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Sunday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First Sunday of every 1 months." +
            " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 03, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 06, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 06, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 03, 06, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 03, 06, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 04, 03, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Monday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Monday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 10, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 10, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 14, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Monday_With_End_date_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Monday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date=new DateTime(2022,01,13,00,00,00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 10, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 10, 04, 00, 00), result[1]);           
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Tuesday of every 1 months." +
             " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 11, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 11, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 08, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 08, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 08, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Wednesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Wednesday of every 1 months." +
          " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 12, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 12, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 09, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Thurday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Thursday of every 1 months." +
             " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 13, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 13, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 10, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Friday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Friday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 14, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 14, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 11, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Saturday of every 1 months." +
          " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 08, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 08, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 12, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Sunday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second Sunday of every 1 months." +
           " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 09, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 09, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 13, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Monday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Monday of every 1 months." +
          " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 17, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 17, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 21, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 21, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 21, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Monday_With_End_Date_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Monday of every 1 months." +
          " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date= new DateTime(2022,02,10,00,00,00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 17, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 17, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Tuesday of every 1 months." +
         " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 18, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 18, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 15, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Wednesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Wednesday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 19, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 19, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 16, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 16, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 16, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Thursday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Thursday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 20, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 20, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 17, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 17, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 17, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Friday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Friday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 21, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 21, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 18, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 18, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 18, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Saturday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 15, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 15, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 19, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_monthly_Return_Third_Sunday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third Sunday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 16, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 16, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 20, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Monday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Monday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 24, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 24, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Monday_With_End_date_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Monday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date = new DateTime(2022, 03, 30, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 24, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 24, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Tuesday of every 1 months." +
         " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 25, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 25, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 22, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Wednesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Wednesday of every 1 months." +
         " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 26, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 26, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 23, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Thursday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Thursday of every 1 months." +
      " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 27, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 27, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 24, 02, 00, 00), result[4]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Friday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Friday of every 1 months." +
       " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 28, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 28, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 25, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Saturday of every 1 months." +
      " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 22, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 22, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Sunday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth Sunday of every 1 months." +
       " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 23, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 23, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Monday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Monday of every 1 months." +
       " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Monday_With_End_Date_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Monday of every 1 months." +
       " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date = new DateTime(2022, 03, 25, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Monday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 04, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Tuesday of every 1 months." +
        " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Tuesday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 25, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 25, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 29, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Return_Last_Wednesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Wednesday of every 1 months." +
      " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Wednesday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 26, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 26, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 23, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 30, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Thursday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Thursday of every 1 months." +
      " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Thursday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 27, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 27, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 24, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 31, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Friday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Friday of every 1 months." +
     " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Friday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 28, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 28, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 25, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 25, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Saturday of every 1 months." +
      " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Saturday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 29, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 29, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Sunday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last Sunday of every 1 months." +
            " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.Sunday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();


            var result = Calculator.CalculateSerie(Config, 5);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 30, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 30, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 02, 00, 00), result[4]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        #endregion


        #region Test Monthly Once Time - day
        [TestMethod]
        public void Scheduler_First_Day_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First day of every 2 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 2
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 03, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 05, 01, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_First_Day_With_EndDate_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First day of every 2 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 04, 8, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 2
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(2021, 03, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Second_Day_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second day of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 02, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Third_Day_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third day of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 03, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Fourth_Day_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth day of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 4, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 04, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Last_Day_OnceTIme()
        {
            string ExpectedDescription = "Ocurrs the Last day of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 01, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Last_Day_With_EndTime_OnceTIme()
        {
            string ExpectedDescription = "Ocurrs the Last day of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 02, 10, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 31, 01, 00, 00), result[0]);            
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion


        #region Test Monthly Every Time - day
        [TestMethod]
        public void Scheduler_First_Day_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First day of every 1 months." +
               " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 03, 01, 04, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_First_Day_With_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First day of every 1 months." +
               " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 03, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 01, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 01, 04, 00, 00), result[1]);           
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Second_Day_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 02, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 02, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Second_Day_With_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 03, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 02, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 02, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Third_Day_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third day of every 1 months." +
             " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 03, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 03, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Third_Day_Eith_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third day of every 1 months." +
             " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 02, 10, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 03, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 03, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Fourth_Day_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 4, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 04, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 03, 04, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Fourth_Day_With_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 4, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 02, 10, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 02, 04, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 02, 04, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Last_Day_EveryTIme()
        {
            string ExpectedDescription = "Ocurrs the Last day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 31, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 02, 28, 02, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Last_Day_With_EndDate_EveryTIme()
        {
            string ExpectedDescription = "Ocurrs the Last day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 01, 5, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                End_date = new DateTime(2021, 02, 10, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2021, 01, 31, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 01, 31, 04, 00, 00), result[1]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Last_Day_EveryTIme_NextYear()
        {
            string ExpectedDescription = "Ocurrs the Last day of every 1 months." +
              " Schedule will be used every 2 hours between 02:00:00 and 04:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 11 ,25 , 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Star_time = new TimeSpan(02, 00, 00),
                End_time = new TimeSpan(04, 00, 00),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Active_days_monthly = Days_Of_Week_Monthly.day,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config,6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2021, 11, 30, 02, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 11, 30, 04, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 12, 31, 02, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 12, 31, 04, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 02, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 04, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion


        #region Test Monthly Once Time - weekend day (Saturday and Sunday)
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 05, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 06, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 05, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 06, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 02, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 04, 03, 01, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 05, 01, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 06, 04, 01, 00, 00), result[9]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday_With_EndDate()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date = new DateTime(2022, 03, 3, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 05, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 06, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday_Sunday()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
                " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 07, 16, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 9);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(9, result.Count);
            Assert.AreEqual(new DateTime(2021, 08, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 09, 04, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 09, 05, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 10, 02, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2021, 10, 03, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2021, 11, 06, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2021, 11, 07, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2021, 12, 04, 01, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2021, 12, 05, 01, 00, 00), result[8]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday_Next_Year()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
                " Schedule will be used at 1:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 12, 01, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2021, 12, 04, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 12, 05, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[2]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_second_weekendday()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 08, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 09, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 12, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 13, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 09, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 04, 10, 01, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_NextMonth_When_CurrentDate_Later_Than_Second_Weekdenday()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 04, 22, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 05, 07, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 11, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 06, 12, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekendday_When_Current_Date_In_Same_Week()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
            " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 04, 07, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 04, 09, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 04, 10, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 05, 07, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 01, 00, 00), result[3]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Third_Weekendday()
        {
            string ExpectedDescription = "Ocurrs the Third weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 15, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 16, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Fourth_Weekendday()
        {
            string ExpectedDescription = "Ocurrs the Fourth weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 4);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 22, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 23, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Last_Weekendday()
        {
            string ExpectedDescription = "Ocurrs the Last weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";


            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 29, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 30, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 30, 01, 00, 00), result[6]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Last_Weekendday_Start_Last_Saturday_Last_DateMonth()
        {
            string ExpectedDescription = "Ocurrs the Last weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";


            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 04, 15, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2022, 04, 30, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 05, 28, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 05, 29, 01, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Last_Weekendday_Start_InLastSaturday()
        {
            string ExpectedDescription = "Ocurrs the Last weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";


            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 29, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 29, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 30, 01, 00, 00), result[1]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Last_Weekendday_Start_InSunday()
        {
            string ExpectedDescription = "Ocurrs the Last weekendday of every 1 months." +
             " Schedule will be used at 1:00:00 starting on 01/01/2022";


            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 2);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 30, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[1]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion


        #region Test Monthly Every Time - weekend day (Saturday and Sunday)
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 01, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 05, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 05, 03, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday__With_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date = new DateTime(2022, 02, 3, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 01, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 02, 03, 00, 00), result[3]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekendday_Next_Year_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekendday of every 1 months." +
            " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2021";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2021, 12, 01, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2021, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2021, 12, 04, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2021, 12, 04, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2021, 12, 05, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2021, 12, 05, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 01, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 01, 01, 03, 00, 00), result[5]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekendday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
          " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 08, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 08, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 09, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 09, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 12, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 02, 13, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 03, 12, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 03, 12, 03, 00, 00), result[9]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_NextMonth_When_CurrentDate_Later_Than_Second_Weekdenday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
          " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 04, 22, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 05, 07, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 05, 07, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 11, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 06, 11, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 06, 12, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 06, 12, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekendday_When_Current_Date_In_Same_Week_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekendday of every 1 months." +
          " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 04, 07, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 04, 09, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 04, 09, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 04, 10, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 10, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 05, 07, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 05, 07, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 05, 08, 03, 00, 00), result[7]);

            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Weekendday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third weekendday of every 1 months." +
          " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 15, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 15, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 16, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 16, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 19, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 02, 20, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Fourth_Weekendday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth weekendday of every 1 months." +
         " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 22, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 22, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 23, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 23, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Last_Weekendday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last weekendday of every 1 months." +
         " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";


            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekendday,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 14);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(14, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 29, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 29, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 30, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 30, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 26, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 02, 27, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 03, 26, 03, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 03, 27, 03, 00, 00), result[11]);
            Assert.AreEqual(new DateTime(2022, 04, 30, 01, 00, 00), result[12]);
            Assert.AreEqual(new DateTime(2022, 04, 30, 03, 00, 00), result[13]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion


        #region Test Monthly Once Time - weekend (Laboral Days (Monday at Friday))

        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekeend_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 05, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 06, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 06, 02, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 03, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 07, 01, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 08, 01, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 08, 02, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 08, 03, 01, 00, 00), result[6]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekeend_Start_In_Saturday_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
              " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 01, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 03, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 04, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 05, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 06, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 07, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 01, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 02, 01, 00, 00), result[6]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekeend_Start_In_Tuesday_Once_Time()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 02, 02, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 3);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 02, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 01, 00, 00), result[2]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekeend_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekend of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 05, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 12);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(12, result.Count);
            Assert.AreEqual(new DateTime(2022, 06, 06, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 06, 07, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 08, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 06, 09, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 10, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 07, 04, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 07, 05, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 07, 06, 01, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 07, 07, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 07, 08, 01, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 08, 08, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 08, 09, 01, 00, 00), result[11]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }


        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekend_When_Current_Date_In_Same_Week_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekend of every 1 months." +
               " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 02, 08, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 12);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(12, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 08, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 07, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 08, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 03, 09, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 03, 10, 01, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 03, 11, 01, 00, 00), result[8]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Weekeend_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Third weekend of every 1 months." +
                  " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 17, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 18, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 19, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 20, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 21, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 01, 00, 00), result[6]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Weekeend_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth weekend of every 1 months." +
                  " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 24, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 25, 01, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 26, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 27, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 28, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 21, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 22, 01, 00, 00), result[6]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Weekeend_OnceTime()
        {
            string ExpectedDescription = "Ocurrs the Last weekend of every 1 months." +
                  " Schedule will be used at 1:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.OnceTime,
                Time_once_frecuency = new TimeSpan(01, 00, 00),
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 7);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(7, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 01, 00, 00), result[1]);/*
            Assert.AreEqual(new DateTime(2022, 03, 28, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 03, 29, 01, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 30, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 31, 01, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 25, 01, 00, 00), result[6]);*/
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        #endregion

        #region Test Monthly Every Time - weekend (Laboral Days (Monday - Friday)
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekeend_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
            " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 05, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(10, result.Count);
            Assert.AreEqual(new DateTime(2022, 06, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 06, 01, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 02, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 06, 02, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 03, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 06, 03, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 07, 01, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 07, 01, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 08, 01, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 08, 01, 03, 00, 00), result[9]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekeend_With_EndDate_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
            " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 05, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                End_date = new DateTime(2022, 06, 15, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 10);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2022, 06, 01, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 06, 01, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 02, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 06, 02, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 03, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 06, 03, 03, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }
        [TestMethod]
        public void Scheduler_Monthly_Return_First_Weekeend_Start_In_Saturday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
            " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 01, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 14);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(14, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 03, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 03, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 04, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 04, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 05, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 01, 05, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 01, 06, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 01, 06, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 01, 07, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 01, 07, 03, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 02, 01, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 02, 01, 03, 00, 00), result[11]);
            Assert.AreEqual(new DateTime(2022, 02, 02, 01, 00, 00), result[12]);
            Assert.AreEqual(new DateTime(2022, 02, 02, 03, 00, 00), result[13]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekeend_Start_In_Tuesday_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the First weekend of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 02, 02, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.First,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 6);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(6, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 02, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 02, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 03, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 04, 03, 00, 00), result[5]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekeend_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekend of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 05, 30, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 12);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(12, result.Count);
            Assert.AreEqual(new DateTime(2022, 06, 06, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 06, 06, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 06, 07, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 06, 07, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 06, 08, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 06, 08, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 06, 09, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 06, 09, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 06, 10, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 06, 10, 03, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 07, 04, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 07, 04, 03, 00, 00), result[11]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }


        [TestMethod]
        public void Scheduler_Monthly_Return_Second_Weekend_When_Current_Date_In_Same_Week_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Second weekend of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 02, 08, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Second,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 12);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(12, result.Count);
            Assert.AreEqual(new DateTime(2022, 02, 08, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 02, 08, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 09, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 02, 10, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 02, 11, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 03, 07, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 03, 07, 03, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 03, 08, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 03, 08, 03, 00, 00), result[11]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Third_Weekeend_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Third weekend of every 1 months." +
            " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Third,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 14);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(14, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 17, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 17, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 18, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 18, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 19, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 01, 19, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 01, 20, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 01, 20, 03, 00, 00), result[7]);
            Assert.AreEqual(new DateTime(2022, 01, 21, 01, 00, 00), result[8]);
            Assert.AreEqual(new DateTime(2022, 01, 21, 03, 00, 00), result[9]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 01, 00, 00), result[10]);
            Assert.AreEqual(new DateTime(2022, 02, 14, 03, 00, 00), result[11]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 01, 00, 00), result[12]);
            Assert.AreEqual(new DateTime(2022, 02, 15, 03, 00, 00), result[13]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Fourth_Weekeend_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Fourth weekend of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Fourth,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 24, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 24, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 01, 25, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 01, 25, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 01, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 01, 26, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 01, 27, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 01, 27, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);
        }

        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Weekeend_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last weekend of every 1 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 1
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 03, 00, 00), result[1]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 02, 28, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 03, 28, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 03, 29, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 03, 29, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);

        }
        [TestMethod]
        public void Scheduler_Monthly_Return_Last_Weekeend_Every_3_Months_EveryTime()
        {
            string ExpectedDescription = "Ocurrs the Last weekend of every 3 months." +
             " Schedule will be used every 2 hours between 01:00:00 and 03:00:00 starting on 01/01/2022";

            ScheduleConfiguration Config = new()
            {
                Active = true,
                Ocurrs_type = Types.Recurring,
                Current_date = new DateTime(2022, 01, 06, 00, 00, 00),
                Frecuency = Frecuencys.Monthly,
                Start_date = new DateTime(2022, 01, 1, 00, 00, 00),
                Daily_frecuency = DailyFrencuencys.EveryTime,
                Star_time = new TimeSpan(01, 0, 0),
                End_time = new TimeSpan(03, 0, 0),
                Time_frecuency = 2,
                Time_type = TimeTypes.Hours,
                Active_days_monthly = Days_Of_Week_Monthly.weekend,
                Actual_week = WeeksInMonth.Last,
                Frecuency_months = 3
            };
            ScheduleCalculator Calculator = new();

            var result = Calculator.CalculateSerie(Config, 8);
            string actualDescription = Calculator.GetDescriptionExecution(Config);

            Assert.AreEqual(8, result.Count);
            Assert.AreEqual(new DateTime(2022, 01, 31, 01, 00, 00), result[0]);
            Assert.AreEqual(new DateTime(2022, 01, 31, 03, 00, 00), result[1]);            
            Assert.AreEqual(new DateTime(2022, 04, 25, 01, 00, 00), result[2]);
            Assert.AreEqual(new DateTime(2022, 04, 25, 03, 00, 00), result[3]);
            Assert.AreEqual(new DateTime(2022, 04, 26, 01, 00, 00), result[4]);
            Assert.AreEqual(new DateTime(2022, 04, 26, 03, 00, 00), result[5]);
            Assert.AreEqual(new DateTime(2022, 04, 27, 01, 00, 00), result[6]);
            Assert.AreEqual(new DateTime(2022, 04, 27, 03, 00, 00), result[7]);
            Assert.AreEqual(ExpectedDescription, actualDescription);

        }
        #endregion
    }
}
//exception no indicar actual_date
