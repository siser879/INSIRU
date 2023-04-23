using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insiru
{
    internal class Conector
    {
        private static string server = ConfigurationManager.AppSettings["ip"];
        private static string bbdd = ConfigurationManager.AppSettings["bbdd"];
        private static string usuario = ConfigurationManager.AppSettings["usuario"];
        private static string password = ConfigurationManager.AppSettings["password"];

        public static string imagenes_Pokemon(string pokemon)
        {
            string sql = "select id from pokemon where Nombre = '" + pokemon + "';";

            using (SqlConnection connection = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + bbdd + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + password))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow row = dt.Rows[0];
                string pokemon_id = Convert.ToString(row["id"]);

                return pokemon_id;
            }
        }

        public static ArrayList obtenerStats(string pokemon)
        {
            string sql = "select Tipo, Vida from pokemon where Nombre = '" + pokemon + "';";

            ArrayList stats = new ArrayList();

            using (SqlConnection connection = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + bbdd + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + password))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                DataRow row = dt.Rows[0];
                stats.Add(Convert.ToString(row["Tipo"]));
                stats.Add(Convert.ToString(row["Vida"]));

                return stats;
            }
        }

        public static ArrayList obtener_Ataque()
        {
            string sql = "select Nombre from Ataque;";

            ArrayList nombres = new ArrayList();

            using (SqlConnection connection = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + bbdd + ";Persist Security Info=True;User ID=" + usuario + ";Password=" + password))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(sql, connection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    string nombre_ataque = Convert.ToString(row["Nombre"]);
                    nombres.Add(nombre_ataque);

                }
                
                return nombres;
            }
        }

    }
}
