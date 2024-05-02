using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Scenario
    {
        protected string person1;
        protected string starships1;
        protected string planet1;
        /// <summary>
        /// pointless function but exists so that visual studio/c# doesn't get mad that I don't have a constructer with 0 arguments
        /// </summary>
        public Scenario()
        {
        }
        /// <summary>
        /// sets the class values based on selection inputed 
        /// </summary>
        /// <param name="person">inputed person</param>
        /// <param name="planet">inputed planet</param>
        /// <param name="starship">inputed starship</param>
        public Scenario(string person, string planet, string starship)
        {
            person1 = person;
            starships1 = starship;
            planet1 = planet;   
        }
        /// <summary>
        /// Program that exists kind of for the sole purpose of being inherited and running if you forget to put scenarios into any inherited classes for some reason
        /// </summary>
        /// <returns>empty scenario</returns>
        public virtual string GenerateScenario()
        {
            string generatedScenario = "Scenario Goes here";
            return generatedScenario;
        }
    }
}
