using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Banco
{

    public class Cliente
    {

        //definir un campo inicializado automaticamente en nulo
        private string connectionString;

        public int ID { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public Cliente(string connectionString)
        {
            this.connectionString = connectionString;
        }


        //inicio de los metodos
        public void Agregar(Cliente cliente)
        {
            string query = "insert into Clientes (RFC,Nombre,Direccion,Email,Telefono) values (@RFC,@Nombre,@Direccion,@Email,@Telefono)";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@RFC",cliente.RFC);
                        command.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Telefono", cliente.Telefono);

                        cn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception (e.Message);
            }
        }
    }
}


