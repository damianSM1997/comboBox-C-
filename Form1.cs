using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cargarEstados();
        }

        private void cargarEstados()
        {
            cbxEstado.DataSource = null;
            cbxEstado.Items.Clear();

            cbxMunicipio.DataSource = null;
            cbxMunicipio.Items.Clear();
            cbxLocalidad.DataSource = null;
            cbxLocalidad.Items.Clear();

            string sql = "SELECT id_estado, estado FROM t_estado ORDER BY estado ASC";

            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable table = new DataTable();
                data.Fill(table);
                cbxEstado.ValueMember = "id_estado";
                cbxEstado.DisplayMember = "estado";
                cbxEstado.DataSource = table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

        }

        private void cbxEstado_SelectionChangeCommitted(object sender, EventArgs e)
        {

            cbxMunicipio.DataSource = null;
            cbxMunicipio.Items.Clear();
            cbxLocalidad.DataSource = null;
            cbxLocalidad.Items.Clear();
            int idEstado = int.Parse(cbxEstado.SelectedValue.ToString());
            string sql = "SELECT id_municipio, municipio FROM t_municipio WHERE id_estado='"+ idEstado + "' ORDER BY municipio ASC";

            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable table = new DataTable();
                data.Fill(table);
                cbxMunicipio.ValueMember = "id_municipio";
                cbxMunicipio.DisplayMember = "municipio";
                cbxMunicipio.DataSource = table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }

        }       

        private void cbxMunicipio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbxLocalidad.DataSource = null;
            cbxLocalidad.Items.Clear();
            int idMunicipio = int.Parse(cbxMunicipio.SelectedValue.ToString());
            string sql = "SELECT id_localidad, localidad FROM t_localidad WHERE id_municipio='" + idMunicipio + "' ORDER BY localidad ASC";

            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable table = new DataTable();
                data.Fill(table);
                cbxLocalidad.ValueMember = "id_localidad";
                cbxLocalidad.DisplayMember = "localidad";
                cbxLocalidad.DataSource = table;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
            finally
            {
                conexionDB.Close();
            }
        }
    }
}
