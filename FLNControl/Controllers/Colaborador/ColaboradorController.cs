using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using engenharia.Models.Colaborador;
using Microsoft.AspNetCore.Mvc;
using engenharia.Models;
using engenharia.DAL.ColaboradorDAL;
using Microsoft.AspNetCore.Http;
using engenharia.DAL.CaixaDAL;
using engenharia.Models.Caixa;
using Microsoft.AspNetCore.Identity;

namespace engenharia.Controllers.ColaboradorController
{

    namespace engenharia.Controllers.ColaboradorController
    {
        public class ColaboradorController : Controller
        {
            public Colaborador usuario;
            public IActionResult Index()
            {
                ColaboradorDAL udal = new ColaboradorDAL();

                if ((udal.Listar()).Count == 0)
                    return View("PrimeiroAcesso");
                else
                    return View();
            }

            [HttpPost]
            public JsonResult Autenticacao(string login, string senha)
            {
                ColaboradorDAL ud = new ColaboradorDAL();
                object loginVerifica = ud.ValidarUsuario(login, senha);

                if (Convert.ToBoolean(loginVerifica) == true)
                {
                    usuario = ud.Pegar(login);
                    HttpContext.Session.SetString("usu_nivel", usuario.nivel);
                    HttpContext.Session.SetString("usu_nome", usuario.nome);
                    HttpContext.Session.SetInt32("usu_id", usuario.id);

                    CaixaDAL cx = new CaixaDAL();
                    Caixa caixa = new Caixa();

                    caixa = cx.Atual();
                    if (caixa.status == null)
                    {
                        caixa.status = "Novo";
                    }
                    HttpContext.Session.SetString("caixa_status", caixa.status);

                    ViewBag.Nivel = usuario.nivel;

                    var teste = new { retorno = true };
                    return Json(teste);
                }
                else
                {
                    return Json(new { retorno = false });
                }
            }   

            public JsonResult EditarUsuarioExistente(string nome, string login, string senha, string cpf, string rg, DateTime data_nasc, string telefone, string email, string cargo, string status, string nivel, int id)
            {
                DAL.ColaboradorDAL.ColaboradorDAL ud = new DAL.ColaboradorDAL.ColaboradorDAL();
                bool created = true;
                bool ativoVerifica = true;
                bool loginVerifica = ud.verificarLoginExisteEdit(id, login);
                if(status != "Ativo")
                {
                    ativoVerifica = ud.Usuarios(status);
                }

                if (loginVerifica == false || ativoVerifica == false)
                {
                    created = false;
                    return Json(new
                    {
                        retorno = created,
                    });
                }
                else
                {
                    bool editar = ud.EditarUsuario(nome, login, senha, cpf, rg, data_nasc, telefone, email, cargo, status, nivel, id);
                    if (editar == true)
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

            [HttpPost]
            public ActionResult Editar(Colaborador colaborador)
            {
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
                ColaboradorDAL ud = new ColaboradorDAL();

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
                ColaboradorDAL ud = new ColaboradorDAL();

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
                    if (gravar == false)
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
