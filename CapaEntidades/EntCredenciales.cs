using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class EntCredenciales
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "El correo electronico no es valido.")]
        public string Correo { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string Contrasena { get; set; }
    }
}
