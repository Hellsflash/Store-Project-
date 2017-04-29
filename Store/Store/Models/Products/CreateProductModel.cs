using System.ComponentModel.DataAnnotations;
using Store.Helpers;
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
        [Display(Name="Picture URL")]
        [ImageValidateion]
        public string ImageUrl { get; set; }  
        
        

    }
}