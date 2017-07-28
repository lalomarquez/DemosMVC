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
                    IList<EntCredenciales> entCrede = SqlMapper.Query<EntCredenciales>(connection, "sp_CredencialesUsuario").ToList();
                    return entCrede.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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

        public List<EntUsuario> ObtenerUsuarios()
        {
            try
            {
                using (var connection = ConnectionFactory())
                {
                    connection.Open();
                    IList<EntUsuario> EmpList = SqlMapper.Query<EntUsuario>(connection, "sp_ObtenerUsuarios").ToList();
                    return EmpList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public void GetUsuarios()
        //{
        //    using (var connection = ConnectionFactory())
        //    {
        //        connection.Open();
        //        var getUsuarios = connection.Execute("sp_ObtenerUsuarios",commandType: CommandType.StoredProcedure).ToString();
        //    }
        //}
    }
}
