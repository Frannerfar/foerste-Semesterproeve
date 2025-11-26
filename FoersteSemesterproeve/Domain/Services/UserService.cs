using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Domain.Services
{
    internal class UserService
    {
        List<User> users;

        public UserService() 
        {
            users = populateUsers();
        }



        private List<User> populateUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User());
            users.Add(new User());
            users.Add(new User());
            users.Add(new User());
            users.Add(new User());

            return users;
        }
    }
}
