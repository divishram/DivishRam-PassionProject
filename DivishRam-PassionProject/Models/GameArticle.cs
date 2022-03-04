using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivishRam_PassionProject.Models
{

    public class GameArticle
    {
        [Key]
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public float Rating { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }

        
        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        //games can be in many stores
        public ICollection<Store> Stores { get; set; } 


    }

    public class GameArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public float Rating { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }     

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
    
    }
}