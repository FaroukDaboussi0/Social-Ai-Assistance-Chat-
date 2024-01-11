using System.Text;
using System;
using beautywebsite.Services;
using Newtonsoft.Json;
namespace beautywebsite.Services;
public class SecurityService
{
    private readonly HttpClient httpClient;
    public SecurityService(HttpClient hc){
        httpClient=hc;
         this.httpClient.BaseAddress = new Uri("http://127.0.0.1:8000/");
    }
    public async Task<string> sendcodeto(String number){
        HttpCodeSend httpCodeSend = new HttpCodeSend(number);
        string serializedRequest = JsonConvert.SerializeObject(httpCodeSend);
        HttpContent content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await httpClient.PostAsync("api/send", content);
         string pass = await response.Content.ReadAsStringAsync();
        return pass;
    }

    public async Task<string> getstatus(String phonenumber, String code){
         HttpCodeRecive httpCodeRecive = new HttpCodeRecive(phonenumber,code);
        string serializedRequest = JsonConvert.SerializeObject(httpCodeRecive);
        HttpContent content = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await httpClient.PostAsync("api/verify",content);
         string pass = await response.Content.ReadAsStringAsync();
        return pass;
    }

}