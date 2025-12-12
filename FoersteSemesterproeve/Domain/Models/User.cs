
namespace FoersteSemesterproeve.Domain.Models
{
    /// <summary>
    ///     User class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class User
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string city;
        public string address;
        public int? postal;
        
        public DateOnly dateofBirth;
        public bool isCoach;
        public bool isAdmin;

        public string password;
        
        public string isCoachText { get; set; }
        public string isAdminText { get; set; }
        
        public List<Activity> activityList;
        
        public MembershipType membershipType { get; set; }

        /// <summary>
        ///     Constructor til User class
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="password"></param>
        /// <param name="isCoach"></param>
        /// <param name="isAdmin"></param>
        /// <param name="dateofBirth"></param>
        /// <param name="postal"></param>
        /// <param name="membershipType"></param>
        public User(int id, string firstName, string lastName, string email, string address, string city, string password, bool isCoach, bool isAdmin, DateOnly dateofBirth, int? postal, MembershipType membershipType) 
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.city = city;
            this.password = password;
            this.isCoach = isCoach;
            this.isAdmin = isAdmin;
            this.dateofBirth = dateofBirth;
            this.postal = postal;
            this.isCoachText = SetMark(isCoach);
            this.isAdminText = SetMark(isAdmin);
            this.activityList = new List<Activity>();
            this.membershipType = membershipType;
        }

        /// <summary>
        ///     Funktionen bruges til at returnere et unicode symbol, baseret på bool parameteren
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="statement"></param>
        /// <returns></returns>
        private string SetMark(bool statement)
        {
            if(statement)
            {
                //return "Yes";
                return "✔";
            }
            else
            {
                //return "No";
                return "❌";
            }
        }

        /// <summary>
        ///     Funktionen bruges til at sætte unicode symboler i brugerens isCoachText og isAdminText, 
        ///     efter brugeren er blevet redigeret i, i EditUserPage.
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        public void CheckBothMarks()
        {
            this.isCoachText = SetMark(isCoach);
            this.isAdminText = SetMark(isAdmin);
        }
    }
}
