using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Validators;
using Infraestrutura.EntityFrameworkDAL;
using Infraestrutura.Repositorios;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        BeneficiarioValidator beneficiarioValidator;
        EntrevistaRepositorio entrevistaRepositorio;

        public BeneficiarioController()
        {
            this.beneficiarioValidator = new BeneficiarioValidator();
            this.entrevistaRepositorio = new EntrevistaRepositorio();
        }

        public JsonResult BeneficiarioList(int idCliente)
        {
            var entidades = entrevistaRepositorio.ListarBeneficiarios().Where(b => b.IDCliente == idCliente);
            var beneficiarios = new List<BeneficiarioModel>();
            entidades.ToList().ForEach(e => {
                beneficiarios.Add(new BeneficiarioModel() { Cpf = e.Cpf, ID = e.ID, Nome = e.Nome });
            });
            return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = beneficiarios.Count });
        }


        

        // GET: Beneficiario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beneficiario/Create
        [HttpPost]
        public ActionResult Create(BeneficiarioModel beneficiario)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }
                else
                {
                    var entidade = new Beneficiario(beneficiario.Cpf, beneficiario.Nome, beneficiario.IDCliente);
                    var validador = beneficiarioValidator.Validate(entidade);
                    if (!validador.IsValid)
                    {
                        return Json(string.Join(Environment.NewLine, validador.Errors));
                    }
                    entrevistaRepositorio.Adicionar(entidade);

                    return Json("Cadastro efetuado com sucesso");
                }
                
            }
            catch(System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return Json($"Falha: Cpf já existe");
            }
            catch (Exception ex)
            {
                return Json($"Falha: {ex.Message}" );
            }
        }

        // GET: Beneficiario/Edit/5
        public JsonResult Update(int Id)
        {
            var beneficiario = entrevistaRepositorio.ObterBeneficiario(Id);
            return Json(new { beneficiario.ID,beneficiario.Nome,beneficiario.Cpf }, JsonRequestBehavior.AllowGet);
        }

        

        
        // POST: Beneficiario/Delete/5
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var beneficiario = entrevistaRepositorio.ObterBeneficiario(Id);
                entrevistaRepositorio.Apagar(beneficiario);
                return Json("Beneficiário excluído com sucesso");
            }
            catch (Exception ex)
            {
                return Json($"Falha: {ex.Message}");
            }
        }
    }
}
