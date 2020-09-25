using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using BioskopProjekat.Models;

namespace BioskopProjekat.Controllers
{
    public class LoginController : Controller
    {

        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Radnici
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        void connectionString()
        {
            con.ConnectionString = "data source=localhost; database=Bioskop; integrated security=SSPI;";
        }
        [HttpPost]
        public ActionResult Verify(Login log)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from Login where username='" + log.username + "' and password='" + log.password + "'";
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                con.Close();
                return View("PocetnaStranica");


            }
            else
            {
                con.Close();
                return View("Login");
            }

        }
    }
}