using System.Collections.Generic;
using FitCenter.Models.Model;
using FitCenter.Models.ModelDto.Product;

namespace FitCenter.Models.ModelDto.Meal
{
    public class DetailsMealsDto : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<DetailsProductDto> Products { get; set; }
        public ICollection<DetailsProductDto> Products = new List<DetailsProductDto>();

    }
}   