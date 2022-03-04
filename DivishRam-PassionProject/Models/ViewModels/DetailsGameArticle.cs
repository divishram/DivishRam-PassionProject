using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivishRam_PassionProject.Models.ViewModels
{
    public class DetailsGameArticle
    {
        public GameArticleDto SelectedArticle { get; set; }

        //IDK IF this is right
        public IEnumerable<StoreDto> AvailableStores { get; set; }
        public IEnumerable<StoreDto> UnavailableStores { get; set; }


    }
}