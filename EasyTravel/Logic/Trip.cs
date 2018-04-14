using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTravel.Logic
{
    public class Trip
    {
        private Country country;
        private List<Objective> objectives;

        public Trip()
        {
            objectives = new List<Objective>();
        }

        public void AddObjective(Objective objective)
        {
            objectives.Add(objective);
        }
    }
}