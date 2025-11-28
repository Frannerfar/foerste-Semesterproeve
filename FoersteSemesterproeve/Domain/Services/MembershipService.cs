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
        public MembershipType targetMembershipType;
        public List<MembershipType> membershipTypes;

        public MembershipService()
        {
            membershipTypes = populateMembershipTypes();
        }



        private List<MembershipType> populateMembershipTypes()
        {
            List<MembershipType> membershipTypes = new List<MembershipType>();

            membershipTypes.Add(new MembershipType("NONE", 0, 0));
            membershipTypes.Add(new MembershipType("STANDARD", 200, 2400));
            membershipTypes.Add(new MembershipType("PRO", 400, 4800));
            membershipTypes.Add(new MembershipType("ULTRA PRO", 1000, 12000));

            return membershipTypes;
        }

        public void AddMembershipType(string title, int monthly, int yearly)
        {
            this.membershipTypes.Add(new MembershipType(title, monthly, yearly));
        }

        public void DeleteMembershipTypeByObject(MembershipType membershipType)
        {
            membershipTypes.Remove(membershipType);
        }
    }
}
    