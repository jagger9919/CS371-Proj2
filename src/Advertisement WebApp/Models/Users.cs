using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Advertisement_WebApp.Models
{
    public class Users
    {
        [Key]
        public string User_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }
}
