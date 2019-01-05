using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Recorder.Service.Helpers;

namespace Recorder.Service.Attributes
{
    public class IpAddressAttribute: ValidationAttribute
    {        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var address = (string) value;

            if (address.isIPAddress())
                return ValidationResult.Success;
           
            return new ValidationResult("It is not an IP-address.");
        }        
    }
}
