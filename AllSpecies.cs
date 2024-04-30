using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class AllSpecies
    {
        public int count { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
        public List<AllSpecies> results { get; set; }
    }
}
