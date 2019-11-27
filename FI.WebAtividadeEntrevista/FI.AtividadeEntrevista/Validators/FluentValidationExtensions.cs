using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Validators
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotNullWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage("Campo, {PropertyName}, não pode ser nulo.");
        }
    }
}
