using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utilidades;

namespace WebAtividadeEntrevista.CustonValidators
{
    public class ValidarCpf : ValidationAttribute, IClientValidatable
    {
        public ValidarCpf()
        {

        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(null),
                ValidationType = "validarcpf"
            };
        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = Util.ValidarCPf(value.ToString());
            return valido;
            
        }
    }
}