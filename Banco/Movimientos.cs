using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Banco
{
    class Movimientos
    {
        private string connectionString;

        public int ID { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public decimal Cantidad { get; set; }
        public MovimientoTipo TipoMovimiento { get; set; }
        public string Descripcion { get; set; }
        public int TarjetaID { get; set; }
        public int ClienteID { get; set; }

        public Movimientos(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public enum MovimientoTipo
        {
            Deposito,Retiro
        }

        public void Insertar(Movimientos movimientos)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    //definimos la transaccion
                    SqlTransaction transaction;

                    connection.Open();
                    //empezamos la transaccion
                    transaction = connection.BeginTransaction();
                    try
                    {

                        string query = "insert into Movimientos (FechaMovimiento,Cantidad,TipoMovimiento,Descripcion,TarjetaID,ClienteID) " +
                            "values (@FechaMovimiento,@Cantidad,@TipoMovimiento,@Descripcion,@TarjetaID,@ClienteID)";

                        using (SqlCommand command =  new SqlCommand(query,connection))
                        {
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@FechaMovimiento", movimientos.FechaMovimiento);
                            command.Parameters.AddWithValue("@Cantidad", movimientos.Cantidad);
                            command.Parameters.AddWithValue("@TipoMovimiento", movimientos.TipoMovimiento.ToString());
                            command.Parameters.AddWithValue("@Descripcion", movimientos.Descripcion);
                            command.Parameters.AddWithValue("@TarjetaID", movimientos.TarjetaID);
                            command.Parameters.AddWithValue("@ClienteID", movimientos.ClienteID);

                            command.Transaction = transaction;

                            command.ExecuteNonQuery();


                        }
                        int cuentaId = 0;
                        query = "select CuentaID from Tarjetas where ID = @TarjetaId";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@TarjetaId", movimientos.TarjetaID);

                            command.Transaction = transaction;

                            int.TryParse(command.ExecuteScalar().ToString(), out cuentaId);
                            

                        }

                        if (cuentaId == 0)
                        {
                            throw new Exception("No se enccontro cuenta "+movimientos.TarjetaID);
                        }

                        query = "update Cuentas set Saldo=Saldo";


                        if (movimientos.TipoMovimiento == MovimientoTipo.Deposito)
                        {
                            query += "+@Cantidad";

                        }
                        else
                        {
                            query += "-@Cantidad";
                        }

                        query += " where ID = @CuentaId";

                        using (SqlCommand command = new SqlCommand(query,connection))
                        {
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@CuentaId", cuentaId);
                            command.Parameters.AddWithValue("@Cantidad", movimientos.Cantidad);

                            command.Transaction = transaction;

                            command.ExecuteNonQuery();

                        }


                        transaction.Commit();
                    }
                    catch (Exception err)
                    {
                        transaction.Rollback();
                        throw new Exception(err.Message);
                    }
                    finally
                    {
                        //solo por previención... el using se encarga.
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }



    }

}

