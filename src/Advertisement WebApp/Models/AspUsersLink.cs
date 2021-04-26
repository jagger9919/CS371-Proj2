using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore; 

namespace Advertisement_WebApp.Models
{
    public class AspUsersLink
    {
        [Key]
        public string User_ID { get; set; }
        public string aspUser_ID { get; set; }
    }
}
