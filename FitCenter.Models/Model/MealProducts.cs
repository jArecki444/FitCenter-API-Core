using System.ComponentModel.DataAnnotations.Schema;

namespace FitCenter.Models.Model
{
    public class MealProducts : Entity
    {
        public int Id { get; set; }

        [ForeignKey(nameof(MealId))]
        public int MealId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public int ProductId {get; set;}
        public int UserId { get; set; }
    }
}