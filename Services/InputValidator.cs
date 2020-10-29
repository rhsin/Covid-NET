using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Covid.Services
{
    public interface IInputValidator
    {
        public bool IsValidCounty(string input);
        public bool IsValidState(string input);
        public bool IsValidColumn(string input);
        public bool IsValidOrder(string input);
        public bool IsValidMonth(int input);
        public bool IsValidLimit(int input);
    }

    public class InputValidator : IInputValidator
    {
        public bool IsValidCounty(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                var inputExpression = new Regex("^[a-zA-Z ]{0,24}$");

                return inputExpression.IsMatch(input);
            }
            return true;
        }

        public bool IsValidState(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                var inputExpression = new Regex("^[a-zA-Z ]{0,13}$");

                return inputExpression.IsMatch(input);
            }
            return true;
        }

        public bool IsValidColumn(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                var columns = new List<string>
                {
                    "id", "date", "county", "state", "fips", "cases", "deaths"
                };
                return columns.Contains(input.ToLower());
            }
            return false;
        }

        public bool IsValidOrder(string input)
        {
            if (!String.IsNullOrEmpty(input))
            {
                return input.ToLower() == "desc" || input.ToLower() == "asc";
            }
            return true;
        }

        public bool IsValidMonth(int input)
        {
            return input >= 1 && input <= 12;
        }

        public bool IsValidLimit(int input)
        {
            return input >= 1 && input <= 1000;
        }
    }
}
