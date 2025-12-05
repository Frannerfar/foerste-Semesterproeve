using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoersteSemesterproeve.Domain.Models
{
    public class MembershipType
    {
        public string name;
        public int monthlyPayDKK;
        public int yearlyPayDKK;

        public MembershipType(string nameInput, int monthlyPayDKKInput, int yearlyPayDKKInput)
        {
            name = nameInput;
            monthlyPayDKK = monthlyPayDKKInput;
            yearlyPayDKK = yearlyPayDKKInput;
        }
    }
}
