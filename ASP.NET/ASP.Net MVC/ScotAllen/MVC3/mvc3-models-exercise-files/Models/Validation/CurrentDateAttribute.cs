using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Validation
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
            :base("{0} is not current (between {1:d} and {2:d}")
        {
            
        }

        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            var date = (DateTime)value;
            var minDate = MinDate ?? DateTime.Now.AddMonths(-6);
            var maxDate = MaxDate ?? DateTime.Now.AddDays(7);

            if (date < minDate || date > maxDate)
            {
                var message = String.Format(ErrorMessageString,
                                            validationContext.DisplayName,
                                            minDate, maxDate);
                return new ValidationResult(message);
            }

            return null;
        }

        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
    }
}