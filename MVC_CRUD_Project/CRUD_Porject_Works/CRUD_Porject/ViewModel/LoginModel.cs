﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_Porject.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}