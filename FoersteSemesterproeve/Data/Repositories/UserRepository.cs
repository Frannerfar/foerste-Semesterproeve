using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FoersteSemesterproeve.Data.DTO;
using FoersteSemesterproeve.Domain.Models;
using FoersteSemesterproeve.Domain.Services;
using FoersteSemesterproeve.Domain.Interfaces;
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
        string filepath = Path.Combine(Environment.CurrentDirectory, "Data", "Files", "users.json");
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
            string userJson = File.ReadAllText(filepath);

            if(!string.IsNullOrEmpty(userJson))
            {
                List<UserDto> userDtos = JsonSerializer.Deserialize<List<UserDto>>(userJson);
                if(userDtos != null)
                {
                    List<User> users = ConvertFromDto(userDtos, membershipService.membershipTypes);
                    return users;
                }
            }
            return new List<User>();
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
                    HasPaid = user.hasPaid,
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
                    dto.HasPaid,
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
