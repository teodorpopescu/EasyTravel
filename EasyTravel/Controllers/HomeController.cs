using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlClient;
using System.Data.OleDb;
using EasyTravel.Logic;

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
            string csv = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/CompleteCountries.csv"));
            Country country = new Country(csv.Split('\n')[130]);
            ViewBag.Country = country.GetName();
            ViewBag.PhoneNumbers = country.GetNumbers();
            string[] fun_facts = country.GetFunFacts();
            if (fun_facts != null)
            {
                if (fun_facts.Length >= 1) ViewBag.FunFact1 = country.GetFunFacts()[0];
                else ViewBag.FunFact1 = "It's a beautiful country!";

                if (fun_facts.Length >= 2) ViewBag.FunFact2 = country.GetFunFacts()[1];
                else ViewBag.FunFact2 = "People are very hospitable!";
            }
            ViewBag.Currency = country.GetCurrencyISO();
            ViewBag.CurrencyValue = country.GetCurrencyValue().ToString();
            ViewBag.MyCurrency = "USD";
            ViewBag.GoogleMapsKey = Logic.Constants.GOOGLE_MAPS_KEY;
            ViewBag.PlacePreferences = "night_club,museum,restaurant,gym";
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