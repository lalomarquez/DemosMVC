using System;
using System.ComponentModel.DataAnnotations;

namespace CapaEntidades
{
    public class EntReporteAsistencia
    {
        private string tipoRegistro;

        [Required(ErrorMessage = "Ingrese un ID de usuario.")]
        public int IdUsuario { get; set; }      
        
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }

        [Display(Name = "Nombre completo")]
        public string FullName
        {
            get
            {
                return Nombre + " " + APaterno + " " + AMaterno;
            }
        }

        [Display(Name = "Fecha y hora")]
        public DateTime FechaHora { get; set; }

        [Display(Name = "Movimiento")]
        public string TipoRegistro
        {
            get { return tipoRegistro; }
            set
            {
                if (value == "1")
                    tipoRegistro = "Entrada";
                else
                    tipoRegistro = "Salida";
            }
        }
    }
}
