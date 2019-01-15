using System;
using System.Collections.Generic;
using System.Text;

namespace FitCenter.Models.ModelDto.Product
{
    public class UpdateProductDto
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
    }
}
