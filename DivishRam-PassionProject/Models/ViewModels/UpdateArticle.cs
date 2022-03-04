using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DivishRam_PassionProject.Models.ViewModels
{
    public class UpdateArticle
    {
        public GameArticleDto SelectedArticle { get; set; }

        public IEnumerable<PublisherDto> PublisherOptions { get; set; }
    }
}