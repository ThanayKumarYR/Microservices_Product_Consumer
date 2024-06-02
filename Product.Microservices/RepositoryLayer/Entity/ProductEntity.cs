using System.ComponentModel.DataAnnotations;

namespace Customer.Microservice.RepositoryLayer.Entity
{
    public class ProductEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Rate { get; set; }
    }
}
