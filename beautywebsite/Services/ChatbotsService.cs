using System.Text;
using System;
using Newtonsoft.Json;
namespace beautywebsite.Services;
public class ChatbotsService 
{
    private readonly HttpClient httpClient;

    public ChatbotsService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/"); // API base address
       
    }

    public async Task<string> GetReplyFromLocalAPI(HttpMessageRequest httpMessageRequest)
    {
        try
        {
         string serializedRequest = JsonConvert.SerializeObject(httpMessageRequest);
        
        // Create HttpContent from the serialized object
        HttpContent content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        Console.WriteLine(serializedRequest);
        // Send a POST request to  local API with the request body
        HttpResponseMessage response = await httpClient.PostAsync("api/endpoint", content);

        // Ensure the request was successful before reading the content
        response.EnsureSuccessStatusCode();

        // Read the response content
        string reply = await response.Content.ReadAsStringAsync();
        Console.WriteLine(reply);
        return reply;
    }
    catch (HttpRequestException ex)
    {
        // Handle any exceptions that might occur during the HTTP request
        throw new Exception("Error occurred while communicating with the local API: " + ex.Message);
    }}
}
