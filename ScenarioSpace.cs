using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class ScenarioSpace: Scenario
    {
        private string starship2;
        /// <summary>
        /// another don't get mad at me c# function
        /// </summary>
        public ScenarioSpace()
        {
        }
        /// <summary>
        /// constructor based on user inputs
        /// </summary>
        /// <param name="person">inputed person</param>
        /// <param name="planet">inputed planet</param>
        /// <param name="starship">inputed starship</param>
        /// <param name="starshipTwo">inputed second starship (usually random)</param>
        public ScenarioSpace(string person, string planet, string starship, string starshipTwo)
        {
            person1 = person;
            planet1= planet;
            starships1 = starship;
            starship2 = starshipTwo;
        }
        /// <summary>
        /// Generates a scenario based on a random number and the values of the class
        /// </summary>
        /// <returns>A randomly generated scenario (as a string)</returns>
        public override string GenerateScenario()
        {
            string scenario = string.Empty;
            Random rnd = new Random();
            int randomScenario = rnd.Next(1, 3);
            if (randomScenario == 1)
            {
                scenario = person1 + " flew " + starships1 + " above " + planet1 + ", and got into a dogfight against "+ starship2;
            }
            else if (randomScenario == 2)
            {
                scenario = person1 + " flew " + starships1 + " above " + planet1 + ", and burned up in the atmosphere"; 
            }
            else if (randomScenario == 3)
            {
                scenario = person1 + " flew " + starships1 + " above " + planet1 + ", and got chased down by " + starship2 + " until they hid in an asteroid";
            }
            return scenario;
        }
    }
}
