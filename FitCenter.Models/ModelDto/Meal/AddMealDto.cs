using System;
using System.Collections.Generic;
using System.Text;
using FitCenter.Models.Model;

namespace FitCenter.Models.ModelDto.Product
{
    public class AddMealDto : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DetailsProductDto> Products { get; set; }
    }
}
