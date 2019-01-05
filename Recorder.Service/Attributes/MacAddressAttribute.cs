using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Recorder.Service.Attributes
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
