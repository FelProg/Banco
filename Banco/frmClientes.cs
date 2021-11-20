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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmCliente cliente = new frmCliente(connectionString);
            cliente.ShowDialog(); //abre una forma nueva bloqueando la anterior.
            MostrarClientes();
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
                cliente.ShowDialog(); //abre una forma nueva bloqueando la anterior.
                MostrarClientes();
            }

        }
    }
}
