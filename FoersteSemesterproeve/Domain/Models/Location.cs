
namespace FoersteSemesterproeve.Domain.Models
{
    public class Location
    {
        public string name;
        public string description;
        public int? maxCapacity;

        public Location(string name, string description, int? maxCapacity)
        {
            this.name = name;
            this.description = description;
            this.maxCapacity = maxCapacity;
        }
    }
}
