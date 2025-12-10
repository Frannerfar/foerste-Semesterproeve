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
        public MembershipType? targetMembershipType;
        public List<MembershipType> membershipTypes;

        public MembershipService()
        {
            membershipTypes = PopulateMembershipTypes();
        }



        private List<MembershipType> PopulateMembershipTypes()
        {
            List<MembershipType> membershipTypes = new List<MembershipType>();

            membershipTypes.Add(new MembershipType(1, "NONE", 0, 0));
            membershipTypes.Add(new MembershipType(2, "STANDARD", 200, 2400));
            membershipTypes.Add(new MembershipType(3, "PRO", 400, 4800));
            membershipTypes.Add(new MembershipType(4, "ULTRA PRO", 1000, 12000));

            return membershipTypes;
        }

        public int GetNewId(List<MembershipType> membershipTypesInput)
        {
            int highestId = 0;
            for (int i = 0; i < membershipTypesInput.Count; i++)
            {
                if (membershipTypesInput[i].id > highestId)
                {
                    highestId = membershipTypesInput[i].id;
                }
            }
            highestId++;
            return highestId;
        }

        public void AddMembershipType(string title, int monthly, int yearly)
        {
            this.membershipTypes.Add(new MembershipType(GetNewId(membershipTypes), title, monthly, yearly));
        }

        public void DeleteMembershipTypeByObject(MembershipType membershipType)
        {
            membershipTypes.Remove(membershipType);
        }
    }
}
    