using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Parcial_1_de_LAB_II
{
    public partial class frmCargaReserva : Form
    {
        //Jatip Gerardo Miguel Angel
        //LEGAJO: 54305

        private GestorConexion miGestorConexion;
        private GestorReservas miGestorReservas;
        private string consulta;
        private int idPersona;
        private int idServicio;
        public string usuario;
     


        public frmCargaReserva()
        {
            InitializeComponent();

            miGestorConexion = new GestorConexion();
            miGestorReservas = new GestorReservas();
            consulta = "";
            idPersona = 0;
            idServicio = 0;
            usuario = "";
        
        }


        private void frmPaginaDestinos_Load(object sender, EventArgs e)
        {
            
            lblSaludoConNombre.Text = "Bienvenid@ a Ooking, cargá la reserva aquí";
            rbReservas.Checked = true;

            if (rbReservas.Checked == true) 
            {
                dgvReservas.DataSource = miGestorReservas.llenar_grid();
                dgvReservas.ReadOnly = true;
                txtCodigo.ReadOnly = true;
                llenarComboPersonas();
                llenarComboServicios();
                dgvReservas.ClearSelection();              
            }         
        }

        public void borrarCampos() 
        {
            txtNombre.Clear();
            txtDNI.Clear();
            txtCodigo.Clear();
            dtpIngreso.Value = DateTime.Now;
            dtpSalida.Value = DateTime.Now;
            //dtpSalida.Value = Convert.ToDateTime("01/01/2000");
            cboCantPersonas.SelectedIndex = -1;
            cboServicio.SelectedIndex = -1;

            
        }
        public void llenarComboPersonas() 
        {
            //llenar un comboBox con datos de la tabla
            string consulta = "SELECT cant_Persona FROM cant_Personas";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            miGestorConexion.abrirConexion();
            SqlDataReader miReader = cmd.ExecuteReader();
            while (miReader.Read()) 
            { 
                cboCantPersonas.Items.Add(miReader["cant_Persona"].ToString());
            }
            miGestorConexion.cerrarConexion();
        }

        public void llenarComboServicios() 
        {
            //llenar un comboBox con datos de la tabla
            string consulta = "SELECT tipo_Servicio FROM Servicio";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            miGestorConexion.abrirConexion();
            SqlDataReader miReader = cmd.ExecuteReader();
            while (miReader.Read())
            {
                cboServicio.Items.Add(miReader["tipo_Servicio"].ToString());
            }
            miGestorConexion.cerrarConexion();
        }

        /*public DataTable llenar_grid()
        {
            DataTable miDT = new DataTable();
            string consulta = "SELECT dbo.Reserva.id_Reserva, dbo.Reserva.nombre, dbo.Reserva.dni, dbo.Reserva.Ingreso, dbo.Reserva.Salida, dbo.cant_Personas.cant_Persona, dbo.cant_Personas.precio, dbo.Servicio.tipo_Servicio, dbo.Servicio.Adicional FROM dbo.Reserva INNER JOIN dbo.cant_Personas ON dbo.Reserva.id_Personas = dbo.cant_Personas.id_Personas INNER JOIN dbo.Servicio ON dbo.Reserva.id_Servicio = dbo.Servicio.id_Servicio WHERE dbo.Reserva.id_Reserva IS NOT NULL";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            SqlDataAdapter miDA = new SqlDataAdapter(cmd);
            miDA.Fill(miDT);
            return miDT;

        }*/

        /*private void btnAgregar_Click(object sender, EventArgs e)
        {
            int valor = 0;
            int indice = 0;
            int idReserva = 0;

            string consulta = "INSERT INTO Reserva (id_Reserva, nombre, dni, Ingreso, Salida, id_Personas, id_Servicio) VALUES (@id_Reserva, @nombre, @dni, @Ingreso, @Salida, @id_Personas, @id_Servicio)";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            valor = dgvReservas.Rows.Count;
            indice = valor - 2;
            idReserva = Convert.ToInt32(dgvReservas.Rows[indice].Cells[0].Value) + 1;
            cmd.Parameters.AddWithValue("@id_Reserva", idReserva);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
            cmd.Parameters.AddWithValue("@Ingreso", dtpIngreso.Value.Date);
            cmd.Parameters.AddWithValue("@Salida", dtpSalida.Value.Date);
            cmd.Parameters.AddWithValue("@id_Personas", idPersona);
            cmd.Parameters.AddWithValue("@id_Servicio", idServicio);
            miGestorConexion.abrirConexion();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron ingresados");
            dgvReservas.DataSource = miGestorReservas.llenar_grid();
            miGestorConexion.cerrarConexion();
            borrarCampos();
        }*/

        private void btnEditar_Click(object sender, EventArgs e)
        {
            consulta = "UPDATE Reserva SET id_Reserva=@id_Reserva, nombre=@nombre, dni=@dni, Ingreso=@Ingreso, Salida=@Salida, id_Personas=@id_Personas, id_Servicio=@id_Servicio WHERE id_Reserva=@id_Reserva";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            cmd.Parameters.AddWithValue("@id_Reserva", txtCodigo.Text);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
            cmd.Parameters.AddWithValue("@Ingreso", dtpIngreso.Value.Date);
            cmd.Parameters.AddWithValue("@Salida", dtpSalida.Value.Date);
            cmd.Parameters.AddWithValue("@id_Personas", idPersona);
            cmd.Parameters.AddWithValue("@id_Servicio", idServicio);
            miGestorConexion.abrirConexion();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Los datos se cambiaron correctamente");
            dgvReservas.DataSource = miGestorReservas.llenar_grid();
            miGestorConexion.cerrarConexion();
            borrarCampos();
        }

     

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string consulta = "DELETE FROM Reserva WHERE id_Reserva=@id_Reserva";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            cmd.Parameters.AddWithValue("@id_Reserva", txtCodigo.Text);
            miGestorConexion.abrirConexion();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Se borraron los datos correctamente");
            dgvReservas.DataSource = miGestorReservas.llenar_grid();
            miGestorConexion.cerrarConexion();
            borrarCampos();
                
        }

        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = e.RowIndex;

            if (indice != -1)
            {
                txtCodigo.Text = dgvReservas.Rows[indice].Cells[0].Value.ToString();
                txtNombre.Text = dgvReservas.Rows[indice].Cells[1].Value.ToString();
                txtDNI.Text = dgvReservas.Rows[indice].Cells[2].Value.ToString();
                cboCantPersonas.SelectedItem = dgvReservas.Rows[indice].Cells[5].Value;
                cboServicio.SelectedItem = dgvReservas.Rows[indice].Cells[7].Value;
            }
            else 
            {
                MessageBox.Show("Seleccione un campo válido");
            }
        }

        private void cboCantPersonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            index = cboCantPersonas.SelectedIndex;
            idPersona = index + 1;
        }

        private void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            index = cboServicio.SelectedIndex;
            idServicio = index + 1;
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            int valor = 0;
            int indice = 0;
            int idReserva = 0;

            string consulta = "INSERT INTO Reserva (id_Reserva, nombre, dni, Ingreso, Salida, id_Personas, id_Servicio) VALUES (@id_Reserva, @nombre, @dni, @Ingreso, @Salida, @id_Personas, @id_Servicio)";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            valor = dgvReservas.Rows.Count;
            indice = valor - 2;
            idReserva = Convert.ToInt32(dgvReservas.Rows[indice].Cells[0].Value) + 1;
            cmd.Parameters.AddWithValue("@id_Reserva", idReserva);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@dni", txtDNI.Text);
            cmd.Parameters.AddWithValue("@Ingreso", dtpIngreso.Value.Date);
            cmd.Parameters.AddWithValue("@Salida", dtpSalida.Value.Date);
            cmd.Parameters.AddWithValue("@id_Personas", idPersona);
            cmd.Parameters.AddWithValue("@id_Servicio", idServicio);
            miGestorConexion.abrirConexion();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Los datos fueron ingresados");
            dgvReservas.DataSource = miGestorReservas.llenar_grid();
            miGestorConexion.cerrarConexion();
            borrarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            //Botón para preguntar si se quiere salir
            if (MessageBox.Show("Está Seguro?", "Cerrar Ooking", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes) 
            {
                Application.Exit();
            }

        }

        private void rbPersonas_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonas.Checked == true) 
            {
                dgvReservas.DataSource = "";
                dgvReservas.Refresh();
                dgvReservas.DataSource = miGestorReservas.llenar_Grid_Persona();
                dgvReservas.ReadOnly = true;
                txtCodigo.ReadOnly = true;
                //llenarComboPersonas();
                //llenarComboServicios();
                dgvReservas.ClearSelection();
            }
        }

        private void rbServicio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbServicio.Checked == true)
            {
                dgvReservas.DataSource = "";
                dgvReservas.Refresh();
                dgvReservas.DataSource = miGestorReservas.llenar_Grid_Servicio();
                dgvReservas.ReadOnly = true;
                txtCodigo.ReadOnly = true;
                //llenarComboPersonas();
                //llenarComboServicios();
                dgvReservas.ClearSelection();
            }
        }
    }
}
