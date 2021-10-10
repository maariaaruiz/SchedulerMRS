using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchedulerNegocio;
using System;

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

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetNextDateExecution());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_TypeNotRecognized_ReturnException()
        {
            String MessageException = "The type is not recognized";
            ScheduleConfiguration Config = new() { Active = true, Type = -1 };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetNextDateExecution());

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
                Configuration_Date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_Date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetNextDateExecution();
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
                Configuration_Date = null
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
                Configuration_Date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_Date = null
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
                Configuration_Date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_Date = new DateTime(2021, 10, 5, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetNextDateExecution());

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
                Configuration_Date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_Date = new DateTime(2021, 09, 1, 15, 30, 00),
                End_Date = new DateTime(2021, 09, 30, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.GetNextDateExecution());

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
                Current_Date = null
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckConfiguration());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Recurring_01_10_2021_Frecuency_2_Return_03_10_2021()
        {
            DateTime? DateExpected = new DateTime(2021, 10, 3, 00, 00, 00);
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_Date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Frecuency_Days = 2,
                Start_Date = new DateTime(2021, 10, 1, 00, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetNextDateExecution();
            DateTime? actual = Calculator.Next_Execution_Time;

            Assert.AreEqual(DateExpected.Value, actual.Value);
        }

        [TestMethod]
        public void GetNextDate_Recurrng_FrecuencyDays_Negative_ReturnException()
        {
            String MessageException = "The frequency must be greater than 0";
            ScheduleConfiguration Config = new() { Frecuency_Days = -2 };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDate_Recurring_Frecuency_NotDaily_ReturnExeption()
        {
            String MessageException = "The frequency is not set correctly";
            ScheduleConfiguration Config = new()
            {
                Frecuency_Days = 1,
                Frecuency = "Monthly"
            };
            ScheduleCalculator Calculator = new(Config);

            Exception ex = Assert.ThrowsException<Exception>(() => Calculator.CheckFrecuency());

            Assert.AreEqual(MessageException, ex.Message);
        }

        [TestMethod]
        public void GetNextDescription_Once_ReturnDescription()
        {
            String ExpectedDescription = "Ocurrs once. Schedule will be" +
                " used on 01/10/2021 at 15:30 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Once,
                Configuration_Date = new DateTime(2021, 10, 1, 15, 30, 00),
                Start_Date = new DateTime(2021, 10, 1, 15, 30, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetNextDateExecution();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }

        [TestMethod]
        public void GetNextDescription_Recurring_ReturnDesciption()
        {
            String ExpectedDescription = "Ocurrs every day. Schedule will be" +
                " used on 03/10/2021 at 0:00 starting on 01/10/2021";
            ScheduleConfiguration Config = new()
            {
                Active = true,
                Type = (int)Types.Recurring,
                Current_Date = new DateTime(2021, 10, 1, 00, 00, 00),
                Frecuency = Frecuencys.Daily.ToString(),
                Frecuency_Days = 2,
                Start_Date = new DateTime(2021, 10, 1, 00, 00, 00)
            };
            ScheduleCalculator Calculator = new(Config);

            Calculator.GetNextDateExecution();
            string ActualDescription = Calculator.Description_Execution;

            Assert.AreEqual(ExpectedDescription, ActualDescription);
        }
    }
}
