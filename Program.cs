using System;
using System.Net.Http;
using System.Threading.Tasks;
///////////////////////////////////////////////////////////////////////////////
//
// Author: Colee Cottrell, cottrellc@etsu.edu
// Course: CSCI-2910-201 - Server Side Web Programming
// Assignment: Lab 5
// Date: 2/19/2024
// Description: C# Console App utilizing the Chuck Norris Jokes Web API
// Packages: I utilized Newtonsoft.JSON for this project. It helps Deserialize the JSON responses to retrieve the jokes from the API
//////////////////////////////////////////////////////////////////////////////

namespace ChuckNorrisJokesApp
{
    class Program
    {
        static async Task Main(string[] args) // Main method for program
        {
            Console.WriteLine("Ready to receive laughter??"); // Welcome display

            bool continuePlaying = true;  // Boolean to decide whether user wants to coninue recieving jokes and loop till they decide to stop
            while (continuePlaying)
            {
                string joke = await GetRandomJoke();
                Console.WriteLine("\nChuck Norris Joke:");
                Console.WriteLine(joke);

                Console.Write("\nDo you want another Chuck Norris joke? (yes/no): ");
                string response = Console.ReadLine().Trim().ToLower();
                continuePlaying = (response == "yes"); // Updates continuePlaying based on user decision
            }

            Console.WriteLine("Thank you, goodbye!");
        }

        static async Task<string> GetRandomJoke() // Method for recieving the Chuck Norris jokes
        {
            using (HttpClient client = new HttpClient()) // Instantiating a new HTTPclient to make HTTP requests
            {
                client.BaseAddress = new Uri("https://api.chucknorris.io"); // URL to the Chuck Norris joke API

                HttpResponseMessage response = await client.GetAsync("/jokes/random"); // Send GET request and await API response

                if (response.IsSuccessStatusCode) // Is the request successfull?
                {
                    string jokeJson = await response.Content.ReadAsStringAsync(); // Read the content recieved as a string
                    dynamic jokeObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jokeJson); // Deserializes the JSON response
                    return jokeObject.value;
                }
                else
                {
                    return "Could not retrieve joke. Please try again.";
                }
            }
        }
    }
}