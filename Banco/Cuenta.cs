using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Banco
{
    class Cuenta
    {
        private string connectionString;

        public int ID { get; set; }
        public int NumeroDeCuenta { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public Decimal LimiteDeCredito { get; set; }
        public Decimal Saldo { get; set; }
        public int ClienteId { get; set; }
        public string NombreDelCliente { get; set; }

        public Cuenta(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Cuenta()
        {
            //
        }

        //cargamos la grid inicial de frmCuentas
        public DataTable GetCuentas()
        {
            string query = "select Nombre,ID,NumeroDeCuenta,FechaDeAlta,LimiteDeCredito,Saldo from v_cuentas";
            //string query = "" +
            //    "select Clientes.Nombre,Cuentas.ID,Cuentas.NumeroDeCuenta,Cuentas.FechaDeAlta,Cuentas.LimiteDeCredito,Cuentas.Saldo " +
            //    "from Clientes,Cuentas " +
            //    "where Clientes.ID = Cuentas.ClienteId; ";
               //ID tabla clientes <- llave foranea en tabla cuentas
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
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

        //sobrecarga de GetCuentas para buscar por nombre del cliente
        public DataTable GetCuentas(string dato)
        {
            string query = "select Nombre,ID,NumeroDeCuenta,FechaDeAlta,LimiteDeCredito,Saldo from v_Cuentas where (Nombre like '%'+@dato+'%') or (Email like '%'+@dato+'%')";
            
            if (int.TryParse(dato,out int numero))
            {
                query += " or (NumeroDeCuenta = @dato)";
            }

            //el query en linea 66 sustituye a este comentario
            //string query = "" +
            //    "select Clientes.Nombre,Cuentas.ID,Cuentas.NumeroDeCuenta,Cuentas.FechaDeAlta,Cuentas.LimiteDeCredito,Cuentas.Saldo " +
            //    "from Clientes,Cuentas " +
            //    "where(Clientes.ID = Cuentas.ClienteId) and (Clientes.Nombre like '%'+@nombre+'%')";
            //ID tabla clientes <- llave foranea en tabla cuentas
            
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@dato",dato);

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

        public Cuenta GetCuenta(int cuentaId)
        {
            string query = "select * from v_Cuentas where ID = @ID";
            //string query = "" +
            //    "select Clientes.Nombre,Cuentas.ID,Cuentas.NumeroDeCuenta,Cuentas.FechaDeAlta,Cuentas.LimiteDeCredito,Cuentas.Saldo " +
            //    "from Clientes,Cuentas " +
            //    "where(Clientes.ID = Cuentas.ClienteId) and (Cuentas.ID = @ID)";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@ID", cuentaId);
                        cn.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        Cuenta cuenta = new Cuenta();

                        while (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                cuenta.ID = Convert.ToInt32(reader["ID"]);
                                cuenta.NumeroDeCuenta = Convert.ToInt32(reader["NumeroDeCuenta"]);
                                cuenta.FechaDeAlta = Convert.ToDateTime(reader["FechaDeAlta"]).Date;
                                cuenta.LimiteDeCredito =Convert.ToDecimal(reader["LimiteDeCredito"]);
                                cuenta.Saldo = Convert.ToDecimal(reader["Saldo"]);
                                cuenta.NombreDelCliente = reader["Nombre"].ToString(); //este variable viene de la tabla Clientes
                            }
                        }
                        return cuenta;
                    }

                }

            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }

        

        public void Agregar(Cuenta cuenta)
        {
            string query = "insert into Cuentas (NumeroDeCuenta,FechaDeAlta,LimiteDeCredito,Saldo,ClienteId) " +
                "values (@NumeroDeCuenta,@FechaDeAlta,@LimiteDeCredito,@Saldo,@ClienteId)";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;

                        //command.Parameters.AddWithValue("@ID", cuenta.ID);
                        command.Parameters.AddWithValue("@NumeroDeCuenta", cuenta.NumeroDeCuenta);
                        command.Parameters.AddWithValue("@FechaDeAlta", cuenta.FechaDeAlta);
                        command.Parameters.AddWithValue("@LimiteDeCredito", cuenta.LimiteDeCredito);
                        command.Parameters.AddWithValue("@Saldo",cuenta.Saldo);
                        command.Parameters.AddWithValue("@ClienteId",cuenta.ClienteId);


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

       
        public void Actulizar(Cuenta cuenta)
        {
            string query = ""  +
                "update Cuentas set NumeroDeCuenta = @NumeroDeCuenta, FechaDeAlta = @FechaDeAlta, LimiteDeCredito = @LimiteDeCredito " +
                "where ID = @ID";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@ID", cuenta.ID);
                        command.Parameters.AddWithValue("@NumeroDeCuenta", cuenta.NumeroDeCuenta);
                        command.Parameters.AddWithValue("@FechaDeAlta", cuenta.FechaDeAlta);
                        command.Parameters.AddWithValue("@LimiteDeCredito", cuenta.LimiteDeCredito);

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
        
        public void Eliminar(int cuentaId)
        {
            string query = "delete from Cuentas where ID = @ID";
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, cn))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@ID", cuentaId);

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
