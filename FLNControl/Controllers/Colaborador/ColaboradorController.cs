using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.Models.Colaborador;
using Microsoft.AspNetCore.Mvc;
using engenharia.Models;

namespace engenharia.Controllers.ColaboradorController
{

    namespace engenharia.Controllers.ColaboradorController
    {
        public class ColaboradorController : Controller
        {
            
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public JsonResult Autenticacao(string login, string senha)

            {
                DAL.ColaboradorDAL.ColaboradorDAL ud = new DAL.ColaboradorDAL.ColaboradorDAL();

                object loginVerifica = ud.ValidarUsuario(login, senha);
                    
                if (Convert.ToBoolean(loginVerifica) == true)
                { 
                    var teste = new { retorno = true };
                    return Json(teste);
                }
                else
                {
                    return Json(new { retorno = false });
                }
            }

            [HttpPost]
            public ActionResult GravarEditar(string novoLogin)
            {
                DAL.ColaboradorDAL.ColaboradorDAL ud = new DAL.ColaboradorDAL.ColaboradorDAL();

                if (novoLogin == "Editar")
                {
                    return RedirectToAction("Editar");
                }
                else
                {
                    return RedirectToAction("Gravar");
                }
            }

            [HttpGet]
            public ActionResult Editar(string nome, string login, string senha, string cpf, string rg, DateTime data_nasc, string telefone, string email, string cargo, string status, string nivel)
            {
                Colaborador colaborador = new Colaborador();
                colaborador.nome = nome;
                colaborador.login = login;
                colaborador.senha = senha;
                colaborador.cpf = cpf;
                colaborador.rg = rg;
                colaborador.data_nasc = data_nasc;
                colaborador.telefone = telefone;
                colaborador.email = email;
                colaborador.cargo = cargo;
                colaborador.status = status;
                colaborador.nivel = nivel;

                return View("Editar", colaborador);
            }

            [HttpPost]
            public ActionResult Gravar(string novoLogin)
            {
                return View("Gravar");
            }

            

            [HttpPost]
            public ActionResult AutenticacaoEdit(string novologinEdit, string novasenhaEdit)
            {
                DAL.ColaboradorDAL.ColaboradorDAL ud = new DAL.ColaboradorDAL.ColaboradorDAL();

                bool loginVerifica = ud.ValidarUsuarioEdit(novologinEdit, novasenhaEdit);

                if (loginVerifica == false)
                {
                    return View("Colaborador/Editar");
                }
                else
                {
                    ud.EditarUsuario(novologinEdit, novasenhaEdit);
                    return RedirectToAction("Index", "Acesso");
                }
            }

            [HttpPost]
            public ActionResult CadastrarAcesso(string nome, string login, string senha, string cpf, string rg, DateTime data_nasc, string telefone, string email, string cargo, string status, string nivel)
            {
                DAL.ColaboradorDAL.ColaboradorDAL ud = new DAL.ColaboradorDAL.ColaboradorDAL();

                bool created = true;
                bool loginVerifica = ud.verificarLoginExiste(login);

                if (loginVerifica == false)
                {
                    created = false;
                    return Json(new
                    {
                        retorno = created,
                    });
                }
                else
                {
                    bool gravar = ud.GravarUsuario(nome, login, senha, cpf, rg, data_nasc, telefone, email, cargo, status, nivel);
                    if(gravar == true)
                    {
                        return Json(new
                        {
                            retorno = created,
                        });
                    }
                    else
                    {
                        created = false;
                        return Json(new
                        {
                            retorno = created,
                        });
                    }
                }
            }

        }
    }
}