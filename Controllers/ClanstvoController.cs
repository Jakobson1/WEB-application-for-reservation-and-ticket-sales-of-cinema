using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioskopProjekat.Models;
using System.Data.Entity;

namespace BioskopProjekat.Controllers
{
    public class ClanstvoController : Controller
    {
        // GET: Clanstvo
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult GetData()
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                List<Clanstvo> empList = db.Clanstvoes.ToList<Clanstvo>();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditClan(int id = 0)
        {
            if (id == 0)
                return View(new Clanstvo());
            else
            {
                using (BioskopEnti2 db = new BioskopEnti2())
                {
                    return View(db.Clanstvoes.Where(x => x.IDClan == id).FirstOrDefault<Clanstvo>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditClan(Clanstvo clanstvo)
        {
            using (BioskopEnti2 db = new BioskopEnti2())
            {
                if (clanstvo.IDClan == 0)
                {
                    db.Clanstvoes.Add(clanstvo);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Uspesno Sacuvano" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(clanstvo).State = EntityState.Modified;
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
                Clanstvo emp = db.Clanstvoes.Where(x => x.IDClan == id).FirstOrDefault<Clanstvo>();
                db.Clanstvoes.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Uspesno izbrisano" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}