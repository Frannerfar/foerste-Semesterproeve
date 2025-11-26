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

            membershipTypes.Add(new MembershipType());
            membershipTypes.Add(new MembershipType());
            membershipTypes.Add(new MembershipType());
            membershipTypes.Add(new MembershipType());
            membershipTypes.Add(new MembershipType());

            return membershipTypes;
        }
    }
}
    