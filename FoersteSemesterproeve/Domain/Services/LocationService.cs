using FoersteSemesterproeve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Services
{
    public class LocationService
    {

        public List<Location> locations;

        public LocationService() 
        {
            locations = populateLocations();
        }


        private List<Location> populateLocations()
        {
            List<Location> locations = new List<Location>();

            

            return locations;

            

        }
        //Hvordan ser en oversigt af lokaler ud(ala stil med Homepage, hvor kolonner indeholder hver deres lokale med gældende aktiviteter):
    }
}
