﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsApiTest.Domains
{

    [Table("Products")]

    public class Products
    {
        [Key]
        public Guid IdProduct { get; set; } = Guid.NewGuid();

        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string? Nome { get; set; }

      
    }
}
