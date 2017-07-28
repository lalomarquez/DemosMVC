using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class EntUsuario
    {
        public int IdUsuario { get; set; }

        //[Display(Name = "Usuario")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El apellido paterno es requerido.")]
        public string APaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = "El apellido materno es requerido.")]
        public string AMaterno { get; set; }
        
        [Required(ErrorMessage = "El correo es requerido.")]
        //[RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        //ErrorMessage = "El correo electronico no es valido.")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contrasena { get; set; }
    }
}
