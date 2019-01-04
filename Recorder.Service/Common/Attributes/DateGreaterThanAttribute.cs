using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recorder.Service.Common.Attributes
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private string DateToCompareToFiledName { get; set; }

        public DateGreaterThanAttribute(string dateToCompareToFiledName)
        {
            DateToCompareToFiledName = dateToCompareToFiledName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTimeToCheck = (DateTime)value;
            DateTime anotherDateTime = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFiledName)
                .GetValue(validationContext.ObjectInstance);

            if (dateTimeToCheck > anotherDateTime)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not later.");
            }
        }
    }
}
