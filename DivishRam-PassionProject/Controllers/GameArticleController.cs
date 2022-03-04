/*
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
    public class GameArticleController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static GameArticleController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44324/api/"); //maybe change this?
        }

        // GET: GameArticle/list
        [HttpGet]
        public ActionResult List()
        {
            
            string url = "gamearticledata/listarticles";
            HttpResponseMessage response = client.GetAsync(url).Result;          

            IEnumerable<GameArticleDto> gameArticles = response.Content.ReadAsAsync<IEnumerable<GameArticleDto>>().Result;
            return View(gameArticles);

        }

        //GET : GameArticle/Details/3
        public ActionResult Details(int id)
        {
            DetailsGameArticle ViewModel = new DetailsGameArticle(); //maybe error here
            string url = "gamearticledata/findarticle" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            GameArticleDto SelectedArticle = response.Content.ReadAsAsync<GameArticleDto>().Result;
            Debug.WriteLine("article receive: ");
            Debug.WriteLine(SelectedArticle.Title);

            ViewModel.SelectedArticle = SelectedArticle;

            //show stores associated
            url = "storedata/liststoresforarticles" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<StoreDto> AvailableStores = response.Content.ReadAsAsync<IEnumerable<StoreDto>>().Result;

            ViewModel.AvailableStores = AvailableStores;

            return View(ViewModel);


        }
    }
}
*/