using System.Collections.Generic;

namespace BudgetCalculatorTests
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}