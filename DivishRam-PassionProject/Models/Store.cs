using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivishRam_PassionProject.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        public string StoreName { get; set; }

        //a store can have many games
        public ICollection<GameArticle> GameArticles { get; set; }
    }

    public class StoreDto
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }


}