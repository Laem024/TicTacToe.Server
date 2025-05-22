using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Shared.DTO.User
{
    public class UserLoginDTO
    {
        [Required]
        [MaxLength(320, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene un formato valido")]
        public required string Email { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set;}
    }
}