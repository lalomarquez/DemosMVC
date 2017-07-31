using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using Dapper;
using System.Data.Common;

namespace DAL
{
    public class AccesoDatos
    {
        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(ConnectionString.Connection);

        public static class ConnectionString
        {
            public static string Connection = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        }

        public List<EntCredenciales> ObtenerCredenciales()
        {
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    IList<EntCredenciales> entCrede = SqlMapper.Query<EntCredenciales>(connection, "sp_ObtenerCredenciales").ToList();
                    return entCrede.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EntLista> ListaUsuarios()
        {
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    IList<EntLista> entLista = SqlMapper.Query<EntLista>(connection, "sp_ListaUsuarios").ToList();
                    return entLista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EntUsuario> ObtenerTodosUsuarios()
        {
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    IList<EntUsuario> getAll = SqlMapper.Query<EntUsuario>(connection, "sp_ListaUsuarios").ToList();
                    return getAll.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string AltaNuevoUsuario(EntUsuario usuario)
        {
            string resultado = string.Empty;
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    var insertarUsuario = connection.Execute("sp_InsertarUsuario", usuario, commandType: CommandType.StoredProcedure);                    
                }
                resultado = "Un nuevo usuario ha sido dado de alta.";
            }
            catch (Exception ex)
            {
                //throw ex;
                resultado = ex.ToString();
            }
            return resultado;
        }

        public string ActualizarUsuario(EntUsuario usuario)
        {
            string resultado = string.Empty;
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    var updateUsuario = connection.Execute("sp_ActualizarUsuario", usuario, commandType: CommandType.StoredProcedure);
                }
                resultado = "Se ha actualizado el usuario: " + Convert.ToString(usuario.IdUsuario);
            }
            catch (Exception ex)
            {
                resultado = ex.ToString();
            }
            return resultado;
        }

        public bool EliminarUsuario(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@IdUsuario", Id);

                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    connection.Execute("sp_EliminarUsuario", param, commandType: CommandType.StoredProcedure);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }            
        }

        public string ValidarExisteUsuario(EntUsuario usuario)
        {
            string resultado = string.Empty;
            try
            {
                using (var connection = ConnectionFactory())
                {                    
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Correo", usuario.Correo);
                    parameter.Add("@Resultado", dbType: DbType.AnsiString, direction: ParameterDirection.Output, size: 100);

                    connection.Open();
                    var insertarUsuario = connection.Execute("sp_ValidarExisteUsuario", parameter, commandType: CommandType.StoredProcedure);
                    resultado = parameter.Get<string>("@Resultado");
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                resultado = ex.ToString();
            }
            return resultado;
        }





        public List<dynamic> ObtererCredenciales_old()
        {
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    var credenciales = SqlMapper.Query(connection, "sp_CredencialesUsuario").ToList();
                    return credenciales;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
