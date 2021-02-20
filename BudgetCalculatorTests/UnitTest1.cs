using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BudgetCalculatorTests
{
    public class BudgetCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Query_OneDayWith31AmountInJan_ReturnOne()
        {
            var budgetRepo = Substitute.For<IBudgetRepo>();
            budgetRepo.GetAll().Returns(new List<Budget>()
            {
                new Budget(){ YearMonth = "202101", Amount = 31}
            });
            var budgetCalculator = new BudgetCalculator(budgetRepo);
            Assert.AreEqual(1, budgetCalculator.Query(new DateTime(2021,1,1), new DateTime(2021,1,1)));
        }
    }
}