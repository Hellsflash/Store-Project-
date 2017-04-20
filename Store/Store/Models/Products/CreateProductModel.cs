using System.ComponentModel.DataAnnotations;

namespace Store.Models.Products
{
    public class CreateProductModel
    {
  
        [Required]
        public string Name { get; set; }

        [Required]
        public string Categorie { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }   
    }
}