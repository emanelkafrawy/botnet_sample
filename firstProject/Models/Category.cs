using System;
using System.ComponentModel.DataAnnotations;

namespace firstProject.Models
{
	public class Category
	{
        [Key]
        public int Id { get; set; }
        [Required]
		public required string Name { get; set; }

        public ICollection<Item>? Items { get; set; }
    }
}

