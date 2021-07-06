using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalAP2.Validaciones
{
    public class IdValidacion : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int id = 0;

                try
                {
                    id = Convert.ToInt32(value);
                }
                catch (Exception)
                {
                    return new ValidationResult("No es un Id válido");
                }

                if (id >= 0)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("El Id no puede estar negativo");
            }
            return new ValidationResult("Debes poner un Id");
        }
    }
}