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
    public partial class frmClientes : Form
    {
        private string connectionString;

        public frmClientes(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            MostrarClientes();
        }

        private void MostrarClientes()
        {
            try
            {
                Cliente cliente = new Cliente(connectionString);
                dgvClientes.DataSource = cliente.GetClientes();
            }
            catch (Exception err)
            {
                MessageBox.Show("sucedio el siguiente error : " + err.Message);
            }
        }


        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("No hay registros disponibles");
            }
            else
            {
                int clienteId = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["ID"].Value);
                frmCliente cliente = new frmCliente(clienteId, connectionString);
                this.Hide();
                cliente.ShowDialog(); //abre una forma nueva bloqueando la anterior.
                this.Show();
                MostrarClientes();
            }

        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCliente cliente = new frmCliente(connectionString);
            this.Hide();
            cliente.ShowDialog(); //abre una forma nueva bloqueando la anterior.
            this.Show();
            MostrarClientes();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("Esta opción cerrara toda la aplicación. Desea continuar","Confirmación",MessageBoxButtons.YesNo);
            if (confirmacion == DialogResult.Yes)
            {
                //Application.Exit();
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione el elemento que desea eliminar haciendo doble click sobre su registro en la tabla","Eliminar",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione el registro que desea actualizar haciendo doble click sobre su registro en la tabla", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menúPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Neceistamos llamar a una función de la clase cliente para que busque el registro
            try
            {
                Cliente cliente = new Cliente(connectionString);
                dgvClientes.DataSource = cliente.GetClientes(txtNombre.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("sucedio el siguiente error : " + err.Message);
            }
        }
    }
}
