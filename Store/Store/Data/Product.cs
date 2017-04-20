using System.ComponentModel.DataAnnotations;

namespace Store.Data
{
    public class Product
    {
        public int Id { get; set; }

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

        [Required]
        public string AuthorId { get; set; }


        public virtual User Author { get; set; }
   }
}