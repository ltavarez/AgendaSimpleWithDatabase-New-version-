using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer.Servicios;
using Database.modelos;

namespace AgendaSerializada
{
    public partial class FrmPrincipal : Form
    {
        private ServicioPersona servicio;
        private int id { get; set; } 
        public FrmPrincipal()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            servicio = new ServicioPersona(connection);
            id = -1;
        }

        #region Eventos

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            LoadPersona();
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if(id < 0)
            {
                AddPersona();
            }
            else
            {
                EditPersona();
            }
            
        }      

        private void DgvPersonas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                id = Convert.ToInt32(DgvPersonas.Rows[e.RowIndex].Cells[0].Value.ToString());
                BtnDeseleccionar.Visible = true;
                LoadEditPersona();
            }
        }

        private void cerrarSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnDeseleccionar_Click(object sender, EventArgs e)
        {
            Deseleccionar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        #endregion

        #region "Metodos privado"

        private void AddPersona()
        {
            Persona nuevaPersona = new Persona
            {
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Telefono = TxtTelefono.Text
            };

            bool respuesta = servicio.Add(nuevaPersona);

            if (respuesta)
            {
                MessageBox.Show("Se ha creado con exito", "Notificacion");
            }
            else
            {
                MessageBox.Show("Ooops ha ocurrido un error", "Error");
            }

           

            Clear();
            LoadPersona();
        }

        private void EditPersona()
        {
            Persona nuevaPersona = new Persona
            {
                Id = id,
                Nombre = TxtNombre.Text,
                Apellido = TxtApellido.Text,
                Telefono = TxtTelefono.Text
            };

            bool respuesta = servicio.Edit(nuevaPersona);

            if (respuesta)
            {
                MessageBox.Show("Se ha editado con exito", "Notificacion");
            }
            else
            {
                MessageBox.Show("Ooops ha ocurrido un error", "Error");
            }

           

            Clear();
            Deseleccionar();
            LoadPersona();
        }

        private void Clear()
        {
            TxtNombre.Clear();
            TxtApellido.Clear();
            TxtTelefono.Clear();
        }

        private void Eliminar()
        {
            if(id < 0)
            {
                MessageBox.Show("Debe seleccionar una persona", "Advertencia");
            }
            else
            {

                DialogResult respuesta = MessageBox.Show("Esta seguro que desea eliminar esta persona?", "Confirmacion"
                    , MessageBoxButtons.OKCancel);

                if(respuesta == DialogResult.OK)
                {                    

                    bool resultado = servicio.Delete(id);

                    if (resultado)
                    {
                        MessageBox.Show("Se ha eliminado con exito", "Notificacion");
                    }
                    else
                    {
                        MessageBox.Show("Ooops ha ocurrido un error", "Error");
                    }

                    LoadPersona();
                    Deseleccionar();
                }

            }


        }

        private void LoadEditPersona()
        {
            Persona personaAEditar = servicio.GetById(id);

            TxtNombre.Text = personaAEditar.Nombre;
            TxtApellido.Text = personaAEditar.Apellido;
            TxtTelefono.Text = personaAEditar.Telefono;

        }

        private void LoadPersona()
        {         
            DgvPersonas.DataSource = servicio.GetAll();
            DgvPersonas.ClearSelection();
        }

        private void Deseleccionar()
        {
            DgvPersonas.ClearSelection();
            BtnDeseleccionar.Visible = false;
            Clear();
            id = -1;
        }

        #endregion

       
    }
}
