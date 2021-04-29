using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Advertisement_WebApp.Models
{
    public class Statuses
    {
        [Key]
        public string Status_ID { get; set; }
        public string Name { get; set; }
    }
}
