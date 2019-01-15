using System.Collections.Generic;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.Product;

namespace FitCenter.Models.ModelDto.Meal
{
    public class DetailsMealsDto : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        //public ICollection<DetailsProductDto> Products { get; set; }
        public ICollection<DetailsProductDto> Products = new List<DetailsProductDto>();
    }
}   