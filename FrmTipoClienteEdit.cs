using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDesconectado
{
    public partial class FrmTipoClienteEdit : Form
    {
        DataRow fila;
        public FrmTipoClienteEdit(DataRow filaEditar = null)
        {
            InitializeComponent();
            if (filaEditar != null)
            {
                this.fila = filaEditar;
                this.Text = "Editar registro";
                mostrarDatos();
            }
        }

        private void aceptarCambios(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnombres.Text))
            {
                MessageBox.Show("Debe ingresar algo valido", "Arbaizo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        private void mostrarDatos()
        {
            txtap.Text = this.fila["Apellidos"].ToString();
            txtnombres.Text = this.fila["Nombres"].ToString();
            cbotipos.Text = this.fila["Tipo"].ToString();
            txtlimite.Text = this.fila["LimiteCredito"].ToString();
            txttelefono.Text = this.fila["Telefono"].ToString();
            txtemail.Text = this.fila["Email"].ToString();
            txtdireccion.Text = this.fila["Direccion"].ToString();
        }
    }
}
