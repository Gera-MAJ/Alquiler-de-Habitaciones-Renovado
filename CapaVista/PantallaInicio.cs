using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial_1_de_LAB_II
{
    public partial class frmPantallaPrincipal : Form
    {
        //Jatip Gerardo Miguel Angel
        //LEGAJO: 54305

        private GestorReservas miGestorResevas;

        public frmPantallaPrincipal()
        {
            InitializeComponent();
            miGestorResevas = new GestorReservas();
        }


        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {
            miGestorResevas.setUsuario (txtUsuario.Text);
        }
        private void txtContrasenia_TextChanged(object sender, EventArgs e)
        {
            miGestorResevas.setContrasenia(txtContrasenia.Text);
        }

        private void btnIngreso_Click(object sender, EventArgs e)
        {
            if (miGestorResevas.getUsuario() == "geramaj" && miGestorResevas.getContrasenia() == "cardio87")
            {
                this.Hide();
                frmCargaReserva frmCargaReserva1 = new frmCargaReserva();
                frmCargaReserva1.Show();
                MessageBox.Show("Bienvenid@ "+ miGestorResevas.getUsuario() +", a Ooking!!");
            }
            else 
            {
                MessageBox.Show("Debe ingresar un usuario y contraseña correcto");
            }
        }
    }
}
