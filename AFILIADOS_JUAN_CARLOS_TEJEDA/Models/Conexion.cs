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
            OpenConection();
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
            cmd.Parameters.AddWithValue("@Id_Estatus", afiliados.Estatuses.ID);
            cmd.Parameters.AddWithValue("@Id_Planes", afiliados.Planes.ID);
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
            cmd.Parameters.AddWithValue("@Id_Estatus", afiliados.Estatuses.ID);
            cmd.Parameters.AddWithValue("@Id_Planes", afiliados.Planes.ID);
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

        public Estatus ObtenerEstatus(int id)
        {
            Estatus estatus = new Estatus();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ESTATUS_SELECT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                estatus.ID = Convert.ToInt32(rdr["Id"]);
                estatus.Estado = Convert.ToString(rdr["Estatus"]);
                con.Close();
            }
            return estatus;
        }

        public Planes ObtenerPlanes(int id)
        {
            Planes planes = new Planes();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("PLANES_SELECT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                rdr.Read();
                planes.ID = Convert.ToInt32(rdr["Id"]);
                planes.Plan = Convert.ToString(rdr["Planes"]);
                planes.MontoCobertura = Convert.ToDecimal(rdr["Monto_Cobertura"]);
                planes.FechaRegistro = Convert.ToDateTime(rdr["Fecha_Registro"]);
                planes.Estatuses = ObtenerEstatus(Convert.ToInt32(rdr["Id_Estatus"]));
                con.Close();
            }
            return planes;
        }

        public IEnumerable<Estatus> ObtenerEstatus()
        {
            List<Estatus> lstEstatus = new List<Estatus>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ESTATUS_SELECT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Estatus estatus = new Estatus();

                    estatus.ID = Convert.ToInt32(rdr["Id"]);
                    estatus.Estado = Convert.ToString(rdr["Estatus"]);

                    lstEstatus.Add(estatus);

                }

                con.Close();
            }
            return lstEstatus;
        }

        public IEnumerable<Planes> ObtenerPlanes()
        {
            List<Planes> lstPlanes = new List<Planes>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("PLANES_SELECT", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Planes planes = new Planes();

                    planes.ID = Convert.ToInt32(rdr["Id"]);
                    planes.Plan = Convert.ToString(rdr["Planes"]);
                    planes.MontoCobertura = Convert.ToDecimal(rdr["Monto_Cobertura"]);
                    planes.FechaRegistro = Convert.ToDateTime(rdr["Fecha_Registro"]);
                    planes.Estatuses = ObtenerEstatus(Convert.ToInt32(rdr["Id_Estatus"]));

                    lstPlanes.Add(planes);

                }

                con.Close();
            }
            return lstPlanes;
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
                    afiliados.Estatuses = ObtenerEstatus( Convert.ToInt32(rdr["Id_Estatus"]));
                    afiliados.Planes = ObtenerPlanes(Convert.ToInt32(rdr["Id_Planes"]));


                    lstAfiliados.Add(afiliados);

                }

                con.Close();
            }
            return lstAfiliados;
        }
    }
}