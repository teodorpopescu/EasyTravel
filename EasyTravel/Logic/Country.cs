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

            if (String.IsNullOrEmpty(fire)) has_universal = true;
            else has_universal = false;

            if (String.IsNullOrEmpty(ambulance)) ambulance = fire;
            if (v.Length > 4) fun_facts = new string[v.Length - 4];
            else fun_facts = null;
            for (int i = 4; i < v.Length; ++i) fun_facts[i - 4] = v[i];
        }

        public string GetName()
        { 
            return name;
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