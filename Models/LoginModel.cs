using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LIDOM.Models
{
    public partial class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;
    }
}
