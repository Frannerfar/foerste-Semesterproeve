
namespace FoersteSemesterproeve.Domain.Models
{
    /// <summary>
    ///     MembershipType class
    /// </summary>
    /// <author>Rasmus, Marcus, Martin</author>
    public class MembershipType
    {
        public int id;
        public string name { get; set; }
        public int monthlyPayDKK;
        public int yearlyPayDKK;

        /// <summary>
        ///     Constructor til MembershipType class
        /// </summary>
        /// <author>Rasmus, Marcus, Martin</author>
        /// <param name="id"></param>
        /// <param name="nameInput"></param>
        /// <param name="monthlyPayDKKInput"></param>
        /// <param name="yearlyPayDKKInput"></param>
        public MembershipType(int id, string nameInput, int monthlyPayDKKInput, int yearlyPayDKKInput)
        {
            this.id = id;
            this.name = nameInput;
            this.monthlyPayDKK = monthlyPayDKKInput;
            this.yearlyPayDKK = yearlyPayDKKInput;
        }
    }
}
