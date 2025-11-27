using FoersteSemesterproeve.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Services
{
    public class MembershipService
    {
        public List<MembershipType> membershipTypes;

        public MembershipService()
        {
            membershipTypes = populateMembershipTypes();
        }



        private List<MembershipType> populateMembershipTypes()
        {
            List<MembershipType> membershipTypes = new List<MembershipType>();

            membershipTypes.Add(new MembershipType("NONE", 38326, 78376546));
            membershipTypes.Add(new MembershipType("ishdfuihs", 982347, 2837));
            membershipTypes.Add(new MembershipType("bsb", 344, 445));
            membershipTypes.Add(new MembershipType("ahfd", 283, 9586));
            membershipTypes.Add(new MembershipType("sdfkj", 7594, 1202834));

            return membershipTypes;
        }
    }
}
    