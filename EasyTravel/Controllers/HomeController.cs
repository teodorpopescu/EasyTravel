using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace EasyTravel.Controllers
{
    public class HomeController : Controller
    {
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
      
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public void wetter()
        {
            try
            {
                string ort; // ort = Location
                ort = "Bucharest";
                XmlDocument googlewetter = new XmlDocument();
                googlewetter.Load(string.Format("http://www.google.com/ig/api?weather={0}", ort));

                string stadt = googlewetter.SelectSingleNode("/xml_api_reply/weather/forecast_information/city").Attributes[0].InnerText;
                lbStadt.Text = stadt;

                string temperatur = googlewetter.SelectSingleNode("/xml_api_reply/weather/current_conditions/temp_c").Attributes[0].InnerText;
                lbTemperatur.Text = temperatur;

                string aktuell = googlewetter.SelectSingleNode("/xml_api_reply/weather/current_conditions/condition").Attributes[0].InnerText;
                lbAktuell.Text = aktuell;

                string wind = googlewetter.SelectSingleNode("/xml_api_reply/weather/current_conditions/wind_condition").Attributes[0].InnerText;
                lbWind.Text = wind;

                string luft = googlewetter.SelectSingleNode("/xml_api_reply/weather/current_conditions/humidity").Attributes[0].InnerText;
                lbLuft.Text = luft;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
    }
}