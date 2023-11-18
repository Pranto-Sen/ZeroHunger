using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.DTOs
{
    public class FoodDTO
    {
        public int FoodId { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public string PreserveTime { get; set; }
        public string FoodAmount { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Assign { get; set; }
        public int ResId { get; set; }
    }
}