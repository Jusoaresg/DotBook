﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Book.Models{

    public class ShoppingCartItem
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string UserId {get; set;}
        [ForeignKey("UserId")]
        public IdentityUser User {get; set;}

        [Required]
        public int ProductId {get; set;}
        [ForeignKey("ProductId")]
        public Product Product {get; set;}
        [Range(1, 1000)]
        public int Amount {get; set;} = 1;
        public double Price {get; set;}

    }
};


