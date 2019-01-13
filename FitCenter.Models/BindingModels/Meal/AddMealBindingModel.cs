using System;
using System.Collections.Generic;
using System.Text;

namespace FitCenter.Models.BindingModels.Meal
{
    public class AddMealBindingModel
    {
        public string Name { get; set; }
        public ICollection<int> ProductsIds { get; set; }
    }
}
