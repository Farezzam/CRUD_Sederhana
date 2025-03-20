using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Sederhana
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=Data Source=LAPTOP-JICJ6MBI\\FARISNAUFAL;" + "Initial Catalog=OrganisasiMahasiswa2;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void clearForm()
        {
            txtNIM.Clear();
            txtNama.Clear();
            txtEmail.Clear();
            txtTelepon.Clear();
            txtAlamat.Clear();

            txtNIM.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT NIM AS [NIM], Nama, Email, Telepon, Alamat FROM MAHASISWA";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMahasiswa.AutoGenerateColumns = true;
                    dgvMahasiswa.DataSource = dt;

                    clearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtNIM.Text == "" || txtNama.Text == "" || txtEmail.Text == "" || txtTelepon.Text == "" || txtAlamat.Text == "")
                    {
                        MessageBox.Show("Semua data harus diisi", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO MAHASISWA (NIM, Nama, Email, Telepon, Alamat) VALUES (@NIM, @Nama, @Email, @Telepon, @Alamat)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        cmd.Parameters.AddWithValue("@Nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Telepon", txtTelepon.Text);
                        cmd.Parameters.AddWithValue("@Alamat", txtAlamat.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            clearForm();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal ditambahkan", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
    
}
    
