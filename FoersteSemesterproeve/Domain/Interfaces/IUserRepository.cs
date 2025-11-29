using FoersteSemesterproeve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Interfaces
{
    public interface IUserRepository
    {
        List<User> LoadUsers();
        void SaveUsers(List<User> users);
        int GetNewId(List<User> existingUsers);
    }
}
