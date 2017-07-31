using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class EntLista
    {
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Correo { get; set; }
        
        [Display(Name = "Nombre completo")]        
        public string FullName
        {
            get
            {
                return Nombre + " " + APaterno + " " + AMaterno;
            }
        }
    }
}
