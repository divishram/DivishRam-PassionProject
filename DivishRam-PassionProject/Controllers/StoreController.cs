using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using DivishRam_PassionProject.Models;
using DivishRam_PassionProject.Models.ViewModels;
using System.Web.Script.Serialization;

namespace DivishRam_PassionProject.Controllers
{
    public class StoreController: Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static StoreController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/");
        }

        // GET: Keeper/List
        public ActionResult List()
        {
            //objective: communicate with our store data and retrieve list of stores
            

            string url = "storedata/liststores";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<StoreDto> Stores = response.Content.ReadAsAsync<IEnumerable<StoreDto>>().Result;
            //Debug.WriteLine("Number of Keepers received : ");
            //Debug.WriteLine(Keepers.Count());

            return View(Stores);
        }

    }
}
