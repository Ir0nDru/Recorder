using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recorder.Service.Common.Attributes
{
    public class MacAddressAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string mac = (string)value;

            var addMacReg = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";            
            var regex = new Regex(addMacReg);

            if (regex.IsMatch(mac))
                return ValidationResult.Success;

            return new ValidationResult("It is not MAC-address.");
        }
    }
}
