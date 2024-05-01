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

    }
}
