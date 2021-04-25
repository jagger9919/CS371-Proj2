using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertisement_WebApp.Models
{
    public class Advertisements
    {
        string AdvTitle { get; set; }
        string AdvDetials { get; set; }
        string AdvDatetime { get; set; }
        decimal Price { get; set; }
        string Category_ID { get; set; }
        string User_ID { get; set; }
        string Moderator_ID { get; set; }
        string Status_ID { get; set; }
    }
}
