using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class ScenarioGround:Scenario
    {
        private string person2;
        /// <summary>
        /// another c# don't get mad function
        /// </summary>
        public ScenarioGround()
        {

        }
        /// <summary>
        /// cunstructor that defines inherited and unique values of the class based on inputs
        /// </summary>
        /// <param name="person">inputed person</param>
        /// <param name="planet">inputed planet</param>
        /// <param name="starship">inputed starship</param>
        /// <param name="anotherPerson">inputed second person (usually random)</param>
        public ScenarioGround(string person, string planet, string starship, string anotherPerson)
        {
            person1 = person;
            person2 = anotherPerson;
            planet1= planet;
            starships1 = starship;
        }
        /// <summary>
        /// generates a scenario string based on a random value
        /// </summary>
        /// <returns>a random scenario using the values of the class</returns>
        public override string GenerateScenario()
        {
            string scenario=string.Empty;
            Random rnd = new Random();
            int randomScenario = rnd.Next(1, 3);
            if (randomScenario == 1)
            {
                scenario = person1 + " flew " + starships1 + " to " + planet1 + ", and got a drink at local bar with " + person2;
            }
            else if (randomScenario == 2)
            {
                scenario = person1 + " flew " + starships1 + " to " + planet1 + ", and got into a fight with " + person2 + " who he found in a cave";
            }
            else if (randomScenario == 3)
            {
                scenario = person1 + " flew " + starships1 + " to " + planet1 + ", and crash landed";
            }
            return scenario;
        }
    }
}
