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
        public string address;
        public string city;
        public string password;
        public bool isCoach;
        public bool isAdmin;
        public string isCoachText { get; set; }
        public string isAdminText { get; set; }
        public DateOnly dateofBirth;
        public int postal;
        public bool hasPaid;
        //public List<Activity> list;
        //public MembershipType membershipType;

        public User(int id, string firstName, string lastName, string email, string address, string city, string password, bool isCoach, bool isAdmin, DateOnly dateofBirth, int postal, bool hasPaid) 
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
    }
}
