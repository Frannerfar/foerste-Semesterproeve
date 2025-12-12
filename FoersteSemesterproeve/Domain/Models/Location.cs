
namespace FoersteSemesterproeve.Domain.Models
{
    /// <summary>
    ///     Location class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class Location
    {
        public string name;
        public string description;
        public int? maxCapacity;

        /// <summary>
        ///     Constructor til Location class
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="maxCapacity"></param>
        public Location(string name, string description, int? maxCapacity)
        {
            this.name = name;
            this.description = description;
            this.maxCapacity = maxCapacity;
        }
    }
}
