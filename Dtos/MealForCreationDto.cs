using System.Collections.Generic;
using Backend.Models;

namespace backend.Dtos
{
    public class MealForCreationDto
    {
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        // public ICollection<Product> Products { get; set; }
        public Product Products {get; set;}
    }
}