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

namespace FoersteSemesterproeve.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        string filepath = Path.Combine(Environment.CurrentDirectory, "Data", "Files", "users.json");
        MembershipService membershipService;

        public UserRepository(MembershipService membershipService) 
        {
            this.membershipService = membershipService;
        }

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

        public void SaveUsers(List<User> users) 
        {
            List<UserDto> userDtos = ConvertToDto(users);

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;

            string jsonText = JsonSerializer.Serialize(userDtos, jsonOptions);

            SaveFile(filepath, jsonText);
        }

        public int GetNewId(List<User> users)
        {
            int count = users.Count;
            int? id = users[count - 1].id;
            if (id.HasValue)
            {
                int tempId = id.Value + 1;
                while (true)
                {
                    if (users.Any(u => u.id == tempId))
                    {
                        tempId++;
                    }
                    else
                    {
                        return tempId;
                    }
                }
            }
            else
            {
                return (users.Count + 1);
            }
        }


        private List<UserDto> ConvertToDto(List<User> users)
        {
            List<UserDto> userDtos = users.Select(u => new UserDto 
            {
                Id = u.id,
                FirstName = u.firstName,
                LastName = u.lastName,
                Email = u.email,
                DateOfBirth = u.dateofBirth,
                City = u.city,
                Address = u.address,
                Postal = u.postal,
                IsCoach = u.isCoach,
                IsAdmin = u.isAdmin,
                HasPaid = u.hasPaid,
                Password = u.password,
                // TODO: Mangler stadig aktiviteter og ID til aktiviteter
                //       Tilføjes når Rasmus har færdigjort aktiviteter
                MembershipTypeId = u.membershipType.id,
                Gender = (int)u.gender
            }).ToList();

            return userDtos;
        }

        private List<User> ConvertFromDto(List<UserDto> dtos, List<MembershipType> membershipTypes)
        {
            List<User> users = dtos.Select(d => new User(
                d.Id,
                d.FirstName,
                d.LastName,
                d.Email,
                d.Address,
                d.City,
                d.Password,
                d.IsCoach,
                d.IsAdmin,
                d.DateOfBirth,
                d.Postal,
                d.HasPaid,
                membershipService.membershipTypes.First(m => m.id == d.MembershipTypeId),
                // TODO: Mangler stadig aktiviteter og ID til aktiviteter
                //       Tilføjes når Rasmus har færdigjort aktiviteter
                (User.Gender)d.Gender)).ToList();

            return users;
        }


        private string LoadFile(string path)
        {
            string text = File.ReadAllText(path);
            Debug.WriteLine("File read!");
            Debug.WriteLine($"File path: {path}");
            Debug.WriteLine(text);
            return text;
        }

        private void SaveFile(string path, string text)
        {
            File.WriteAllText(path, text);
            Debug.WriteLine("File saved!");
            Debug.WriteLine($"File path: {path}");
            Debug.WriteLine(text);
        }
    }
}
