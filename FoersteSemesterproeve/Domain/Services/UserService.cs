using FoersteSemesterproeve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        string filepath = Path.Combine(Environment.CurrentDirectory, "Data", "Files", "users.txt");


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>26-11-2025</created>
        /// <updated>29-11-2025</updated>
        /// <param name="membershipService"></param>
        /// <param name="userRepository"></param>
        public UserService(MembershipService membershipService) 
        {
            this.membershipService = membershipService;

            users = LoadUsers();
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
            for(int i = 0; user.activityList.Count > 0; i++)
            {
                user.activityList[i].participants.Remove(user);
            }
            user.activityList.Clear();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <returns></returns>
        public List<User> LoadUsers()
        {
            string userText = File.ReadAllText(this.filepath);
            Debug.WriteLine("File read!");
            Debug.WriteLine($"File path: {this.filepath}");
            Debug.WriteLine(userText);

            List<User> users = new List<User>();

            string[] usersInTextFormat = userText.Split(";");

            for (int i = 0; i < usersInTextFormat.Length; i++)
            {
                try
                {
                    string[] userInformationParts = usersInTextFormat[i].Split(",");

                    string stringID = userInformationParts[0];
                    string stringFirstName = userInformationParts[1];
                    string stringLastName = userInformationParts[2];
                    string stringEmail = userInformationParts[3];
                    string stringStreet = userInformationParts[4];
                    string stringCity = userInformationParts[5];
                    string stringPassword = userInformationParts[6];
                    string stringIsCoach = userInformationParts[7];
                    string stringIsAdmin = userInformationParts[8];
                    string stringDOB = userInformationParts[9];
                    string stringPostal = userInformationParts[10];
                    string stringMembershipID = userInformationParts[11];

                    Debug.WriteLine($"ITERATION: {i}");

                    // Til at tjekke om noget er fejlet
                    bool flag = false;

                    // Konverterer stringID om til en faktisk integer ID.
                    int id;
                    bool isUserID = int.TryParse(stringID, out id);
                    if (!isUserID) { MessageBox.Show($"UserID {stringID} could not be converted to a integer"); flag = true; }

                    // Konverterer stringIsCoach til faktisk bool.
                    bool isCoach;
                    bool isUserCoachBool = bool.TryParse(stringIsCoach, out isCoach);
                    if (!isUserCoachBool) { MessageBox.Show($"isCoach bool: {stringIsCoach} could not be converted to a bool"); flag = true; }

                    // Konverterer stringIsAdmin til faktisk bool.
                    bool isAdmin;
                    bool isUserAdminBool = bool.TryParse(stringIsAdmin, out isAdmin);
                    if (!isUserAdminBool) { MessageBox.Show($"isAdmin bool: {stringIsAdmin} could not be converted to a bool"); flag = true; }

                    DateOnly dob;
                    //bool isUserDOB = DateOnly.TryParse(stringDOB, out dob);
                    bool isUserDOB = DateOnly.TryParseExact(stringDOB, "d-M-yyyy", out dob);

                    if (!isUserDOB) { MessageBox.Show($"Date of Birth DateOnly: {stringDOB} could not be converted to a DateOnly"); flag = true; }

                    int postal;
                    int? postalNullable = null;
                    if (int.TryParse(stringPostal, out postal))
                    {
                        postalNullable = postal;
                    }
                    else
                    {
                        MessageBox.Show($"Postal integer: {stringPostal} could not be converted to a integer");
                        flag = true;
                    }

                    int membershipTypeId;
                    bool isMembershipTypeID = int.TryParse(stringMembershipID, out membershipTypeId);
                    if (!isMembershipTypeID) { MessageBox.Show($"MembershipTypeID {stringMembershipID} could not be converted to a integer, from userID: {id}"); flag = true; }

                    // Hvis 
                    if (!flag)
                    {
                        MembershipType? membershipType = null;
                        for (int j = 0; j < membershipService.membershipTypes.Count; j++)
                        {
                            if (membershipService.membershipTypes[j].id == membershipTypeId)
                            {
                                membershipType = membershipService.membershipTypes[j];
                            }
                        }
                        if (membershipType != null)
                        {
                            User user = new User(id, stringFirstName, stringLastName, stringEmail, stringStreet, stringCity, stringPassword, isCoach, isAdmin, dob, postal, membershipType);
                            users.Add(user);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show($"ERROR in iteration #{i} \n\n RAW DATA:\n {usersInTextFormat[i]} \n\n EXCEPTION: \n {e.Message}");
                    continue;
                }
                //DialogBox dialogBox = new DialogBox($"stringID: {stringID} - stringFirstName: {stringFirstName} - stringLastName: {stringLastName} - stringEmail: {stringEmail} - stringStreet: {stringStreet} - stringCity: {stringCity} - stringPassword: {stringPassword} - stringIsCoach: {stringIsCoach} - stringIsAdmin: {stringIsAdmin} - stringDOB: {stringDOB} - stringPostal: {stringPostal} - stringMembershipID: {stringMembershipID}");
                //dialogBox.ShowDialog();
            }
            return users;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="users"></param>
        /// <returns></returns>
        public int GetNewId(List<User> users)
        {
            int highestId = 0;
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].id > highestId)
                {
                    highestId = users[i].id;
                }
            }
            highestId++;
            return highestId;
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
        public void AddUser(string firstName, string lastName, string email, string city, string address, DateOnly date, int? postal, bool isAdmin, bool isCoach, bool hasPaid, MembershipType membershipType)
        {
            int newId = this.GetNewId(this.users);
            this.users.Add(new User(newId, firstName, lastName, email, address, city, "1234", isCoach, isAdmin, date, postal, membershipType));
        }
    }
}
