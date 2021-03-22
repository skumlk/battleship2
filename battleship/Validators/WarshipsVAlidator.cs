
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Battleship.Dtos;

namespace Battleship.Validators
{

    public class WarshipsAttributeValidator : ValidationAttribute
    {
        private int _battleshipCount;
        private int _destroyerCount;

        public WarshipsAttributeValidator(int battleshipCount, int destroyerCount)
        {
            _battleshipCount = destroyerCount;
            _destroyerCount  = battleshipCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<WarshipMinDto> warships = (List<WarshipMinDto>)validationContext.ObjectInstance;

            return ValidationResult.Success;
        }
    }
}

