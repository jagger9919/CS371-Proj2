using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Advertisement_WebApp.Models
{
    public class Moderators
    {
        [Key]
        public string User_ID { get; set; }
    }
}
