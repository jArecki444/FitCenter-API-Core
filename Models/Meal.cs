using System.Collections.Generic;
using backend.Dtos;

namespace Backend.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int UserId { get; set; }
        // public Product Products {get; set;}
        // public ICollection<ProductForDetailedDto> Product { get; set; }
        public ICollection<MealProducts> Products { get; set; }
    }
}