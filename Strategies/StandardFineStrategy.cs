using System;
namespace Project5LMS.Strategies
{
    public class StandardFineStrategy : IFineCalculationStrategy
    {
        private const decimal FinePerDay = 0.50m;
        public decimal CalculateFine(int daysOverdue)
        {
            if (daysOverdue <= 0)
                return 0m;
            decimal fine = daysOverdue * FinePerDay;
            return Math.Round(fine, 2, MidpointRounding.AwayFromZero);
        }
    }
}