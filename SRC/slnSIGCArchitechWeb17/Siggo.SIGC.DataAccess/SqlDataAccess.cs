using Siggo.SIGC.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siggo.SIGC.DataAccess
{
    public class SqlDataAccess
    {
        public static void EjecutarComando(string strComando)
        {
            using (SqlConnection connection = new SqlConnection(Globales.ConfigServidor()))
            {
                SqlCommand command = new SqlCommand(strComando, connection);
                command.CommandTimeout = 120;
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public static DataTable EjecutarQuery(string strQuery)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection connection = new SqlConnection(Globales.ConfigServidor()))
            {
                SqlCommand command = new SqlCommand(strQuery, connection);
                command.CommandTimeout = 120;
                command.Connection.Open();

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    if (dr != null)
                    {
                        int nIdRegistro = 0;
                        while (dr.Read())
                        {
                            nIdRegistro++;

                            if (nIdRegistro == 1)
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    dtResultado.Columns.Add(dr.GetName(i));
                                }
                            }

                            DataRow drow = dtResultado.NewRow();

                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                drow[dr.GetName(i)] = dr[i].ToString();
                            }

                            dtResultado.Rows.Add(drow);
                        }
                    }
                }
            }

            return dtResultado;
        }

        public static object RetornarValor(string strComando)
        {
            object objResultado = new object();
            using (SqlConnection connection = new SqlConnection(Globales.ConfigServidor()))
            {
                SqlCommand command = new SqlCommand(strComando, connection);
                command.CommandTimeout = 120;
                command.Connection.Open();
                objResultado = command.ExecuteScalar();
            }
            return objResultado;
        }
    }
}
