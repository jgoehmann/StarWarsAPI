using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
/// <summary>
/// All the below functions are the borrowed functions that were given in class in order to get values using APIs, with some slight tweeks to accept IDs that often times have been user generated
/// </summary>
    internal class JSONHelper
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Planet> GetPlanet(string id)
        {
            Planet myDeserializedClass = new Planet();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                // just a cheeky way of getting the program know whether or not the id submitted is a full link or just an id
                //this *unfortunetly* limits the planet count to under 100k but the character limit could be adjusted
                if (id.Length < 8)
                {
                    HttpResponseMessage response = await client.GetAsync("https://swapi.py4e.com/api/planets/" + id + "/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    myDeserializedClass = JsonConvert.DeserializeObject<Planet>(responseBody);
                }
                else
                {

                    HttpResponseMessage response = await client.GetAsync(id);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    myDeserializedClass = JsonConvert.DeserializeObject<Planet>(responseBody);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return myDeserializedClass;
        }
        public static async Task<Person> GetPerson(string id)
        {
            Person myDeserializedClass = new Person();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //same cheeky method as used in the previous function
                if (id.Length < 8)
                {
                    HttpResponseMessage response = await client.GetAsync("https://swapi.py4e.com/api/people/" + id + "/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    myDeserializedClass = JsonConvert.DeserializeObject<Person>(responseBody);
                }
                else{
                    HttpResponseMessage response = await client.GetAsync(id);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    myDeserializedClass = JsonConvert.DeserializeObject<Person>(responseBody);
                }
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return myDeserializedClass;
        }
        public static async Task<AllSpecies> GetAllSpecies()
        {
            AllSpecies myDeserializedClass = new AllSpecies();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://swapi.py4e.com/api/species/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                myDeserializedClass = JsonConvert.DeserializeObject<AllSpecies>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return myDeserializedClass;
        }
        public static async Task<Starships> GetStarships(string c)
        {
            Starships myDeserializedClass = new Starships();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //implemented the cheeky method to tell between a full link ID or just the number ID
                if (c.Length < 8)
                {
                    HttpResponseMessage response = await client.GetAsync("https://swapi.py4e.com/api/starships/"+c+"/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    myDeserializedClass = JsonConvert.DeserializeObject<Starships>(responseBody);
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync(c);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    myDeserializedClass = JsonConvert.DeserializeObject<Starships>(responseBody);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return myDeserializedClass;
        }
        public static async Task<Species> GetSpecies(string c)
        {
            Species myDeserializedClass = new Species();
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://swapi.py4e.com/api/species/"+c+"/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                myDeserializedClass = JsonConvert.DeserializeObject<Species>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return myDeserializedClass;
        }
    }
}
