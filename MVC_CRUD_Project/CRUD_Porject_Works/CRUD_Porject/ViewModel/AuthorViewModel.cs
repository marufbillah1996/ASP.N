using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Porject.ViewModel
{
    public class AuthorViewModel
    {
        [Required]
        public int AuthorID { get; set; }
        [Required,StringLength(50),Display(Name ="Author Name")]
        public string AuthorName { get; set; }
        [Required, StringLength(50), Display(Name = "Author Address")]
        public string AuthorAddress { get; set; }
    }
}