using System;
using System.IO;
using System.Web.Mvc;

namespace slnSIGCArchitechWeb17.Controllers
{
    public class UploadImageController : Controller
    {
        // GET: UploadImage
        public ActionResult Index()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    var name = Guid.NewGuid().ToString();
                    var fileName = String.Format("{0}{1}", name, extension);

                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    var pathRelativo = "../../../Images/" + fileName;
                    file.SaveAs(path);
                    return Json(new { path = pathRelativo });
                }
            }
            return RedirectToAction("Index");
        }
    }
}