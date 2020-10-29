using System;

namespace Covid.Services
{
    public interface IRangeValidator
    {
        public bool IsValid(int input);
    }

    public class MonthValidator : IRangeValidator
    {
        public bool IsValid(int input)
        {
            return input >= 1 && input <= 12;
        }
    }

    public class LimitValidator : IRangeValidator
    {
        public bool IsValid(int input)
        {
            return input >= 1 && input <= 1000;
        }
    }
}
