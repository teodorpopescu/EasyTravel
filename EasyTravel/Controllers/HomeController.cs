using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace EasyTravel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            con();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        private void con()
        {
            string connectionString = System.Configuration.ConfigurationManager.
                 ConnectionStrings["agenda_db"].ConnectionString;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                OleDbDataReader dr;
                String sq = "SELECT * from agenda";
                OleDbCommand cmd = new OleDbCommand(sq, conn);

                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    System.Diagnostics.Debug.WriteLine("User: {0}, {1}", dr.GetValue(0), dr.GetValue(1));
                }

                dr.Close();
                cmd.Dispose();
            }
        }
    }
}