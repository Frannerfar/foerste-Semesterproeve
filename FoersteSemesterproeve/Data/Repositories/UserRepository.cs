using FoersteSemesterproeve.Data.DTO;
using FoersteSemesterproeve.Domain.Interfaces;
using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FoersteSemesterproeve.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Martin</author>
    /// <created>29-11-2025</created>
    public class UserRepository : IUserRepository
    {
        string filepath = Path.Combine(Environment.CurrentDirectory, "Data", "Files", "users.txt");
        MembershipService membershipService;

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="membershipService"></param>
        public UserRepository(MembershipService membershipService) 
        {
            this.membershipService = membershipService;
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

            for(int i = 0;  i < usersInTextFormat.Length; i++)
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
                    if(!isUserID) { MessageBox.Show($"UserID {stringID} could not be converted to a integer"); flag = true; }

                    // Konverterer stringIsCoach til faktisk bool.
                    bool isCoach;
                    bool isUserCoachBool = bool.TryParse(stringIsCoach, out isCoach);
                    if(!isUserCoachBool) { MessageBox.Show($"isCoach bool: {stringIsCoach} could not be converted to a bool"); flag = true; }

                    // Konverterer stringIsAdmin til faktisk bool.
                    bool isAdmin;
                    bool isUserAdminBool = bool.TryParse(stringIsAdmin, out isAdmin);
                    if (!isUserAdminBool) { MessageBox.Show($"isAdmin bool: {stringIsAdmin} could not be converted to a bool"); flag = true; }

                    DateOnly dob;
                    bool isUserDOB = DateOnly.TryParse(stringDOB, out dob);
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
                        for(int j = 0; j < membershipService.membershipTypes.Count; j++)
                        {
                            if(membershipService.membershipTypes[j].id == membershipTypeId)
                            {
                                membershipType = membershipService.membershipTypes[j];
                            }
                        }
                        if (membershipType != null) 
                        { 
                            User user = new User(id, stringFirstName, stringLastName, stringEmail, stringStreet, stringCity, stringPassword,isCoach, isAdmin, dob, postal, membershipType);
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
        public void SaveUsers(List<User> users) 
        {
            List<UserDto> userDtos = ConvertToDto(users);

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;

            string jsonText = JsonSerializer.Serialize(userDtos, jsonOptions);

            SaveFile(filepath, jsonText);
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
        /// <created>29-11-2025</created>
        /// <param name="users"></param>
        /// <returns></returns>
        private List<UserDto> ConvertToDto(List<User> users)
        {
            List<UserDto> userDtos = new List<UserDto>();

            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];

                UserDto dto = new UserDto
                {
                    Id = user.id,
                    FirstName = user.firstName,
                    LastName = user.lastName,
                    Email = user.email,
                    DateOfBirth = user.dateofBirth,
                    City = user.city,
                    Address = user.address,
                    Postal = user.postal,
                    IsCoach = user.isCoach,
                    IsAdmin = user.isAdmin,
                    //HasPaid = user.hasPaid,
                    Password = user.password,
                    MembershipTypeId = user.membershipType.id,
                    // TODO: Mangler stadig aktiviteter og ID til aktiviteter 
                    //       Tilføjes når Rasmus har færdigjort aktiviteter
                };

                userDtos.Add(dto);
            }

            return userDtos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="dtos"></param>
        /// <param name="membershipTypes"></param>
        /// <returns></returns>
        private List<User> ConvertFromDto(List<UserDto> dtos, List<MembershipType> membershipTypes)
        {
            List<User> users = new List<User>();

            for (int i = 0; i < dtos.Count; i++)
            {
                UserDto dto = dtos[i];

                MembershipType membership = null;

                for (int j = 0; j < membershipTypes.Count; j++)
                {
                    if (membershipTypes[j].id == dto.MembershipTypeId)
                    {
                        membership = membershipTypes[j];
                        break;
                    }
                }

                if (membership == null)
                {
                    MessageBox.Show($"MembershipType with ID {dto.MembershipTypeId} not found.");
                }

                User user = new User(
                    dto.Id,
                    dto.FirstName,
                    dto.LastName,
                    dto.Email,
                    dto.Address,
                    dto.City,
                    dto.Password,
                    dto.IsCoach,
                    dto.IsAdmin,
                    dto.DateOfBirth,
                    dto.Postal,
                    //dto.HasPaid,
                    membership
                    // TODO: Mangler stadig aktiviteter og ID til aktiviteter
                    //       Tilføjes når Rasmus har færdigjort aktiviteter
                );

                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="path"></param>
        /// <returns></returns>
        private string LoadFile(string path)
        {
            string text = File.ReadAllText(path);
            Debug.WriteLine("File read!");
            Debug.WriteLine($"File path: {path}");
            Debug.WriteLine(text);
            return text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        /// <param name="path"></param>
        /// <param name="text"></param>
        private void SaveFile(string path, string text)
        {
            File.WriteAllText(path, text);
            Debug.WriteLine("File saved!");
            Debug.WriteLine($"File path: {path}");
            Debug.WriteLine(text);
        }
    }
}
