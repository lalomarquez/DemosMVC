using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using CapaEntidades;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            string correo = "Correo3@correo.com";
            string pass = "sdjahdkjh";            

            var dal = new AccesoDatos();
            List<EntUsuario> listaUsuarios = new List<EntUsuario>();
            listaUsuarios = dal.ObtererUsuarios();

            var v = listaUsuarios.Where(existe => existe.Correo.Equals(correo) && existe.Contrasena.Equals(pass)).FirstOrDefault();

            if (v != null)
                Console.WriteLine("Existe!!");
            else
                Console.WriteLine("usuario o contraseña no válidos.");
                    
            foreach (var item in listaUsuarios)
            {
                Console.WriteLine("{0} {1} {2}", item.IdUsuario, item.Correo, item.Contrasena);
                if ( item.Correo == correo && item.Contrasena == pass)
                    Console.WriteLine("Exitoso");
                else
                    Console.WriteLine("usuario o contraseña no válidos.");
            }

            Console.WriteLine("pulse cualquier tecla para terminar");
            Console.ReadKey();
        }
    }
}
