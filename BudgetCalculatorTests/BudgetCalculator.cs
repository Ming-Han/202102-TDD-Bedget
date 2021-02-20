using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetCalculatorTests
{
    public class BudgetCalculator
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetCalculator(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public decimal Query(DateTime start, DateTime end)
        {
            if (IsDateInvalid(start, end)) return 0; 
            
            List<Budget> budgets = _budgetRepo.GetAll();
            decimal amount = 0;
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            { 
                Budget budget = budgets.SingleOrDefault(budget => budget.YearMonth == day.ToString("yyyyMM"));
                if (budget != null)
                    amount += budget.Amount / DateTime.DaysInMonth(day.Year, day.Month);
            }

            return (decimal)amount;
        }

        private static bool IsDateInvalid(DateTime start, DateTime end)
        {
            if (start > end)
                return true;
            return false;
        }
    }
}