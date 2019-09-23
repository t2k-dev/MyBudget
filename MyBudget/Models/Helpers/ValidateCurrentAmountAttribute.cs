using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBudget.Models.Helpers
{
    public class ValidateCurrentAmountAttribute: ValidationAttribute
    {        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Goal)validationContext.ObjectInstance;
            if (Convert.ToDouble(value) > model.Amount)
                return new ValidationResult(FormatErrorMessage("Сумма пополнения не может быть больше общей суммы"));
            else
                return ValidationResult.Success;
        }
    }
}