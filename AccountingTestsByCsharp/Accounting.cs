﻿using System;
using System.Linq;

namespace AccountingTestsByCsharp
{
    public class Accounting
    {
        private readonly IRepository<Budget> _budgetRepository;

        public Accounting(IRepository<Budget> budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public decimal TotalAmount(DateTime start, DateTime end)
        {
            var budgets = _budgetRepository.GetAll();

            if (budgets.Any())
            {
                var budget = budgets.First();
                var period = new Period(start, end);

                var dailyAmount = budget.Amount / budget.Days;
                var overlappingDays = period.OverlappingDays(new Period(budget.FirstDay, budget.LastDay));
                return (decimal)(dailyAmount * overlappingDays);
            }

            return 0;
        }
    }
}