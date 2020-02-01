using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AFILIADOS_JUAN_CARLOS_TEJEDA.Models
{
    public class Conexion
    {
        string ConnectionString = "Data Source=JTEJEDA;Initial Catalog=AFILIADOS_JUAN_CARLOS_TEJEDA;Integrated Security=true;";
        SqlConnection con;


        public void OpenConection()
        {
            con = new SqlConnection(ConnectionString);
            con.Open();
        }


        public void CloseConnection()
        {
            con.Close();
        }


        public bool AfiliadosInsert(Afiliados afiliados)
        {
            SqlCommand cmd = new SqlCommand("AFILIADOS_INSERT", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombres", afiliados.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", afiliados.Apellidos);
            cmd.Parameters.AddWithValue("@Fecha_Nacimiento", afiliados.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Sexo", afiliados.Sexo);
            cmd.Parameters.AddWithValue("@Cedula", afiliados.Cedula);
            cmd.Parameters.AddWithValue("@Nss", afiliados.Nss);
            cmd.Parameters.AddWithValue("@Fecha_Registro", afiliados.FechaRegistro);
            cmd.Parameters.AddWithValue("@Monto_Consumido", afiliados.MontoConsumido);
            cmd.Parameters.AddWithValue("@Id_Estatus", afiliados.IdEstatus);
            cmd.Parameters.AddWithValue("@Id_Planes", afiliados.IdPlanes);
            return cmd.ExecuteNonQuery() >= 1;
        }
        public bool AfiliadosUpdate(Afiliados afiliados)
        {
            SqlCommand cmd = new SqlCommand("AFILIADOS_UPDATE", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", afiliados.Id);
            cmd.Parameters.AddWithValue("@Nombres", afiliados.Nombres);
            cmd.Parameters.AddWithValue("@Apellidos", afiliados.Apellidos);
            cmd.Parameters.AddWithValue("@FechaNacimiento", afiliados.FechaNacimiento);
            cmd.Parameters.AddWithValue("@Sexo", afiliados.Sexo);
            cmd.Parameters.AddWithValue("@Cedula", afiliados.Cedula);
            cmd.Parameters.AddWithValue("@Nss", afiliados.Nss);
            cmd.Parameters.AddWithValue("@FechaRegistro", afiliados.FechaRegistro);
            cmd.Parameters.AddWithValue("@MontoConsumidos", afiliados.MontoConsumido);
            cmd.Parameters.AddWithValue("@Id_Estatus", afiliados.IdEstatus);
            cmd.Parameters.AddWithValue("@Id_Planes", afiliados.IdPlanes);
            return cmd.ExecuteNonQuery() >= 1;
        }

        public bool AfiliadosCambiarEstatus(Afiliados afiliados)
        {
            SqlCommand cmd = new SqlCommand("AFILIADOS_CAMBIAR_ESTATUS", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", afiliados.Id);
            return cmd.ExecuteNonQuery() >= 1;
        }

        public bool AfiliadosInactivar(Afiliados afiliados)
        {
            SqlCommand cmd = new SqlCommand("AFILIADOS_INACTIVO", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", afiliados.Id);
            return cmd.ExecuteNonQuery() >= 1;
        }

        public SqlDataReader AfiliadosConsultar(int id)
        {
            SqlCommand cmd = new SqlCommand("DBO.AFILIADOS_SELECT", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //Para ver a todos los afiliados
        public IEnumerable<Afiliados> ObtenerAfiliados()
        {
            List<Afiliados> lstAfiliados = new List<Afiliados>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("AFILIADOS_SELECT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Afiliados afiliados = new Afiliados();

                    afiliados.Id = Convert.ToInt32(rdr["Id"]);
                    afiliados.Nombres = Convert.ToString(rdr["Nombres"]);
                    afiliados.Apellidos = Convert.ToString(rdr["Apellidos"]);
                    afiliados.FechaNacimiento = Convert.ToDateTime(rdr["Fecha_Nacimiento"]);
                    afiliados.Sexo = Convert.ToString(rdr["Sexo"]);
                    afiliados.Cedula = Convert.ToString(rdr["Cedula"]);
                    afiliados.Nss = Convert.ToString(rdr["Nss"]);
                    afiliados.FechaNacimiento = Convert.ToDateTime(rdr["Fecha_Registro"]);
                    afiliados.MontoConsumido = Convert.ToDecimal(rdr["Monto_Consumido"]);
                    afiliados.IdEstatus = Convert.ToInt32(rdr["Id_Estatus"]);
                    afiliados.IdPlanes = Convert.ToInt32(rdr["Id_Planes"]);


                    lstAfiliados.Add(afiliados);

                }

                con.Close();
            }
            return lstAfiliados;
        }
    }
}