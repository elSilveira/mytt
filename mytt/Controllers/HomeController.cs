using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using mytt.Models;

namespace mytt.Controllers
{
    public class HomeController : Controller
    {
        Contexto db = new Contexto();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var u = db.Usuario.FirstOrDefault(w => w.Username.Equals(model.Username));

                if (u == null)
                {
                    var usuario = new Usuario();

                    TryUpdateModel(usuario);

                    usuario.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password));

                    db.Usuario.Add(usuario);

                    db.SaveChanges();

                    return RedirectToAction("Sucesso");
                }
                else
                {
                    ModelState.AddModelError("Username", "Este nome de usuário já existe! Por favor, escolha outro.");
                }
            }

            return View(model);
        }

        public ActionResult Sucesso()
        {
            return View();
        }
    }
}