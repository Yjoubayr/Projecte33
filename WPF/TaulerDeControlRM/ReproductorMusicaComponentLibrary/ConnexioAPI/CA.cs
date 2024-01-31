using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Configuration;

namespace ReproductorMusicaComponentLibrary.ConnexioAPI
{
    public class CA
    {
        public static string baseApi = ConfigurationManager.AppSettings["baseApi"];
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> GetDataFromApiAsync(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle the error
                    return null;
                    /*return $"Error: {response.StatusCode}";*/
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return $"Exception: {ex.Message}";
            }
        }

        public static async Task<string> PostDataAsync(string apiUrl, string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set the content type to JSON
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP content with the JSON data
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await client.PostAsync(apiUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle the error (e.g., log or throw an exception)
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
        }
        public static async Task<string> PutDataAsync(string apiUrl, string jsonData)
        {
            try
            {
                // Set the content type to JSON
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Create the HTTP content with the JSON data
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send the PUT request
                var response = await _httpClient.PutAsync(apiUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read and return the response content as a string
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle the error
                    return null;
                    /*return $"Error: {response.StatusCode} - {response.ReasonPhrase}";*/
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return $"Exception: {ex.Message}";
            }
        }

        public static async Task<string> DeleteDataFromApiAsync(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle the error
                    return null;
                    /*return $"Error: {response.StatusCode}";*/
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return $"Exception: {ex.Message}";
            }

        }

    }
}
