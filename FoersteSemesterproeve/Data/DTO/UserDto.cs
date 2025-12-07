using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Data.DTO
{
    /// <summary>
    /// 
    /// </summary>
    /// <author>Martin</author>
    /// <created>29-11-2025</created>
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Email { get; set; }


        public string City { get; set; }
        public string Address { get; set; }
        public int? Postal { get; set; }



        public DateOnly DateOfBirth { get; set; }



        public bool IsCoach { get; set; }
        public bool IsAdmin { get; set; }
        public bool HasPaid { get; set; }



        public string Password { get; set; }




        public int MembershipTypeId { get; set; }
        public List<int> ActivityIds { get; set; }



        /// <summary>
        /// 
        /// </summary>
        /// <author>Martin</author>
        /// <created>29-11-2025</created>
        public UserDto()
        {

        }
    }
}
