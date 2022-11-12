using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Porject.ViewModel
{
    public class CustomerSaleInputModel
    {
        public int CustomerID { get; set; }
        [Required, StringLength(50), Display(Name = "Customer Name")]
        public string CustomersName { get; set; }
    
        public List<CustomerBookViewModel> CustomerBooks { get; set; } = new List<CustomerBookViewModel>();
    }
}