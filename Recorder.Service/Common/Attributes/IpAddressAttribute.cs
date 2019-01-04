using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recorder.Service.Common.Attributes
{
    public class IpAddressAttribute: ValidationAttribute
    {        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var address = (string) value;

            if (String.IsNullOrWhiteSpace(address))
                return new ValidationResult(GetErrorMessage());

            string[] splitValues = address.Split('.');
            if (splitValues.Length != 4)
                return new ValidationResult(GetErrorMessage());

            if (splitValues.All(b => byte.TryParse(b, out _)))
                return ValidationResult.Success;

            return new ValidationResult(GetErrorMessage());
        }

        private string GetErrorMessage()
        {
            return "It is not an IP-address.";
        }
    }
}
