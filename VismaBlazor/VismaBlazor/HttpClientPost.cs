
using System.Net.Http;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using System.Net;
using VismaBlazor.Models;
using VismaBlazor;
using Microsoft.AspNetCore.Components;
using System.Linq.Dynamic.Core.Tokenizer;



namespace VismaBlazor
{
    //Klasse for å sende post requests til API og hente autogenererte brukere
    public class HttpClientPost
    {
        private List<BrukerRespons>? BrukerRes;

        ErrorMelding ErrorMelding = new ErrorMelding();

        //Post metode for å sende antall id til API som skal autogenereres
        public async Task Post(int Ids, string domene)
        {
            var token = await HentAuth();
            Console.WriteLine(token);
            if (token != null)
            { 
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://vismaapi-d8eec0554dca.herokuapp.com/velgAntall?domene=" + domene);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var nyIdJson = JsonConvert.SerializeObject(Ids);
                var SData = new StringContent(nyIdJson, System.Text.Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, SData);
                if (res.IsSuccessStatusCode)
                {
                    var response = await res.Content.ReadAsStringAsync();
                    BrukerRes = JsonConvert.DeserializeObject<List<BrukerRespons>>(response);
                    ErrorMelding.Melding = res.Headers.GetValues("melding").FirstOrDefault();
                    }
                else
                {
                    Console.WriteLine(res.StatusCode);
                }
                }
            }
        }
        //Post metode for å sende flere id som skrives manuelt til API som skal autogenereres
        public async Task PostFlereId(string flereIds, string domene) 
        {
            var token = await HentAuth();
            Console.WriteLine(token);
            if (token != null)
            { 
            
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://vismaapi-d8eec0554dca.herokuapp.com/velgID?domene=" + domene);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var nyIdJson = JsonConvert.SerializeObject(flereIds);
                var SData = new StringContent(nyIdJson, System.Text.Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, SData);
                if (res.IsSuccessStatusCode)
                {
                    var response = await res.Content.ReadAsStringAsync();
                    BrukerRes = JsonConvert.DeserializeObject<List<BrukerRespons>>(response);
                    ErrorMelding.Melding = res.Headers.GetValues("melding").FirstOrDefault();
                    }
                else
                {
                    Console.WriteLine(res.StatusCode);
                }
            }
          }
        }
        //Post metode for å sende forespørsel om token og få response for å hente token som derreter sendes sammen med andre requests
        public async Task<string> HentAuth()
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://dev-mfpashiwkekjyu0q.eu.auth0.com/oauth/token");

                var id = Environment.GetEnvironmentVariable("ClientId");
                var secret = Environment.GetEnvironmentVariable("ClientSecret");
                var audi = Environment.GetEnvironmentVariable("Audience");

                var reqbody = new
                {
                    client_id = id,
                    client_secret = secret,
                    audience = audi,
                    grant_type = "client_credentials",
                };

                var reqb = JsonConvert.SerializeObject(reqbody);
                var content = new StringContent(reqb, System.Text.Encoding.UTF8, "application/json");

                var res = await client.PostAsync(endpoint, content);

                if (res.IsSuccessStatusCode)
                {
                    string response = await res.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response);
                    return  jsonResponse.access_token;
                }
                else
                {
                    Console.WriteLine(res.StatusCode);
                    return null;
                }
            }
        }

        public List<BrukerRespons> HentBrukerResponse()
        {
            return BrukerRes;
        }

        public string HentErrorMelding()
        {
            return ErrorMelding.Melding;
        }
    }

  

}   


