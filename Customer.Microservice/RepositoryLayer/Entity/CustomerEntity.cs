using System.ComponentModel.DataAnnotations;

namespace Customer.Microservice.RepositoryLayer.Entity
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}
