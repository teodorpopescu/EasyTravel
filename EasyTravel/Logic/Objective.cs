using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyTravel.Logic
{
    public class Objective
    {
        private string name;
        private double latitude, longitude;
        private DateTime arrive, depart;

        public Objective(string name, double latitude, double longitude, DateTime arrive, DateTime depart)
        {
            this.name = name;
            this.latitude = latitude;
            this.longitude = longitude;
            this.arrive = arrive;
            this.depart = depart;
        }
    }
}