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
            ScheduleConfiguration Config = new() { Active = true
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
                Start_date=new DateTime(2021,01,01)
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
                Time_type=TimeTypes.Hours,
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
                End_date = new DateTime(2021,01,2,00,00,00),
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

    }

}
