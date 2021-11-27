using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banco
{
    public partial class frmCuenta : Form
    {
        private string connectionString;

        public frmCuenta(string connectionString) //se ejecuta si es agregar
        {
            this.connectionString = connectionString;
            InitializeComponent();
            btnAgregar.Visible = true;
            btnActualizar.Visible = false;
            btnEliminar.Visible = false;
        }

        public frmCuenta(int clienteID, string connectionString) //se ejecuta cuando es update con 2 parametros
        {
            this.connectionString = connectionString;
            InitializeComponent();
            btnAgregar.Visible = false;
            btnActualizar.Visible = true;
            btnEliminar.Visible = true;
            AsignarValores(clienteID);
        }

        private void AsignarValores(int cuentaId)
        {
            Cuenta cuenta = new Cuenta(connectionString);
            cuenta = cuenta.GetCuenta(cuentaId);
            txtId.Text = cuenta.ID.ToString();
            txtNombreDelCliente.Text = cuenta.NombreDelCliente;
            txtNumeroDeCuenta.Text = cuenta.NumeroDeCuenta.ToString();
            txtSaldo.Text = cuenta.Saldo.ToString();
            txtLimiteDeCredito.Text = cuenta.LimiteDeCredito.ToString();
            dtpFechaDeAlta.Value = cuenta.FechaDeAlta;
        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Cuenta cuenta = new Cuenta(connectionString);
                
                
                cuenta.NumeroDeCuenta = Convert.ToInt32(txtNumeroDeCuenta.Text);
                cuenta.Saldo = Convert.ToDecimal(txtSaldo.Text);
                cuenta.LimiteDeCredito = Convert.ToDecimal(txtLimiteDeCredito.Text);
                cuenta.FechaDeAlta = dtpFechaDeAlta.Value.Date;
                cuenta.ClienteId = Convert.ToInt32(txtClienteId.Text); //no esta validado por si no existe en la tabla clientes.
                cuenta.Agregar(cuenta);


                MessageBox.Show("Datos Actualizados");
                this.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Cuenta cuenta = new Cuenta(connectionString);
                
                cuenta.ID = Convert.ToInt32(txtId.Text);
                cuenta.NombreDelCliente = txtNombreDelCliente.Text;
                cuenta.NumeroDeCuenta = Convert.ToInt32(txtNumeroDeCuenta.Text);
                cuenta.Saldo = Convert.ToDecimal(txtSaldo.Text);
                cuenta.LimiteDeCredito = Convert.ToDecimal(txtLimiteDeCredito.Text);
                cuenta.FechaDeAlta = dtpFechaDeAlta.Value;
                MessageBox.Show("Datos Actualizados");
                cuenta.Actulizar(cuenta);
                this.Close();

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult respuesta = MessageBox.Show("Estas segur@ de eliminar el registro", "Confirmación", MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.Yes)
                {
                    Cuenta cuenta = new Cuenta(connectionString);
                    cuenta.Eliminar(Convert.ToInt32(txtId.Text));
                    MessageBox.Show("Registro Eliminados");
                    this.Close();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        
        //validará y taerá el nombre del cliente de la tabla cliente.

        private void txtClienteId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente(connectionString);
                //dgvClientes.DataSource = cliente.GetClientes(txtNombre.Text);

                Cliente nombreDelCliente = cliente.GetCliente(Convert.ToInt32(txtClienteId.Text));

                txtNombreDelCliente.Text = nombreDelCliente.Nombre;
            }
            catch (Exception err)
            {
                MessageBox.Show("sucedio el siguiente error : " + err.Message);
            }
        }
    }
}
