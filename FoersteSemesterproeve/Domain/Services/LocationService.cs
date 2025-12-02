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
        public Location? targetLocation;

        public LocationService() 
        {
            locations = populateLocations();
        }


        private List<Location> populateLocations()
        {
            List<Location> locations = new List<Location>();

            locations.Add(new Location("A", "Description here is...", 10));
            locations.Add(new Location("B", "Description here is...", 5));
            locations.Add(new Location("C", "Description here is...", 25));
            locations.Add(new Location("D", "Description here is...", 40));
            locations.Add(new Location("E", "Description here is...", null));
            locations.Add(new Location("F", "Description here is...", null));

            return locations;
        }
        //Hvordan ser en oversigt af lokaler ud(ala stil med Homepage, hvor kolonner indeholder hver deres lokale med gældende aktiviteter):
    }
}
