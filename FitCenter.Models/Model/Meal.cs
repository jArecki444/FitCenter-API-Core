using System.Collections.Generic;

namespace FitCenter.Models.Model
{
    public class Meal : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kcal { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fat { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<MealProducts> MealProducts { get; set; }
    }
}