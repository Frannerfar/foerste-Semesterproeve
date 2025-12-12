using FoersteSemesterproeve.Domain.Models;


namespace FoersteSemesterproeve.Domain.Services
{

    /// <summary>
    ///     LocationService class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class LocationService
    {

        public List<Location> locations;
        public Location? targetLocation;

        /// <summary>
        ///     Constructor til LocationService
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        public LocationService() 
        {
            // Constructoren LocationService tager ingen parametre. Derimod så fylder den listen "locations" op med lokationer.
            // locations modtager liste af lokationer, da PopulateLocations() har en return type List<Location>
            locations = PopulateLocations();
        }

        /// <summary>
        ///     Bruges til at lave pre-definerede lokationer ved opstart af programmet
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <returns></returns>
        private List<Location> PopulateLocations()
        {
            // Der oprettes en variabel med navnet "locations" der modtager en ny instantiated liste af lokationer, List<Location>
            List<Location> locations = new List<Location>();

            // Der instantieres nyt Location objekt og tilføjes direkte til listen af lokationer "locations".
            // Dette gøres 7 gange
            locations.Add(new Location("Room A", "Room is fitting for yoga", 10));
            locations.Add(new Location("Room B", "Room is fitting for meeting", 5));
            locations.Add(new Location("Room C", "Room is fitting for spinning class", 25));
            locations.Add(new Location("Field A", "Small field fiting for street football", 40));
            locations.Add(new Location("Field B", "Huge field fitting for everything", null));
            locations.Add(new Location("Field C", "Large field fitting for most things", null));
            locations.Add(new Location("Small Tennis Court", "Small court fitting for playing a match", null));

            // funktionen returnerer listen af lokationer
            return locations;
        }

        /// <summary>
        ///     Bruges til at tilføje en ny lokation
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="capacity"></param>
        public void AddLocation(string name, string description, int? capacity)
        {
            // Der instantieres nyt Location objekt og tilføjes dirrekte til listen af lokationer "locations".
            locations.Add(new Location(name, description, capacity));
        }
    }
}
