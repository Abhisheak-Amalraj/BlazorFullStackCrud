using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        //constructor
        public UserService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }


        // Property to hold a list of User objects.
        public List<User> Users { get; set;} = new List<User>();


        // Asynchronous method to create a new User object
        public async Task CreateUser(User user)
        {
            // Ensure that the WaterIntakes property of the user is not null
            user.WaterIntakes = user.WaterIntakes ?? new List<WaterIntake>();

            // Make a POST request to the API to create a user
            var result = await _http.PostAsJsonAsync("api/user", user);

            if (result.IsSuccessStatusCode)
            {
                // If the request is successful, updates the local list of users
                var users = await result.Content.ReadFromJsonAsync<List<User>>();
            }
            else
            {
                // If the request fails, show a message
                var error = await result.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"Error creating user: {error}");
            }
        }

        //   private async Task SetUsers(HttpResponseMessage result)
        //   {
        //       var response = await result.Content.ReadFromJsonAsync<List<User>>();
        //       Users = response;
        //       _navigationManager.NavigateTo("users");
        //  }


        // Asynchronous method to retrieve a single user by ID
        public async Task<User> GetSingleUser(int id)
        {
            // Make a GET request to fetch a user by ID
            var result = await _http.GetFromJsonAsync<User>($"api/user/{id}");

            // If the user is found, return it
            if (result != null)
                return result;

            // If the user is not found, throw an exception
            throw new Exception("User not found!");
        }


        // Asynchronous method to retrieve all users
        public async Task GetUsers()
        {
            // Make a GET request to fetch all users
            var result = await _http.GetFromJsonAsync<List<User>>("api/user");

            // If the request is successful, update the Users property
            if (result != null)
            {
                Users = result;
            }
        }


        // Asynchronous method to update an existing user
        public async Task<bool> UpdateUser(User user)
        {
            // Make a PUT request to update the user
            var response = await _http.PutAsJsonAsync($"api/user/{user.ID}", user);
            if (response.IsSuccessStatusCode)
            {
                // If the update is successful, return true
                return true;
            }
            else
            {
                // If the update fails, read the error content and throw an exception
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Error updating user: {errorContent}");
            }
        }



        // Asynchronous method to delete a user by ID
        public async Task DeleteUser(int id)
        {
            // Make a DELETE request to the API to remove the user
            var result = await _http.DeleteAsync($"api/user/{id}");

            if (result.IsSuccessStatusCode)
            {
                // If the delete operation is successful, fetch the updated list of users
                var users = await result.Content.ReadFromJsonAsync<List<User>>();
                Users = users;
                _navigationManager.NavigateTo("users");
            }
            else
            {
                // If the delete operation fails, show a error message
                var error = await result.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"Error deleting user: {error}");
            }
        }
    }
}
