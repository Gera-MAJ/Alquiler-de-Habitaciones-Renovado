using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Parcial_1_de_LAB_II
{
    public class GestorReservas
    {
        private string usuario;
        private string contrasenia;
        private GestorConexion miGestorConexion;
        private DataTable miDT;
        
        public GestorReservas()
        {
            usuario = "";
            contrasenia = "";
            miGestorConexion = new GestorConexion();
            miDT = new DataTable();
        }

        public void setUsuario(string usuario) 
        {
            this.usuario = usuario;
        }

        public string getUsuario() 
        {
            return usuario;
        }

        public void setContrasenia(string contrasenia) 
        {
            this.contrasenia = contrasenia;
        }

        public string getContrasenia() 
        {
            return contrasenia;
        }

        public DataTable llenar_grid()
        {
            //DataTable miDT = new DataTable();
            string consulta = "SELECT dbo.Reserva.id_Reserva, dbo.Reserva.nombre, dbo.Reserva.dni, dbo.Reserva.Ingreso, dbo.Reserva.Salida, dbo.cant_Personas.cant_Persona, dbo.cant_Personas.precio, dbo.Servicio.tipo_Servicio, dbo.Servicio.Adicional FROM dbo.Reserva INNER JOIN dbo.cant_Personas ON dbo.Reserva.id_Personas = dbo.cant_Personas.id_Personas INNER JOIN dbo.Servicio ON dbo.Reserva.id_Servicio = dbo.Servicio.id_Servicio WHERE dbo.Reserva.id_Reserva IS NOT NULL";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            SqlDataAdapter miDA = new SqlDataAdapter(cmd);
            miDA.Fill(miDT);
            return miDT;
        }
        public DataTable llenar_Grid_Persona()
        {
            //DataTable miDT = new DataTable();
            string consulta = "SELECT dbo.cant_Personas.* FROM dbo.cant_Personas";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            SqlDataAdapter miDA = new SqlDataAdapter(cmd);
            miDA.Fill(miDT);
            return miDT;
        }

        public DataTable llenar_Grid_Servicio()
        {
            //DataTable miDT = new DataTable();
            string consulta = "SELECT dbo.Servicio.* FROM dbo.Servicio";
            SqlCommand cmd = new SqlCommand(consulta, miGestorConexion.getConexion());
            SqlDataAdapter miDA = new SqlDataAdapter(cmd);
            miDA.Fill(miDT);
            return miDT;
        }
    }
}
