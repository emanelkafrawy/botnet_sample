using System;
using System.ComponentModel.DataAnnotations;

namespace firstProject.Models
{
	public class Item
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
        public decimal Price { get; set; }
		public DateTime createdDate { get; set; } = DateTime.Now;
    }
}

