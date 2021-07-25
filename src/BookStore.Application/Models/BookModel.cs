using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Application.Models
{
    public class BookModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Title is Required")]
        [MinLength(2)]
        [MaxLength(250)]
        public string Title { get; set; }

        [MinLength(10)]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The PublishDate is Required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date type")]
        public DateTime PublishDate { get; set; }

        [MinLength(2)]
        [MaxLength(250)]
        public string  Authors { get; set; }
    }
}