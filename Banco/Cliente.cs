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

        public Cliente()
        {
            //
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

        public DataTable GetClientes()
        {

            try
            {
                using(SqlConnection cn = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand("select * from Clientes",cn))
                    {
                        command.CommandType = CommandType.Text;
                        cn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command); //adaptamos los datos para utilizar el fill
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        return data;
                    }
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
        
        public DataTable GetClientes(string nombre)
        {
            string query = "select * from Clientes where nombre like '%'+@nombre+'%'";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@nombre",nombre);
                        cn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command); //adaptamos los datos para utilizar el fill
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        return data;
                    }
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public Cliente GetCliente(int clienteId)
        {
            string query = "select * from Clientes where ID = @ID";
            try
            {
                using(SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@ID", clienteId);
                        cn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        Cliente cliente = new Cliente();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                cliente.ID = Convert.ToInt32(reader["ID"]);
                                cliente.Nombre = reader["Nombre"].ToString();
                                cliente.RFC = reader["RFC"].ToString();
                                cliente.Telefono = reader["Telefono"].ToString();
                                cliente.Email = reader["Email"].ToString();
                                cliente.Direccion = reader["Direccion"].ToString();
                            }
                        }
                        return cliente;
                    }

                }

            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        public void Actulizar(Cliente cliente)
        {
            string query = "update Clientes set RFC = @RFC,Nombre = @Nombre, Direccion= @Direccion,Email= @Email,Telefono = @Telefono where ID = @ID";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@ID", cliente.ID);
                        command.Parameters.AddWithValue("@RFC", cliente.RFC);
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
                throw new Exception(e.Message);
            }
        }

        public void Eliminar(int clienteId)
        {
            string query = "delete from Clientes where ID = @ID";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@ID", clienteId);
                        

                        cn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}


