using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FitCenter.Models.BindingModels.Product
{
    public class UpdateProductBindingModel : AddProductBindingModel
    {
        [Required]
        public int Id { get; set; }
    }
}
