using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTravel.Logic
{ 
    public class Country
    {
        private string name;
        private bool has_universal;
        private string universal, police, fire, ambulance;
        //TODO other informations

        public Country(string s)
        {
            ParseCountry(s);
        }

        private void ParseCountry(string s)
        {
            name = "Romia";
            has_universal = true;
            universal = "112";
            police = "-";
            fire = "-";
            ambulance = "-";
        }

        public string GetName()
        {
            return name;
        }

        public string GetNumbers()
        {
            if (has_universal) return "Universal Number: " + universal;
            else return "Police: " + police + " Fire: " + fire + " Ambulance: " + ambulance;
        }
    }
}