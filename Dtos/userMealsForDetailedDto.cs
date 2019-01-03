using System.Collections.Generic;

namespace backend.Dtos
{
    public class userMealsForDetailedDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<MealForDetailedDto> Meals { get; set; }
    }
}