using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DivishRam_PassionProject.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string Founder { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }

    public class PublisherDto
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string Founder { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

    }

}