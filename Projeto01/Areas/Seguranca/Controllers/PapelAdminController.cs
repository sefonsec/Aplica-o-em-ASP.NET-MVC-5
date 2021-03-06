﻿using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projeto01.Areas.Seguranca.Models;
using Projeto01.Infraestrutura;

namespace Projeto01.Areas.Seguranca.Controllers
{
    public class PapelAdminController : Controller
    {       
        // GET: Seguranca/RoleAdmin
        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        private GerenciadorPapel RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorPapel>();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        [HttpPost]
        public ActionResult Create([Required] string nome)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new Papel(nome));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            return View(nome);
        }
    }
}