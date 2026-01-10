using System;
namespace Project5LMS.Strategies
{
    public class GraduatedFineStrategy : IFineCalculationStrategy
    {
        private const decimal FirstWeekRate = 0.50m;
        private const decimal SecondWeekRate = 1.00m;
        private const decimal BeyondTwoWeeksRate = 2.00m;
        public decimal CalculateFine(int daysOverdue)
        {
            if (daysOverdue <= 0)
                return 0m;
            decimal fine = 0m;
            if (daysOverdue <= 7)
            {
                fine = daysOverdue * FirstWeekRate;
            }
            else if (daysOverdue <= 14)
            {
                fine = (7 * FirstWeekRate) + ((daysOverdue - 7) * SecondWeekRate);
            }
            else
            {
                fine = (7 * FirstWeekRate) + (7 * SecondWeekRate) + ((daysOverdue - 14) * BeyondTwoWeeksRate);
            }
            return Math.Round(fine, 2, MidpointRounding.AwayFromZero);
        }
    }
}