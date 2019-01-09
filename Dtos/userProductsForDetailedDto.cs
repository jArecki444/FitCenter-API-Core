using System.Collections.Generic;

namespace backend.Dtos
{
    public class userProductsForDetailedDto
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<ProductForDetailedDto> Products { get; set; }
    }
}