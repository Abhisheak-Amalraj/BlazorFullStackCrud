using BlazorFullStackCrud.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;


namespace BlazorFullStackCrud.Client.Services.WaterIntakeService
{
    public class WaterIntakeService : IWaterIntakeService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public WaterIntakeService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }


        // List to hold water intake records
        public List<WaterIntake> WaterIntakeRecords { get; set; } = new List<WaterIntake>();

        // List to hold the list of Users
        public List<User> Users { get; set; } = new List<User>();


        // Asynchronous method to create a new water intake record
        public async Task CreateIntakeRecord(WaterIntake intakeRecord)
        {
            // Posts the new intake record to the API and awaits the response
            var result = await _http.PostAsJsonAsync("api/waterintake", intakeRecord);

            // Processes the response to update the local list of water intake records
            await SetWaterIntakeRecords(result);
        }


        // Asynchronous method to get a single intake record by its ID
        public async Task<WaterIntake> GetSingleIntakeRecord(int id)
        {
            // Sends a GET request to the API and awaits the response
            HttpResponseMessage response = await _http.GetAsync($"api/waterintake/{id}");

            // If the response is successful, reads the water intake record from the response
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<WaterIntake>();
            }
            else
            {
                //return null if the record wasn't found or there was an error
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching intake record: {errorMessage}");
                return null;
            }
        }


        // Asynchronous method to get all WaterIntakeRecords
        public async Task GetWaterIntakeRecords()
        {
            try
            {
                // Sends a GET request to fetch all water intake records and updates the local list
                var response = await _http.GetFromJsonAsync<List<WaterIntake>>("api/waterintakes");
                if (response != null)
                {
                    WaterIntakeRecords = response;
                }
                else
                {
                    WaterIntakeRecords = new List<WaterIntake>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handles HTTP request exceptions
                Console.WriteLine($"An error occurred when fetching water intake records: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                // Handle non-HTTP exceptions if necessary
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }


        // Helper method to process the HTTP response and update the local list of water intake records
        private async Task SetWaterIntakeRecords(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                // Reads the updated list of water intake records from the response
                var response = await result.Content.ReadFromJsonAsync<List<WaterIntake>>();
                if (response != null)
                {
                    WaterIntakeRecords = response;
                    _navigationManager.NavigateTo("waterintakes");
                }
                else
                {
                    Console.WriteLine("Received a successful response, but no water intake records were found.");
                }
            }
            else
            {
                // If there's an error, logs it
                var errorMessage = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching water intake records: {errorMessage}");
            }
        }


        // Asynchronous method to update an existing intake record
        public async Task UpdateIntakeRecord(WaterIntake intakeRecord)
        {
            // Sends a PUT request to update the intake record and awaits the response
            var result = await _http.PutAsJsonAsync($"api/waterintake/{intakeRecord.ID}", intakeRecord);

            // Processes the response to update the local list of water intake records
            await SetWaterIntakeRecords(result);
        }


        // Asynchronous method to delete an intake record by its ID
        public async Task DeleteIntakeRecord(int id)
        {
            // Sends a DELETE request to the API and awaits the response
            var result = await _http.DeleteAsync($"api/waterintake/{id}");

            // Processes the response to update the local list of water intake records
            await SetWaterIntakeRecords(result);
        }

    }
}
