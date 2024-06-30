using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace firstProject.Models
{
	public class Item
	{
        private const string categoryName = "Category";

        [Key]
		public int Id { get; set; }
		[Required]
		public required string Name { get; set; }
		[Required]
        [Range(10, 1000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal Price { get; set; }
		public DateTime createdDate { get; set; } = DateTime.Now;
        [Required]
		[DisplayName(categoryName)]
        [ForeignKey(categoryName)]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}

