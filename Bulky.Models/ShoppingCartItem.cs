using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models{

    public class ShoppingCartItem
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string UserId {get; set;}
        [Required]
        public string ProductId {get; set;}
        [ForeignKey("ProductId")]
        public Product Product {get; set;}
        [Range(1, 1000)]
        public int Amount {get; set;} = 1;

    }
};


