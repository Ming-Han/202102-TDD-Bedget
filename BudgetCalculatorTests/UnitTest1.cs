using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace BudgetCalculatorTests
{
    public class BudgetCalculatorTests
    {
        private IBudgetRepo _budgetRepo;

        [SetUp]
        public void SetUp()
        {
            _budgetRepo = Substitute.For<IBudgetRepo>();
        }

        public void GivenBudgets(List<Budget> budgets)
        {
            _budgetRepo.GetAll().Returns(budgets);
        }

        [Test]
        public void Query_OneDayWith31AmountInJan_ReturnOne()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 31}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(1, budgetCalculator.Query(new DateTime(2021, 1, 1), new DateTime(2021, 1, 1)));
        }

        [Test]
        public void Query_OneMonthWith62AmountInJan_Return62()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 62}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(62, budgetCalculator.Query(new DateTime(2021, 1, 1), new DateTime(2021, 1, 31)));
        }

        [Test]
        public void Query_StartDateGreaterEndDate_ReturnZero()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 62},
                new() {YearMonth = "202102", Amount = 28}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(0, budgetCalculator.Query(new DateTime(2021, 2, 1), new DateTime(2021, 1, 30)));
        }

        [Test]
        public void Query_Jan01toFeb28WithJanAmount62FebAmount28_Return90()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 62},
                new() {YearMonth = "202102", Amount = 28}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(90, budgetCalculator.Query(new DateTime(2021, 1, 1), new DateTime(2021, 2, 28)));
        }

        [Test]
        public void Query_Jan30toFeb01WithJanAmount62FebAmount28_Return5()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 62},
                new() {YearMonth = "202102", Amount = 28}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(5, budgetCalculator.Query(new DateTime(2021, 1, 30), new DateTime(2021, 2, 1)));
        }

        [Test]
        public void Query_Jan01toMar31WithJanAmount62MarAmount31FebNoData_Return93()
        {
            GivenBudgets(new List<Budget>
            {
                new() {YearMonth = "202101", Amount = 62},
                new() {YearMonth = "202103", Amount = 31}
            });
            var budgetCalculator = new BudgetCalculator(_budgetRepo);
            Assert.AreEqual(93, budgetCalculator.Query(new DateTime(2021, 1, 1), new DateTime(2021, 3, 31)));
        }
    }
}