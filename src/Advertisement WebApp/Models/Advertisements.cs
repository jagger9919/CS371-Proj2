using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Advertisement_WebApp.Models
{
    public class Advertisements
    {
        [Key]
        public string AdvTitle { get; set; }
        public string AdvDetails { get; set; }
        public DateTime AdvDateTime { get; set; }
        public decimal Price { get; set; }
        public string Category_ID { get; set; }
        public string User_ID { get; set; }
        public string Moderator_ID { get; set; }
        public string Status_ID { get; set; }
    }
}
