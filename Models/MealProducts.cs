using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Dtos;

namespace Backend.Models
{
    public class MealProducts
    {
        public int Id { get; set; }

        [ForeignKey(nameof(MealId))]
        public int MealId { get; set; }
        public int ProductId {get; set;}
        public int UserId { get; set; }
    }
}