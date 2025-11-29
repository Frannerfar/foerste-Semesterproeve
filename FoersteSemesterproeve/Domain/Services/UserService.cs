using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoersteSemesterproeve.Data.Repositories;
using FoersteSemesterproeve.Domain.Interfaces;
using FoersteSemesterproeve.Domain.Models;

namespace FoersteSemesterproeve.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Martin</author>
    /// <created>26-11-2025</created>
    /// <updated>29-11-2025</updated>
    public class UserService
    {
        // Kunne flyttes til en AuthService, hvis vi udbyggede Authentication i applikationen
        public User? authenticatedUser;

        public User? targetUser;
        public List<User> users;
        public MembershipService membershipService;

        private IUserRepository userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="membershipService"></param>
        /// <param name="userRepository"></param>
        public UserService(MembershipService membershipService, IUserRepository userRepository) 
        {
            this.membershipService = membershipService;

            this.userRepository = userRepository;

            users = populateUsers();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>28-11-2025</updated>
        /// <returns></returns>
        private List<User> populateUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User(1, "Admin", "Adminsen", "admin@admin.dk", "", "", "admin", false, true, new DateOnly(2000, 1, 1), 0, false, membershipService.membershipTypes[0], User.Gender.Female));
            users.Add(new User(2, "Member", "Membersen", "member@membersen.dk", "Rolighedsvej 12", "Aalborg", "1234", false, false, new DateOnly(1990, 7, 21), 9000, true, membershipService.membershipTypes[1], User.Gender.Male));
            users.Add(new User(3, "Hans", "Eriksen", "hans@eriksenisthebest.dk", "Søndergade 16", "Silkeborg", "password", true, false, new DateOnly(1960, 6, 10), 8600, true, membershipService.membershipTypes[1], User.Gender.Male));
            users.Add(new User(4, "Ole", "Henriksen", "kontakt@olehenriksen.dk", "Hollywoodgade 90210", "Califorstrup", "OleErSej", false, false, new DateOnly(1958, 9, 20), 90210, true, membershipService.membershipTypes[0], User.Gender.Male));
            users.Add(new User(5, "Mette", "Poulsen", "mette.poulsen@gmail.com","Nørregade 44", "Aarhus", "mette123", false, false, new DateOnly(1985, 3, 14), 8000, false, membershipService.membershipTypes[2], User.Gender.Female));
            users.Add(new User(6, "Kasper", "Nielsen", "kasper.nielsen@hotmail.com", "Parkvej 72", "Herning", "kasperPass", true, false, new DateOnly(1992, 11, 2), 7400, true, membershipService.membershipTypes[1], User.Gender.Male));
            users.Add(new User(7, "Louise", "Andersen", "louise.andersen@live.dk", "Vestergade 8", "Viborg", "loui2025", false, true, new DateOnly(1976, 1, 28), 8800, true, membershipService.membershipTypes[1], User.Gender.Female));
            users.Add(new User(8, "Bertel", "Haarder", "saaspoergforsatan@gmail.com","Enghavevej 33", "Odense", "GoHaarder!", true, false, new DateOnly(1988, 2, 5), 5000, false, membershipService.membershipTypes[0], User.Gender.Male));
            users.Add(new User(9, "Thomas", "Kristensen", "thomas.kristensen@live.dk","Bakkevej 8", "Randers", "thomas75", false, true, new DateOnly(1975, 12, 17), 8900, true, membershipService.membershipTypes[0], User.Gender.Male));
            users.Add(new User(10, "Sofie", "Mortensen", "sofie.mortensen@hotmail.com", "Stationsvej 5", "Skanderborg", "sofiePW", true, true, new DateOnly(1996, 4, 9), 8660, true, membershipService.membershipTypes[0], User.Gender.Female));

            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool isUserPasswordCorrect(User user, string password)
        {
            if (password == user.password)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>27-11-2025</updated>
        /// <param name="user"></param>
        public void DeleteUserByObject(User user)
        {
            users.Remove(user);
            // TODO: Fjern alle referencer
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="city"></param>
        /// <param name="address"></param>
        /// <param name="date"></param>
        /// <param name="postal"></param>
        /// <param name="isAdmin"></param>
        /// <param name="isCoach"></param>
        /// <param name="hasPaid"></param>
        /// <param name="membershipType"></param>
        /// <param name="gender"></param>
        public void AddUser(string firstName, string lastName, string email, string city, string address, DateOnly date, int? postal, bool isAdmin, bool isCoach, bool hasPaid, MembershipType membershipType, User.Gender gender)
        {
            int newId = userRepository.GetNewId(this.users);
            this.users.Add(new User(newId, firstName, lastName, email, address, city, "1234", isCoach, isAdmin, date, postal, hasPaid, membershipType, gender));
            userRepository.SaveUsers(users);
        }
    }
}
