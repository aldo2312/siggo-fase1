using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using slnSIGCArchitechWeb17.Models;
using Siggo.SIGC.BusinessLogic;
using Siggo.SIGC.Entity;
using Siggo.SIGC.Util;

namespace slnSIGCArchitechWeb17.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var oUsuarioweb = new BLUsuarioWeb().ObtenerUsuario(model.UserNameLogOn.Trim());
                Session["PwdCaducado"] = "NO";
                if (oUsuarioweb == null)
                {
                    ModelState.AddModelError("LogOnError", "El nombre de usuario o la contraseña especificados son incorrectos.");
                    return View(model);
                }
                else
                {
                    string PWD = Utilitario.FLAG_APLICA_SEG_PWD == 1 ? Utilitario.DecryptText(oUsuarioweb.Contrasenha) : oUsuarioweb.Contrasenha;
                    if (model.PasswordLogOn.Trim().ToUpper() != PWD.ToUpper())
                    {
                        ModelState.AddModelError("LogOnError", "El nombre de usuario o la contraseña especificados son incorrectos.");
                        return View(model);
                    }
                    Session["IdUsuario"] = oUsuarioweb.IdUsuario;
                    Session["NombreUsuario"] = oUsuarioweb.NombreUsuario;
                    Session["UsuarioInterno"] = Convert.ToBoolean(oUsuarioweb.EsUsuarioInterno) ? "Si" : "No";
                    Session["RolUsuario"] = oUsuarioweb.IdRol;
                    string Empresas = oUsuarioweb.IdEmpresa;
                    BEEmpresa oEmpresa = new BLEmpresa().ObtenerEmpresa(Empresas);
                    Session["IdEmpresa"] = oEmpresa.IdEmpresa;
                    Session["NombreEmpresa"] = oEmpresa.RazonSocial;
                    Session["TipoEmpresaSiggo"] = oEmpresa.TipoEmpresaSiggo;
                    
                    FormsAuthentication.SetAuthCookie(model.UserNameLogOn, model.RememberMe);

                    if (model.UserNameLogOn.Trim().ToUpper() == PWD.ToUpper() || oUsuarioweb.FechaAsignacion == null)
                    {
                        Session["PwdCaducado"] = "SI";
                        return RedirectToAction("ChangePassword", "Account");
                    }
                    if (oUsuarioweb.FechaAsignacion != null)
                    {
                        DateTime oldDate = (DateTime)oUsuarioweb.FechaAsignacion;
                        DateTime newDate = DateTime.Now;
                        TimeSpan Diferencia = newDate - oldDate;
                        Int32 nDias = Diferencia.Days;

                        if (nDias > Globales.DIAS_RENOVAR_PWD) // Si se ha vencido la contraseña
                        {
                            Session["PwdCaducado"] = "SI";
                            return RedirectToAction("ChangePassword", "Account");
                        }
                    }

                    return RedirectToAction("Index", "Home");

                    //if (Convert.ToBoolean(oUsuarioweb.EsUsuarioInterno))
                    //{
                    //    Session["IdEmpresa"] = "SIGGO";
                    //    Session["NombreEmpresa"] = "SIGGO";
                    //    Session["TipoEmpresaSiggo"] = "NN";
                    //    return RedirectToAction("Index", "Home");
                    //}
                    //else
                    //    return RedirectToAction("ChangeCompany", "Home");
                }
            }
            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["IdEmpresa"] = null;
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("LogOn", "Account");
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    var objUser = new BLUsuarioWeb().ObtenerUsuario(Session["IdUsuario"].ToString());

                    if (objUser != null)
                    {
                        string sContraseña = Utilitario.DecryptText(objUser.Contrasenha.Trim()).ToUpper();

                        if (sContraseña != model.OldPassword.Trim().ToUpper())
                        {
                            ModelState.AddModelError("", "La contraseña actual es incorrecta. Verificar.");
                            return View(model);
                        }
                    }
                    
                    //Reseteando cuenta del usuario
                    BEUsuarioWeb oUsuario = new BEUsuarioWeb();
                    oUsuario.IdEmpresa = Session["IdEmpresa"].ToString();
                    oUsuario.IdUsuario = Session["IdUsuario"].ToString();
                    oUsuario.Contrasenha = Utilitario.EncryptText(model.NewPassword);
                    oUsuario.FechaAsignacion = DateTime.Now;
                    oUsuario.RecibeNotificaciones = "N";
                    new BLUsuarioWeb().MantenerUsuario(6, oUsuario);
                    
                    objUser = null;

                    changePasswordSucceeded = true;

                    Session["PwdCaducado"] = "NO";
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "No se completó la operación. Por favor, vuelva a intentarlo.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
    }
}