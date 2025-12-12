
namespace FoersteSemesterproeve.Domain.Models
{
    /// <summary>
    ///     Activity class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class Activity
    {
        public string title { get; set; }
        public User? coach { get; set; }
        public Location location { get; set; }
        public int? maxCapacity {  get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<User> participants { get; set; }
        
        /// <summary>
        ///     Constructor til Activity class
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="title"></param>
        /// <param name="coach"></param>
        /// <param name="location"></param>
        /// <param name="maxCapacity"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        public Activity (string title, User? coach, Location location, int? maxCapacity, DateTime startTime, DateTime endTime)
        {
            this.title = title;
            this.coach = coach;
            this.maxCapacity = maxCapacity;
            this.startTime = startTime;
            this.endTime = endTime;
            this.location = location;
            this.participants = new List<User> ();
        }
    }
}
