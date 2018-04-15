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
        static Country[] countries = null;
        static int country_idx, nr;
        static string country_name;
        static DateTime fid;

        public void update()
        {
            if (countries != null)
            {
                SetIndexFromCountryName(country_name);
                UpdateViewBag();
                return;
            }
            string csv = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/CompleteCountries.csv"));
            string[] tmp = csv.Split('\n');
            countries = new Country[tmp.Length];
            for (int i = 0; i < tmp.Length; ++i)
            {
                countries[i] = new Country(tmp[i]);
            }
            SetIndexFromCountryName(country_name);
            UpdateViewBag();
        }

        public void SetIndexFromCountryName(string country_name)
        {
            for (int i = 0; i < countries.Length; ++i)
            {
                if (countries[i].GetName().Equals(country_name))
                {
                    country_idx = i;
                    break;
                }
            }
        }

        public void UpdateViewBag()
        {
            Country country = countries[country_idx];
            ViewBag.Country = country.GetName();
            ViewBag.PhoneNumbers = country.GetNumbers();
            string[] fun_facts = country.GetFunFacts();
            if (fun_facts != null)
            {
                if (fun_facts.Length >= 1) ViewBag.FunFact1 = country.GetFunFacts()[0];
                else ViewBag.FunFact1 = "It's a beautiful country!";

                if (fun_facts.Length >= 2) ViewBag.FunFact2 = country.GetFunFacts()[1];
                else ViewBag.FunFact2 = "People are very hospitable!";
            } else
            {
                ViewBag.FunFact1 = "It's a beautiful country!";
                ViewBag.FunFact2 = "People are very hospitable!";
            }
            ViewBag.Currency = country.GetCurrencyISO();
            ViewBag.CurrencyValue = country.GetCurrencyValue().ToString();
            ViewBag.MyCurrency = "USD";
            ViewData["fd"] = fid;
            ViewBag.NrDays = nr;
            ViewBag.GoogleMapsKey = Logic.Constants.GOOGLE_MAPS_KEY;
            ViewBag.PlacePreferences = "restaurant,bar,park,gym";
        }

        public ActionResult Index()
        {
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

        public ActionResult InitialView()
        {
            return View();
        }

        public ActionResult Dashboard(string country_n, string firstd, int nrd)
        {
            country_name = country_n;
            fid = DateTime.Parse(firstd);
            nr = nrd;
            update();
            return View();
        }
    }
}