using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRUD_Porject.ViewModel
{
    public class CustomerBookViewModel
    {
        public int BookId { get; set; }
        [Required, StringLength(50), Display(Name = "Book Name")]
        public string Title { get; set; }
        [Required, DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal Price { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Publish Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }
        public bool Available { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        [ForeignKey("Genre")]
        public int GenreID { get; set; }
        [ForeignKey("Publisher")]
        public int PublisherID { get; set; }
        
    }
}