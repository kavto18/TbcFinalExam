using DataContract.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataContract.Validator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PhysicalPersonConnectionValidatorAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var input = value as PhysicalPersonConnection;
            if (input.ConnectedPhysicalPersonId == input.PhysicalPersonId)
            {
                ErrorMessage = "Connected Persons Are Same Persons";
                return false;
            }
            return true;
        }
    }
}
