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
        private string currency;
        private double currency_value;
        private string[] fun_facts;
        //TODO other informations

        public Country(string s)
        {
            ParseCountry(s);
        }

        private void ParseCountry(string s)
        {
            var v = s.Split(';');
            name = v[0];
            police = v[1];
            fire = v[2];
            ambulance = v[3];
            currency = v[4];
            currency_value = Double.Parse(v[5]);
            if (String.IsNullOrEmpty(fire))
            {
                has_universal = true;
                universal = police;
            }
            else has_universal = false;

            if (String.IsNullOrEmpty(ambulance)) ambulance = fire;
            if (v.Length > 6) fun_facts = new string[v.Length - 6];
            else fun_facts = null;
            for (int i = 6; i < v.Length; ++i) fun_facts[i - 6] = v[i];
        }

        public string GetName()
        { 
            return name;
        }

        public string GetCurrencyISO()
        {
            return currency;
        }

        public string[] GetFunFacts()
        {
            return fun_facts;
        }

        public string GetNumbers()
        {
            if (has_universal) return "Universal Number: " + universal;
            else return "Police: " + police + " Fire: " + fire + " Ambulance: " + ambulance;
        }
    }
}