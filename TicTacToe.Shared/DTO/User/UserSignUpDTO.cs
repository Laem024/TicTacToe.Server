using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Shared.DTO.User
{
    public class UserSignUpDTO
    {
        [Required]
        [MaxLength(320, ErrorMessage = "El campo {0} no puede exceder los {1} caracteres.")]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene un formato valido")]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]{4,50}$", ErrorMessage = "El nombre de usuario solo puede contener letras, números, puntos, guiones y guiones bajos, y debe tener entre 4 y 50 caracteres.")]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,100}$", ErrorMessage = "La contraseña debe incluir al menos una letra mayúscula, una minúscula, un número y un carácter especial.")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public required string ConfirmPassword { get; set; }
    }
}