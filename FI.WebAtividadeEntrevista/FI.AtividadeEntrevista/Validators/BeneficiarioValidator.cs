using FI.AtividadeEntrevista.DML;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace FI.AtividadeEntrevista.Validators
{
    public class BeneficiarioValidator : AbstractValidator<Beneficiario>
    {
        public BeneficiarioValidator()
        {
            RuleFor(b => b.Nome)
                .NotNullWithMessage()
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(b => b.Cpf)
                .NotNullWithMessage()
                .Custom((cpf, context) => {
                    if (!Util.ValidarCPf(cpf))
                        context.AddFailure("Cpf inválido");
                } );
            
        }
    }
}
