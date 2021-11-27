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
    public partial class frmCliente : Form
    {
        private string connectionString;

        public frmCliente(string connectionString) //se ejecuta si es agregar
        {
            this.connectionString = connectionString;
            InitializeComponent();
            btnAgregar.Visible = true;
            btnActualizar.Visible = false;
            btnEliminar.Visible = false;
        }

        public frmCliente(int clienteID, string connectionString) //se ejecuta cuando es update con 2 parametros
        {
            this.connectionString = connectionString;
            InitializeComponent();
            btnAgregar.Visible = false;
            btnActualizar.Visible = true;
            btnEliminar.Visible = true;
            AsignarValores(clienteID);
        }

        private void AsignarValores(int clienteId)
        {
            Cliente cliente = new Cliente(connectionString);
            cliente = cliente.GetCliente(clienteId);
            txtNombre.Text = cliente.Nombre;
            txtDireccion.Text = cliente.Direccion;
            txtRFC.Text = cliente.RFC;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
            txtId.Text = cliente.ID.ToString();
         
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente(connectionString);
                cliente.Nombre = txtNombre.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.RFC = txtRFC.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Email = txtEmail.Text;
                cliente.Agregar(cliente);
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
                Cliente cliente = new Cliente(connectionString);
                cliente.Nombre = txtNombre.Text;
                cliente.Direccion = txtDireccion.Text;
                cliente.RFC = txtRFC.Text;
                cliente.Telefono = txtTelefono.Text;
                cliente.Email = txtEmail.Text;
                cliente.ID = Convert.ToInt32(txtId.Text);
                cliente.Actulizar(cliente);
                MessageBox.Show("Datos Actualizados");
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
                DialogResult respuesta = MessageBox.Show("Estas segur@ de eliminar el registro","Confirmación",MessageBoxButtons.YesNo);

                if (respuesta == DialogResult.Yes)
                {
                    Cliente cliente = new Cliente(connectionString);
                    cliente.Eliminar(Convert.ToInt32(txtId.Text));
                    MessageBox.Show("Registro Eliminados");
                    this.Close();
                }
               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void regresarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
