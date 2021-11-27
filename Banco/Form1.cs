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
    public partial class Form1 : Form
    {
        private string connectionString;

        public Form1()
        {
            InitializeComponent();
            connectionString = "Server=localhost;Database=BancoDB;Trusted_Connection=True;";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblHeader.Text = "\n" +
                "              Bienvenido" +
                "\nSistema Administrativo Bancario" +
                "\n                 Ver 1.0" +
                "\n" +
                "\n        Disfrute su estancia";
        }

        private void administraciónDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClientes clientes = new frmClientes(connectionString);
            this.Hide();
            clientes.ShowDialog();
            try
            {
                this.Show();
            }
            catch (Exception)
            {
               // :P 
            }
            
        }

        private void administracionDeCuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
            frmCuentas cuentas = new frmCuentas(connectionString);
            this.Hide();
            cuentas.ShowDialog();
            try
            {
                this.Show();
            }
            catch (Exception)
            {
                // :P 
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
