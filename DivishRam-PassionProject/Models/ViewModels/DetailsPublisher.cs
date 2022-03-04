using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivishRam_PassionProject.Models.ViewModels
{
    public class DetailsPublisher
    {
        //the species itself that we want to display
        public PublisherDto SelectedPublisher { get; set; }

        //all of the related animals to that particular species
        public IEnumerable<GameArticleDto> RelatedArticles { get; set; }
    }
}