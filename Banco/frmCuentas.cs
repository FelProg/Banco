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
    public partial class frmCuentas : Form
    {
        private string connectionString;
        public frmCuentas(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
            //aqui va el llenado de la grid de cuentas
            MostrarCuentas();
        }

        private void MostrarCuentas()
        {
            try
            {
                Cuenta cuenta = new Cuenta(connectionString);
                dgvCuentas.DataSource = cuenta.GetCuentas();
            }
            catch (Exception err)
            {
                MessageBox.Show("sucedio el siguiente error : " + err.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Neceistamos llamar a una función de la clase cliente para que busque el registro
            try
            {
                Cuenta cuenta = new Cuenta(connectionString);
                dgvCuentas.DataSource = cuenta.GetCuentas(txtClienteCuenta.Text);
            }
            catch (Exception err)
            {
                MessageBox.Show("sucedio el siguiente error : " + err.Message);
            }
        }

        private void menuPrincipalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmacion = MessageBox.Show("Esta opción cerrara toda la aplicación. Desea continuar", "Confirmación", MessageBoxButtons.YesNo);
            if (confirmacion == DialogResult.Yes)
            {
                //Application.Exit();
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCuenta cuenta = new frmCuenta(connectionString);
            this.Hide();
            cuenta.ShowDialog(); //abre una forma nueva bloqueando la anterior.
            this.Show();
            MostrarCuentas();
        }

        private void dgvCuentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                MessageBox.Show("No hay registros disponibles");
            }
            else
            {
                int cuentaId = Convert.ToInt32(dgvCuentas.Rows[e.RowIndex].Cells["ID"].Value);
                frmCuenta cuenta = new frmCuenta(cuentaId, connectionString);
                this.Hide();
                cuenta.ShowDialog(); //abre una forma nueva bloqueando la anterior.
                this.Show();
                MostrarCuentas();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione el elemento que desea eliminar haciendo doble click sobre su registro en la tabla", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Seleccione el elemento que desea actualizar haciendo doble click sobre su registro en la tabla", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
