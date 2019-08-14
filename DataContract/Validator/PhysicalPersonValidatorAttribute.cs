using DataContract.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataContract.Validator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PhysicalPersonValidatorAttribute : ValidationAttribute
    {
        private static readonly char[] GeorgianAlphabeth = { 'ა', 'ბ', 'გ', 'დ', 'ე', 'ვ', 'ზ', 'თ', 'ი', 'კ', 'ლ', 'მ', 'ნ', 'ო', 'პ', 'ჟ', 'რ', 'ს', 'ტ', 'უ', 'ფ', 'ქ', 'ღ', 'ყ', 'შ', 'ჩ', 'ც', 'ძ', 'წ', 'ჭ', 'ხ', 'ჯ', 'ჰ' };

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return true;
            }
            var input = value as PhysicalPerson;
            var nameIsValid = CheckIfOnlyEnglish(input.Name) || CheckIfOnlyGeorgian(input.Name);
            var lastNameIsValid = CheckIfOnlyEnglish(input.LastName) || CheckIfOnlyGeorgian(input.LastName);
            if(!nameIsValid || !lastNameIsValid)
            {
                ErrorMessage = "Failed Name Validation";
                return false;
            }
            if (GetAge(input.BirthDate) < 18)
            {
                ErrorMessage = ("Invalid Birth Day, Not Adult");
                return false;
            }
            return true;
        }

        private bool CheckIfOnlyEnglish(string input)
        {
            if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            return false;
        }

        private bool CheckIfOnlyGeorgian(string input)
        {
            foreach (var c in input)
            {
                if (GeorgianAlphabeth.Contains(c))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        private static int GetAge(DateTime bornDate)
        {
            var today = DateTime.Today;
            var age = today.Year - bornDate.Year;
            if (bornDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
