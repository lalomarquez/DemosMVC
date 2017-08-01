using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class EntRegistrarEntrada
    {        
        public int IdTabla { get; set; }     
        public string TipoRegistro { get; set; }

        [Required(ErrorMessage = "Ingrese un ID de usuario.")]
        public int IdUsuario { get; set; }
    }
}
