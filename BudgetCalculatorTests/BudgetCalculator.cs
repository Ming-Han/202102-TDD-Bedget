using System;
using System.Collections.Generic;
using System.IO;
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
            var daysInMonth = GetDaysInMonth(start, end);

            return CalculateAmount(daysInMonth, budgets);
        }

        private static decimal CalculateAmount(Dictionary<string, int> daysInMonth, List<Budget> budgets)
        {
            decimal amount = 0;
            foreach (var month in daysInMonth)
            {
                Budget budget = budgets.SingleOrDefault(budget => budget.YearMonth == month.Key);
                var day = DateTime.ParseExact(month.Key, "yyyyMM", null);

                if (budget != null)
                    amount += (budget.Amount / DateTime.DaysInMonth(day.Year, day.Month)) * month.Value;
            }

            return amount;
        }

        private static Dictionary<string, int> GetDaysInMonth(DateTime start, DateTime end)
        {
            Dictionary<String, int> daysInMonth = new Dictionary<string, int>();
            for (var day = start.Date; day.Date <= end.Date; day = day.AddDays(1))
            {
                var yearMonth = day.ToString("yyyyMM");
                daysInMonth.TryAdd(yearMonth, 0);
                daysInMonth[yearMonth] += 1;
            }

            return daysInMonth;
        }

        private static bool IsDateInvalid(DateTime start, DateTime end)
        {
            if (start > end)
                return true;
            return false;
        }
    }
}