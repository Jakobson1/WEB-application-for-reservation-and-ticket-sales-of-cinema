using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioskopProjekat.Models;
using System.Data.Entity;

namespace BioskopProjekat.Controllers
{
    public class RezervacijeController : Controller
    {
        // GET: Rezervacije
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                List<Rezervacija> empList = db.Rezervacijas.ToList<Rezervacija>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult AddOrEditRez(int id = 0)
        {
            if (id == 0)
                return View(new Rezervacija());
            else
            {
                using (BioskopEnti2 db = new BioskopEnti2())
                {
                    return View(db.Rezervacijas.Where(x => x.IDRez == id).FirstOrDefault<Rezervacija>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditRez(Rezervacija reze)
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                if (reze.IDRez == 0)
                {
                    db.Rezervacijas.Add(reze);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspesno Sacuvano" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(reze).State = EntityState.Modified;
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
                Rezervacija emp = db.Rezervacijas.Where(x => x.IDRez == id).FirstOrDefault<Rezervacija>();
                db.Rezervacijas.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Uspesno izbrisano" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}