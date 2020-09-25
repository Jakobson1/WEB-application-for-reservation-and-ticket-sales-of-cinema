using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioskopProjekat.Models;
using System.Data.Entity;

namespace BioskopProjekat.Controllers
{
    public class RepertoarController : Controller
    {
        // GET: Repertoar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                List<Film> empList = db.Films.ToList<Film>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Film());
            else
            {
                using (BioskopEnti2 db = new BioskopEnti2())
                {
                    return View(db.Films.Where(x => x.IDFilm == id).FirstOrDefault<Film>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Film film)
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                if (film.IDFilm == 0)
                {
                    db.Films.Add(film);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspesno Sacuvano" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(film).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspesno izmenjeno!" }, JsonRequestBehavior.AllowGet);
                }
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                Film emp = db.Films.Where(x => x.IDFilm == id).FirstOrDefault<Film>();
                db.Films.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Uspesno izbrisano" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}