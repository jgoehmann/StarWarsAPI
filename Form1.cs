using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Tests to see if user entered a usable id and if so runs GetPlanet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetPlanetbtn_Click(object sender, EventArgs e)
        {
            try {
                int test=Int32.Parse(GetPlanettxb.Text);
                if (test > 0)
                {
                    //a quick fix for if the user enters a number greater than whats in the database, in order to make this more
                    //expandable if the database gets updated you could possibly introduce an error check later down the line when 
                    //the planet itself is defined
                    if (test - 61 <= 0)
                    {
                        GetPlanet(GetPlanettxb.Text);
                    }
                    else
                    {
                        MessageBox.Show("A Planet does not exist for the specified ID");
                    }
                }
                else
                {
                    MessageBox.Show("A Planet does not exist for the specified ID");
                }
                
            }
            catch
            {
                MessageBox.Show("Please enter a number");
            }
            
        }
      /// <summary>
      /// Tests to see if user entered a usable id and if so runs GetPerson
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void GetPersonbtn_Click(object sender, EventArgs e)
        {
            try
            {
                int test= Int32.Parse(GetPersontxb.Text);
                if (test > 0) 
                {
                    //the same quick fix as the previous function
                    if (test - 89 <= 0)
                    {
                        GetPerson(GetPersontxb.Text);
                    }
                    else
                    {
                        MessageBox.Show("A Character does not exist for the specified ID");
                    }
                }
                else
                {
                    MessageBox.Show("A Character does not exist for the specified ID");
                }
            }
            catch
            {
                MessageBox.Show("Please enter a number");
            }
        }
        /// <summary>
        /// Runs GetAllSpecies on btn click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetSpeciesbtn_Click(object sender, EventArgs e)
        {
            GetAllSpecies();
        }
        /// <summary>
        /// runs a given id through JSONHelper and then uses the now defined class to set lbls equal to their associated values
        /// </summary>
        /// <param name="id">the text entered into the GetPlanettxb field</param>
        public async void GetPlanet(string id)
        {
            Planet p = await JSONHelper.GetPlanet(id);
            Nametxt.Text = p.name;
            Rotationtxt.Text = p.rotation_period;
            Orbitaltxt.Text = p.orbital_period;
            Diametertxt.Text = p.diameter;
            Climatetxt.Text = p.climate;
            Gravitytxt.Text = p.gravity;
            Terraintxt.Text = p.terrain;
            Watertxt.Text = p.surface_water;
            Populationtxt.Text = p.population;
        }
        /// <summary>
        /// runs a given id through  JSONHelper then uses the now defined class to set lbls equal to their associated value
        /// but since planets are given as a new link it runs them through their own JSONHelper to fetch their values
        /// </summary>
        /// <param name="id">the text endered into the GetPersontxb field</param>
        public async void GetPerson(string id)
        {
            Person p = await JSONHelper.GetPerson(id);
            PNametxt.Text= p.name;
            PHeighttxt.Text = p.height;
            PMasstxt.Text = p.mass;
            PSkintxt.Text = p.skin_color;
            PHairtxt.Text = p.hair_color;
            PEyetxt.Text = p.eye_color;
            PBirthtxt.Text = p.birth_year;
            PGendertxt.Text= p.gender;
            Planet planet = await JSONHelper.GetPlanet(p.homeworld);
            PHomeworldtxt.Text = planet.name;
            string starshipslist = string.Empty;
            foreach(string c in p.starships)
            {
                Starships starships= await JSONHelper.GetStarships(c);
                starshipslist += starships.name + ", ";
                
            }
            Starshipstxt.Text = starshipslist;
        }
        /// <summary>
        /// runs JSONHelper to get the link for all the species, then uses the link, runs it through JSONHelper to
        /// create a new class of the associated species and adds needed values to the listbox then moves on to the next species
        /// </summary>
        public async void GetAllSpecies()
        {
            AllSpecies a = await JSONHelper.GetAllSpecies();
            int count = 1;

            while (count <= a.count)
            {
                Species species = await JSONHelper.GetSpecies(count.ToString());
                string homeworldCallThing = string.Empty;
                if (species.homeworld == null)
                {
                    homeworldCallThing = "n/a";
                }
                else
                {
                    Planet homeworld = await JSONHelper.GetPlanet(homeworldCallThing);
                    homeworldCallThing= homeworld.name; 
                }
                Specieslbx.Items.Add(" Name: " + species.name + " Classification: " + species.classification + " Designation:" + species.designation + " Average Height: " + species.average_height + " Skin Colors: " + species.skin_colors + " Hair colors: " + species.hair_colors + " Eye colors: " + species.eye_colors + " Average Lifespan: " + species.average_lifespan + " Homeworlds: " + homeworldCallThing + " Language: " + species.language);
                count++;
            }
        }
        /// <summary>
        /// This function basically runs an algorithm to check if the user has entered any ids into the generate scenario fields and yells at them if they entered anything that isn't a number, then chooses a scenario to run
        /// depending on what the user input, I'd say it would be better code to run the user input checks as the unique itemms being thrown into a random scenario are generated that way if the scenarios are expanded
        /// to include more items, it woudln't be neccessary to revamp the entire userinput check and create whole new functions to suit the new scenarios but method I used allowed me to incorporate polymorphism which was 
        /// required for the assignment 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenarioGeneratebtn_Click(object sender, EventArgs e)
        {
            if (ScenarioChoicebx.SelectedIndex == 0)
            {
                if (CharacterChoicetxb.Text.Length > 0)
                {
                    if(PlanetChoicetxb.Text.Length > 0)
                    {
                        try
                        {
                            int test = Int32.Parse(CharacterChoicetxb.Text);
                            int test2 = Int32.Parse(PlanetChoicetxb.Text);
                            GenerateSpaceScenario(CharacterChoicetxb.Text, PlanetChoicetxb.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Please enter a number for an ID");
                        }
                    }
                    else
                    {
                        try
                        {
                            int test = Int32.Parse(CharacterChoicetxb.Text);
                            GenerateSpaceScenario(CharacterChoicetxb.Text, 1);
                        }
                        catch
                        {
                            MessageBox.Show("Please enter a number for an ID");
                        }
                    }
                }
                else if(PlanetChoicetxb.Text.Length> 0)
                {
                    try
                    {
                        int test = Int32.Parse(PlanetChoicetxb.Text);
                        GenerateSpaceScenario(PlanetChoicetxb.Text, 2);
                    }
                    catch
                    {
                        MessageBox.Show("Please enter a number for an ID");
                    }
                }
                else
                {
                    GenerateSpaceScenario();
                }
            }else if (ScenarioChoicebx.SelectedIndex == 1)
            {
                if (CharacterChoicetxb.Text.Length > 0)
                {
                    if (PlanetChoicetxb.Text.Length > 0)
                    {
                        try
                        {
                            int test = Int32.Parse(CharacterChoicetxb.Text);
                            int test2 = Int32.Parse(PlanetChoicetxb.Text);
                            GenerateGroundScenario(CharacterChoicetxb.Text, PlanetChoicetxb.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Please enter a number for an ID");
                        }
                    }
                    else
                    {
                        try
                        {
                            int test = Int32.Parse(CharacterChoicetxb.Text);
                            GenerateGroundScenario(CharacterChoicetxb.Text, 1);
                        }
                        catch
                        {
                            MessageBox.Show("Please enter a number for an ID");
                        }
                    }
                }
                else if (PlanetChoicetxb.Text.Length > 0)
                {
                    try
                    {
                        int test = Int32.Parse(PlanetChoicetxb.Text);
                        GenerateGroundScenario(PlanetChoicetxb.Text, 2);
                    }
                    catch
                    {
                        MessageBox.Show("Please enter a number for an ID");
                    }
                }
                else
                {
                    GenerateGroundScenario();
                }
            }
        }
        /// <summary>
        /// Since there is no user input, this function generates random values to generate each required item for the scenario, for the first Starship it will try to generate one based on the person that is chosen but if
        /// no such starships exist it will generate a random one. The values are then sent the Ground Secenario class to generate a random scenario which is returned to this function which changes the form with the new scenario
        /// </summary>
        public async void GenerateGroundScenario()
        {
            Random random= new Random();
            int randomPerson = random.Next(1, 89);
            int randomPlanet = random.Next(1,63);
            int randomStarship = random.Next(1, 43);
            int randomPerson2 = random.Next(1, 89);
            Person person = await JSONHelper.GetPerson(randomPerson.ToString());
            Person person2= await JSONHelper.GetPerson(randomPerson2.ToString());
            Planet planet = await JSONHelper.GetPlanet(randomPlanet.ToString());
            string starship= string.Empty;
            foreach (string c in person.starships)
            {
                Starships starships = await JSONHelper.GetStarships(c);
                starship = starships.name;
            }
            if (starship.Length < 1)
            {
                Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                starship = starships.name;
            }
            ScenarioGround scenarioGround = new ScenarioGround(person.name,planet.name,starship, person2.name);
            GeneratedScenariolbl.Text = scenarioGround.GenerateScenario();
        }
        /// <summary>
        /// This function accomplishes the same as the one before it but since the user has entered both inputs the first person and planet are not randomly generated
        /// </summary>
        /// <param name="person">the user inputed person</param>
        /// <param name="planet">the user inputed planet</param>
        public async void GenerateGroundScenario(string person, string planet)
        {
            Random random = new Random();
            int randomPerson2 = random.Next(1,89);
            int randomStarship = random.Next(1, 43);
            Person person1 = await JSONHelper.GetPerson(person);
            Person person2= await JSONHelper.GetPerson(randomPerson2.ToString());
            Planet planet1 = await JSONHelper.GetPlanet(planet);
            string starship = string.Empty;
            foreach (string c in person1.starships)
            {
                Starships starships = await JSONHelper.GetStarships(c);
                starship = starships.name;
            }
            if (starship.Length < 1)
            {
                Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                starship = starships.name;
            }
            ScenarioGround scenarioGround = new ScenarioGround(person1.name, planet1.name, starship, person2.name);
            GeneratedScenariolbl.Text = scenarioGround.GenerateScenario();
        }
        /// <summary>
        /// This function handles if the user has only inputed and ID into one of the avaliable inputs
        /// </summary>
        /// <param name="personPlanet">The Person or planet that was inputed</param>
        /// <param name="personOrPlanet">If the previous value is a person this is a 1, if the previous value is a planet this is a 2</param>
        public async void GenerateGroundScenario(string personPlanet, int personOrPlanet)
        {
            if (personOrPlanet == 1) 
            {
                Random random = new Random();
                int randomPlanet = random.Next(1, 63);
                int randomStarship = random.Next(1, 43);
                int randomPerson2 = random.Next(1, 89);
                Person person = await JSONHelper.GetPerson(personPlanet);
                Person person2 = await JSONHelper.GetPerson(randomPerson2.ToString());
                Planet planet = await JSONHelper.GetPlanet(randomPlanet.ToString());
                string starship = string.Empty;
                foreach (string c in person.starships)
                {
                    Starships starships = await JSONHelper.GetStarships(c);
                    starship = starships.name;
                }
                if (starship.Length < 1)
                {
                    Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                    starship = starships.name;
                }
                ScenarioGround scenarioGround = new ScenarioGround(person.name, planet.name, starship, person2.name);
                GeneratedScenariolbl.Text = scenarioGround.GenerateScenario();
            }
            else
            {
                Random random = new Random();
                int randomPerson = random.Next(1, 89);
                int randomStarship = random.Next(1, 43);
                int randomPerson2 = random.Next(1, 89);
                Person person = await JSONHelper.GetPerson(randomPerson.ToString());
                Person person2 = await JSONHelper.GetPerson(randomPerson2.ToString());
                Planet planet = await JSONHelper.GetPlanet(personPlanet);
                string starship = string.Empty;
                foreach (string c in person.starships)
                {
                    Starships starships = await JSONHelper.GetStarships(c);
                    starship = starships.name;
                }
                if (starship.Length < 1)
                {
                    Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                    starship = starships.name;
                }
                ScenarioGround scenarioGround = new ScenarioGround(person.name, planet.name, starship, person2.name);
                GeneratedScenariolbl.Text = scenarioGround.GenerateScenario();
            }
        }
        /// <summary>
        /// This function is the same as the GenerateGroundScenario function that handles no user input but if the user selected a space scenario
        /// </summary>
        public async void GenerateSpaceScenario()
        {
            Random random = new Random();
            int randomPerson = random.Next(1, 89);
            int randomPlanet = random.Next(1, 63);
            int randomStarship = random.Next(1, 43);
            int randomStarship2 = random.Next(1, 43);
            Person person = await JSONHelper.GetPerson(randomPerson.ToString());
            Starships starship2 = await JSONHelper.GetStarships(randomStarship2.ToString());
            Planet planet = await JSONHelper.GetPlanet(randomPlanet.ToString());
            string starship = string.Empty;
            foreach (string c in person.starships)
            {
                Starships starships = await JSONHelper.GetStarships(c);
                starship = starships.name;
            }
            if (starship.Length < 1)
            {
                Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                starship = starships.name;
            }
            ScenarioSpace scenarioSpace = new ScenarioSpace(person.name, planet.name, starship, starship2.name);
            GeneratedScenariolbl.Text = scenarioSpace.GenerateScenario();
        }
        /// <summary>
        /// This function is the same as the GenerateGroundScenario that handles if the user input both options but if the user selected a space scenario
        /// </summary>
        /// <param name="person">inputed person ID</param>
        /// <param name="planet">inputed planet ID</param>
        public async void GenerateSpaceScenario(string person, string planet)
        {
            Random random = new Random();
            int randomStarship2 = random.Next(1, 43);
            int randomStarship = random.Next(1, 43);
            Person person1 = await JSONHelper.GetPerson(person);
            Starships starship2 = await JSONHelper.GetStarships(randomStarship2.ToString());
            Planet planet1 = await JSONHelper.GetPlanet(planet);
            string starship = string.Empty;
            foreach (string c in person1.starships)
            {
                Starships starships = await JSONHelper.GetStarships(c);
                starship = starships.name;
            }
            if (starship.Length < 1)
            {
                Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                starship = starships.name;
            }
            ScenarioSpace scenarioSpace = new ScenarioSpace(person1.name, planet1.name, starship, starship2.name);
            GeneratedScenariolbl.Text = scenarioSpace.GenerateScenario();
        }
        /// <summary>
        /// This function is the same as the GenerateGroundScenario that handled if the user only inputed one option but if the user selected space
        /// </summary>
        /// <param name="personPlanet">inputed planet or person</param>
        /// <param name="personOrPlanet">1 if person, 2 if planet</param>
        public async void GenerateSpaceScenario(string personPlanet, int personOrPlanet)
        {
            if (personOrPlanet == 1)
            {
                Random random = new Random();
                int randomPlanet = random.Next(1, 63);
                int randomStarship = random.Next(1, 43);
                int randomStarship2 = random.Next(1, 43);
                Person person = await JSONHelper.GetPerson(personPlanet);
                Starships starship2 = await JSONHelper.GetStarships(randomStarship2.ToString());
                Planet planet = await JSONHelper.GetPlanet(randomPlanet.ToString());
                string starship = string.Empty;
                foreach (string c in person.starships)
                {
                    Starships starships = await JSONHelper.GetStarships(c);
                    starship = starships.name;
                }
                if (starship.Length < 1)
                {
                    Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                    starship = starships.name;
                }
                ScenarioSpace scenarioSpace = new ScenarioSpace(person.name, planet.name, starship, starship2.name);
                GeneratedScenariolbl.Text = scenarioSpace.GenerateScenario();
            }
            else
            {
                Random random = new Random();
                int randomPerson = random.Next(1, 89);
                int randomStarship = random.Next(1, 43);
                int randomStarship2 = random.Next(1, 89);
                Person person = await JSONHelper.GetPerson(randomPerson.ToString());
                Starships starship2 = await JSONHelper.GetStarships(randomStarship2.ToString());
                Planet planet = await JSONHelper.GetPlanet(personPlanet);
                string starship = string.Empty;
                foreach (string c in person.starships)
                {
                    Starships starships = await JSONHelper.GetStarships(c);
                    starship = starships.name;
                }
                if (starship.Length < 1)
                {
                    Starships starships = await JSONHelper.GetStarships(randomStarship.ToString());
                    starship = starships.name;
                }
                ScenarioSpace scenarioSpace = new ScenarioSpace(person.name, planet.name, starship, starship2.name);
                GeneratedScenariolbl.Text = scenarioSpace.GenerateScenario();
            }
        }
        ///I once again would like to say that this is I think a very in-eifficent way to handle user input, a better way would be to check which environment the user selected, then generate the required items for that environment
        ///as the computer reaches each item it checks if the user has inputed a value for the item and if its a viable, if the user hasn't entered a viable input then it randomly generates it. This method may result in more lines 
        ///initially and actually may be more computationally heavy in the long run but it would allow new and more items to be introduced into the program without revamping essentially the entire algorithm.
    }
}
