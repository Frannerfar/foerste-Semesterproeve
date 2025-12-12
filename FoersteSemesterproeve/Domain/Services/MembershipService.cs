using FoersteSemesterproeve.Domain.Models;


namespace FoersteSemesterproeve.Domain.Services
{

    /// <summary>
    ///     MembershipService class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class MembershipService
    {
        public MembershipType? targetMembershipType;
        public List<MembershipType> membershipTypes;


        /// <summary>
        ///     Constructor til MembershipService
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        public MembershipService()
        {
            // Constructoren har ingen parametre, men kalder derimod ved instantiering metoden "PopulateMembershipTypes"
            // PopulateMembershipTypes returner en liste af medlemsskaber, List<MembershipType> og sættes i fields "membershipTypes".
            membershipTypes = PopulateMembershipTypes();
        }


        /// <summary>
        ///     Bruges til at lave pre-definerede medlemsskaber ved opstart af programmet
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <returns></returns>
        private List<MembershipType> PopulateMembershipTypes()
        {
            // Der oprettes en variabel med navnet "membershipTypes" der modtager en ny instantiated liste af membershipTypes, List<MembershipType>
            List<MembershipType> membershipTypes = new List<MembershipType>();

            // Der instantieres nyt MembershipType objekt og tilføjes direkte til listen af medlemsskaber "membershipTypes".
            // Dette gøres 4 gange
            membershipTypes.Add(new MembershipType(1, "NONE", 0, 0));
            membershipTypes.Add(new MembershipType(2, "STANDARD", 200, 2400));
            membershipTypes.Add(new MembershipType(3, "PRO", 400, 4800));
            membershipTypes.Add(new MembershipType(4, "ULTRA PRO", 1000, 12000));

            // funktionen returnerer listen af medlemsskaber
            return membershipTypes;
        }

        /// <summary>
        ///     Bruges til at få næste ID i rækken, ud fra de eksisterende MembershipType objekter i parametereren "membershipTypesInput".
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="membershipTypesInput"></param>
        /// <returns></returns>
        public int GetNewId(List<MembershipType> membershipTypesInput)
        {
            // int variabel sættes til 0
            int highestId = 0;
            // Looper igennem antallet af medlemsskaber i listen
            for (int i = 0; i < membershipTypesInput.Count; i++)
            {
                // Hvis medlemsskabets ID er højere end variablen "highestId"
                if (membershipTypesInput[i].id > highestId)
                {
                    // Sæt variablen "highestId" til at have værdien fra loopets nuværende iteration af medlemsskabs ID
                    highestId = membershipTypesInput[i].id;
                }
            }
            // Tilføj 1 til variablen, da vi gerne vil have næste ID og ikke bare det højeste ID.
            highestId++;
            // returnerer det variablen "highestId", som nu repræsenterer det næste ID til brug i oprettelse
            return highestId;
        }

        /// <summary>
        ///     Bruges til at tilføje nyt medlemsskab
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="title"></param>
        /// <param name="monthly"></param>
        /// <param name="yearly"></param>
        public void AddMembershipType(string title, int monthly, int yearly)
        {
            // Der instantieres nyt MembershipType objekt og tilføjes direkte til listen af MembershipType "membershipTypes".
            this.membershipTypes.Add(new MembershipType(GetNewId(membershipTypes), title, monthly, yearly));
        }


        /// <summary>
        ///     Bruges til at fjerne et medlemsskab fra listen af medlemsskaber
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="membershipType"></param>
        public void DeleteMembershipTypeByObject(MembershipType membershipType)
        {
            // parameterern membershipType fjernes fra listen membershipTypes
            membershipTypes.Remove(membershipType);
        }
    }
}
    