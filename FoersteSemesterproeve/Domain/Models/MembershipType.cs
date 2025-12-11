
namespace FoersteSemesterproeve.Domain.Models
{
    public class MembershipType
    {
        public int id;
        public string name { get; set; }
        public int monthlyPayDKK;
        public int yearlyPayDKK;

        public MembershipType(int id, string nameInput, int monthlyPayDKKInput, int yearlyPayDKKInput)
        {
            this.id = id;
            this.name = nameInput;
            this.monthlyPayDKK = monthlyPayDKKInput;
            this.yearlyPayDKK = yearlyPayDKKInput;
        }
    }
}
