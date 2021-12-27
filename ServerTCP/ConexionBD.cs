using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ServerTCP
{
    class ConexionBD // Clase para manipular la conexión con la base de datos.
    {
        // Por medio de la función establece una conexión con la base de datos.
        private SqlConnection Connection = new SqlConnection("server = LAPTOP-QKFQCOMI\\SQLEXPRESS; database = MIFINCA; integrated security = true");

        public SqlConnection Open() // Abre conexión.
        {
            if(Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            return Connection;
        }
        public void Close() // Cierra conexión.
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

    }
}
