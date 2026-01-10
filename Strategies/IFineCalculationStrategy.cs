using System;
namespace Project5LMS.Strategies
{
    public interface IFineCalculationStrategy
    {
        decimal CalculateFine(int daysOverdue);
    }
}