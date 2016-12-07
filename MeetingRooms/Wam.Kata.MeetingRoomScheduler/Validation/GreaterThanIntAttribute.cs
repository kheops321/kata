using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Wam.Kata.MeetingRoomScheduler.Validation
{
    public class GreaterThanIntAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public GreaterThanIntAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var targetProperty = validationContext.ObjectInstance.GetType().GetProperties().First(x => x.Name == _propertyName);
            var targetValue = targetProperty.GetValue(validationContext.ObjectInstance);

            return (int)targetValue < (int)value
                ? null
                : new ValidationResult(ErrorMessage);
        }
    }
}