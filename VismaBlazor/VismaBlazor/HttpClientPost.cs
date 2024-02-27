
using System.Net.Http;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using System.Net;
using VismaBlazor.Models;
using VismaBlazor;


namespace VismaBlazor
{
    public class HttpClientPost
    {

        private List<BrukerResponse>? BrukerRes;

        public async Task Post()
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://vismaapi2024.azurewebsites.net/");


                //var Ids = IdData.HentIdListe();

                Console.WriteLine("Ids:  ");

                var Ids = 10;

                var nyIdJson = JsonConvert.SerializeObject(Ids);
                Console.WriteLine(nyIdJson);
                var SData = new StringContent(nyIdJson, System.Text.Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, SData);

                if (res.IsSuccessStatusCode)
                {

                    var response = res.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Success");
                    BrukerRes = JsonConvert.DeserializeObject<List<BrukerResponse>>(response);

                }


                else
                {
                    Console.WriteLine("Fail");
                }

            }

        }

        public List<BrukerResponse> HentBrukerResponse()
        {
            return BrukerRes;
        }
    }

  

}   


