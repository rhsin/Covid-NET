using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Covid.Services
{
    public interface IInputValidator
    {
        public bool IsValid(string input);
    }

    public class CountyValidator : IInputValidator
    {
        public bool IsValid(string input)
        {
            var inputExpression = new Regex("^[0-9A-Za-z ]{0,24}+$");

            return inputExpression.IsMatch(input);
        }
    }

    public class StateValidator : IInputValidator
    {
        public bool IsValid(string input)
        {
            var inputExpression = new Regex("^[0-9A-Za-z ]{0,13}+$");

            return inputExpression.IsMatch(input);
        }
    }

    public class ColumnValidator : IInputValidator
    {
        public bool IsValid(string input)
        {
            var columns = new List<string>
            {
                "county", "state", "cases", "death"
            };

            return columns.Contains(input.ToLower());
        }
    }

    public class OrderValidator : IInputValidator
    {
        public bool IsValid(string input)
        {
            return input.ToLower() == "desc" || String.IsNullOrEmpty(input);
        }
    }
}
