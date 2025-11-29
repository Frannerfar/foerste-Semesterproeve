using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace FoersteSemesterproeve.Domain.Models
{
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
        public bool hasPaid;

        public string password;
        
        public string isCoachText { get; set; }
        public string isAdminText { get; set; }
        
        public List<Activity> activityList;
        
        public MembershipType membershipType { get; set; }

        public Gender gender;

        public User(int id, string firstName, string lastName, string email, string address, string city, string password, bool isCoach, bool isAdmin, DateOnly dateofBirth, int? postal, bool hasPaid, MembershipType membershipType, Gender gender) 
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
            this.hasPaid = hasPaid;
            this.isCoachText = SetMark(isCoach);
            this.isAdminText = SetMark(isAdmin);
            this.activityList = new List<Activity>();
            this.membershipType = membershipType;
            this.gender = gender;

        }

        public enum Gender
        {
            Male,
            Female
        }

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

        public void CheckBothMarks()
        {
            this.isCoachText = SetMark(isCoach);
            this.isAdminText = SetMark(isAdmin);
        }
    }
}
