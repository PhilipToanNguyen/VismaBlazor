
using System.Net.Http;
using System.Text.Json;
using System;
using Newtonsoft.Json;
using System.Net;
using VismaBlazor.Models;
using VismaBlazor;
using Microsoft.AspNetCore.Components;



namespace VismaBlazor
{
    
    public class HttpClientPost
    {

        private List<BrukerRespons>? BrukerRes;
        

        public async Task Post(int Ids)

        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://vismaapi-d8eec0554dca.herokuapp.com/velgAntall");


                Console.WriteLine("Ids:  ");


                var nyIdJson = JsonConvert.SerializeObject(Ids);
                Console.WriteLine(nyIdJson);
                var SData = new StringContent(nyIdJson, System.Text.Encoding.UTF8, "application/json");

                var res = await client.PostAsync(endpoint, SData);

                if (res.IsSuccessStatusCode)
                {

                    var response = res.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("BOOM BABY");
                    BrukerRes = JsonConvert.DeserializeObject<List<BrukerRespons>>(response);
                  

                }


                else
                {
                    Console.WriteLine("FAIL!");
                    Console.WriteLine(res.StatusCode);

                }

            }

        }

        public async Task PostFlereId(string flereIds)

        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://vismaapi-d8eec0554dca.herokuapp.com/velgID");

                var nyIdJson = JsonConvert.SerializeObject(flereIds);
                Console.WriteLine(nyIdJson);
                var SData = new StringContent(nyIdJson, System.Text.Encoding.UTF8, "application/json");
                var res = await client.PostAsync(endpoint, SData);

                if (res.IsSuccessStatusCode)
                {

                    var response = res.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("BOOM BABY");
                    BrukerRes = JsonConvert.DeserializeObject<List<BrukerRespons>>(response);


                }


                else
                {
                    Console.WriteLine("FAIL!");
                    Console.WriteLine(res.StatusCode);
                }

            }

        }

        public List<BrukerRespons> HentBrukerResponse()
        {
            return BrukerRes;
        }
    }

  

}   


