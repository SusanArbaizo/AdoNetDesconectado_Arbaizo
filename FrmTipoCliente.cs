using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdoNetDesconectado
{
    public partial class FrmTipoCliente : Form
    {
        string cadenaConexion = @"Server=localhost\sqlexpress;
                                 DataBase=Comercial;
                                  Integrated Security=true";
        SqlDataAdapter adaptador;
        SqlConnection conexion;
        DataSet datos;
        public FrmTipoCliente()
        {
            InitializeComponent();
            dgvDatos.AutoGenerateColumns = false;
            //CREAMOS LA INSTANCIA DE LA CONEXION
            conexion = new SqlConnection(cadenaConexion);

            //CREAMOS LA INSTANCIA DEL ADAPTADOR

            adaptador = new SqlDataAdapter();

            //CREAMOS LA INSTANCIA DEL DATASET

            datos = new DataSet();
            //CONFIGURAR METODOS DEL ADAPTADOR

            adaptador.SelectCommand = new SqlCommand("SELECT * FROM Cliente", conexion);
            adaptador.InsertCommand = new SqlCommand("INSERT INTO Cliente(Apellidos, Nombres, Direccion, Telefono, Email, Tipo, LimiteCredito) VALUES(@apellidos, @nombres, @direccion, @telefono, @email, @tipo, @limitecredito)", conexion);
            adaptador.InsertCommand.Parameters.Add("@apellidos", SqlDbType.VarChar, 50, "Apellidos");
            adaptador.InsertCommand.Parameters.Add("@nombres", SqlDbType.VarChar, 50, "Nombres");
            adaptador.InsertCommand.Parameters.Add("@direccion", SqlDbType.VarChar, 80, "Direccion");
            adaptador.InsertCommand.Parameters.Add("@telefono", SqlDbType.VarChar, 20, "Telefono");
            adaptador.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar, 40, "Email");
            adaptador.InsertCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 10, "Tipo");
            adaptador.InsertCommand.Parameters.Add("@limitecredito", SqlDbType.Decimal, 50, "LimiteCredito");
            adaptador.InsertCommand.Connection = conexion;

            //
            //adaptador.DeleteCommand = new SqlCommand("DELETE FROM TipoCliente WHERE ID=@id", conexion);
            //adaptador.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "ID");

            adaptador.DeleteCommand = new SqlCommand("DELETE FROM Cliente WHERE ID = @id", conexion);
            adaptador.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 0, "ID");
            adaptador.DeleteCommand.Connection = conexion;

            //dsps
            adaptador.UpdateCommand = new SqlCommand("UPDATE Cliente SET Nombre = @nombre WHERE ID = @id", conexion);
            adaptador.UpdateCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 20, "Nombre");
            adaptador.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");
            adaptador.UpdateCommand.Connection = conexion;


            //antes
            //adaptador.UpdateCommand = new SqlCommand("UPDATE TipoCliente SET Nombre = @nombre WHERE ID = @id");
            //adaptador.UpdateCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 20, "Nombre");
            //adaptador.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");
        }

        private void cargarFormulario(object sender, EventArgs e)
        {
            mostrarDatos();
        }
        private void mostrarDatos()
        {
            //LENAR DATOS AL DATASET(DataTable TipoCliente)
            adaptador.Fill(datos, "Cliente");
            //Enlazar datos al DATAGRIDVIEW
            dgvDatos.DataSource = datos.Tables["Cliente"];
        }

        private void NuevoRegistro(object sender, EventArgs e)
        {
            var frm = new FrmTipoClienteEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nuevaFila = datos.Tables["Cliente"].NewRow();
                nuevaFila["Apellidos"] = frm.Controls["txtap"].Text;
                nuevaFila["Nombres"] = frm.Controls["txtnombres"].Text;
                nuevaFila["Tipo"] = frm.Controls["cbotipos"].Text;
                nuevaFila["LimiteCredito"] = frm.Controls["txtlimite"].Text;
                nuevaFila["Telefono"] = frm.Controls["txttelefono"].Text;
                nuevaFila["Email"] = frm.Controls["txtemail"].Text;
                nuevaFila["Direccion"] = frm.Controls["txtdireccion"].Text;

                datos.Tables["Cliente"].Rows.Add(nuevaFila);
                adaptador.Update(datos.Tables["Cliente"]);
            }
        }

        private void editarRegistro(object sender, EventArgs e)
        {
            var filaActual = dgvDatos.CurrentRow;
            if (filaActual != null)
            {
                var ID = filaActual.Cells[0].Value.ToString();
                DataRow fila = datos.Tables["Cliente"].Select($"ID={ID}").FirstOrDefault();

                var frm = new FrmTipoClienteEdit(fila);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    fila["Apellidos"] = frm.Controls["txtap"].Text;
                }
            }
        }

        private void EliminarRegistro(object sender, EventArgs e)
        {
            var filaActual = dgvDatos.CurrentRow;
            if (filaActual != null)
            {
                var ID = filaActual.Cells[0].Value.ToString();
                var fila = datos.Tables["Cliente"].Select($"ID={ID}").FirstOrDefault();
                if (fila != null)
                {
                    fila.Delete();
                    adaptador.Update(datos.Tables["Cliente"]);
                }
            }
            
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {

        }

        private void tsb_ActualizarBD(object sender, EventArgs e)
        {
            adaptador.Update(datos.Tables["CLiente"]);
            datos.Tables["Cliente"].Clear();
            mostrarDatos();
        }
    }
}
