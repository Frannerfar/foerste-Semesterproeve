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
            locations = PopulateLocations();
        }


        private List<Location> PopulateLocations()
        {
            List<Location> locations = new List<Location>();

            locations.Add(new Location("Room A", "Room is fitting for yoga", 10));
            locations.Add(new Location("Room B", "Room is fitting for meeting", 5));
            locations.Add(new Location("Room C", "Room is fitting for spinning class", 25));
            locations.Add(new Location("Field A", "Small field fiting for street football", 40));
            locations.Add(new Location("Field B", "Huge field fitting for everything", null));
            locations.Add(new Location("Field C", "Large field fitting for most things", null));
            locations.Add(new Location("Small Tennis Court", "Small court fitting for playing a match", null));

            return locations;
        }

        public void AddLocation(string name, string description, int? capacity)
        {
            locations.Add(new Location(name, description, capacity));
        }
    }
}
