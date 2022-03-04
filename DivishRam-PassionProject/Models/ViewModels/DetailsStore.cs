using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivishRam_PassionProject.Models.ViewModels
{
    public class DetailsStore
    {

        public StoreDto SeleectedStore { get; set; }
        public IEnumerable<GameArticleDto> AvailableArticles { get; set; }
    }
}