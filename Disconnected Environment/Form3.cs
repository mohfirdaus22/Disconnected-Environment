﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Disconnected_Environment
{


    public partial class Form3 : Form
    {

        private string stringConnection = "data source=Jorengezzz\\THEPASHTER;" +
           "database=Activity6;User ID=sa;Password=Salahtompo22";
        private SqlConnection koneksi;
        private string nim, nama, alamat, jk, prodi;
        private DateTime tgl;
        BindingSource customersBindingSource = new BindingSource();
        private string kstr;

        private void button3_Click(object sender, EventArgs e)
        {
          this.txtNIM.DataBindings.Clear();
          this.txtNama.DataBindings.Clear();
          this.txtAlamat.DataBindings.Clear();
          this.cbxJenisKelamin.DataBindings.Clear();
          this.dtTanggalLahir.DataBindings.Clear();
          this.cbxProdi.DataBindings.Clear();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNIM.Text = "";
            txtNama.Text = "";
            txtAlamat.Text = "";
            dtTanggalLahir.Value = DateTime.Today;
            txtNIM.Enabled = true;
            txtNama.Enabled = true;
            cbxJenisKelamin.Enabled = true;
            txtAlamat.Enabled = true;
            txtAlamat.Enabled = true;
            dtTanggalLahir.Enabled = true;
            cbxProdi.Enabled = true;
            Prodicbx();
            btnSave.Enabled = true;
            btnClear.Enabled = true;
            btnAdd.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            nim = txtNIM.Text;
            nama = txtNama.Text;
            jk = cbxJenisKelamin.Text;
            alamat = txtAlamat.Text;
            tgl = dtTanggalLahir.Value;
            prodi = cbxProdi.Text;
            int hs = 0;
            koneksi.Open();
            string strs = "select id_prodi from dbo.Prodi where nama_prodi = @dd";
            SqlCommand cm = new SqlCommand(strs, koneksi);
            cm.CommandType = CommandType.Text;
            cm.Parameters.Add(new SqlParameter("@dd", prodi));
            SqlDataReader dr = cm.ExecuteReader();
            while (dr.Read())
            {
                hs = int.Parse(dr["id_prodi"].ToString());
            }

            dr.Close();
            string str = "insert into dbo.Data_Mahasiswa (nim, nama_mahasiswa, jenis_kelamin, alamat, tgl_lahir, id_prodi)" +
                "values(@NIM, @Nm, @Jk, @Al, @Tgl, @Idp)";
            SqlCommand cmd = new SqlCommand(str, koneksi);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("NIM", nim));
            cmd.Parameters.Add(new SqlParameter("Nm", nama));
            cmd.Parameters.Add(new SqlParameter("Jk", jk));
            cmd.Parameters.Add(new SqlParameter("Al", alamat));
            cmd.Parameters.Add(new SqlParameter("Tgl", tgl));
            cmd.Parameters.Add(new SqlParameter("Idp", hs));
            cmd.ExecuteNonQuery();
            koneksi.Close();
            MessageBox.Show("Data Berhasill Disimpan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            refreshform();
        }


        private void refreshform()
        {
            txtNIM.Enabled = false;
            txtNama.Enabled = false;
            cbxJenisKelamin.Enabled = false;
            txtAlamat.Enabled = false;
            dtTanggalLahir.Enabled = false;
            cbxProdi.Enabled = false;
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnClear.Enabled = false;
            clearBinding();
            FormDataMahasiswa_Load();
        }

       

        public Form3()
        {
            InitializeComponent();
            koneksi = new SqlConnection(kstr);
            this.bnMahasiswa.BindingSource = this.customersBindingSource;
            refreshform();

        }

        private void FormDataMahasiswa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'data_MahasiswaDataSet1.Mahasiswa' table. You can move, or remove it, as needed.
            this.mahasiswaTableAdapter.Fill(this.data_MahasiswaDataSet1.Mahasiswa);
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, m.alamat, " +
                "m.jenis_kelamin, m.tgl_lahir,p.nama_prodi From dbo.Mahasiswa, " +
                "join dbo.Prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customersBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "NIM", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "jenis_kelamin", true));
            this.dtTanggalLahir.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_prodi", true));
            koneksi.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            refreshform();
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'data_MahasiswaDataSet1.Mahasiswa' table. You can move, or remove it, as needed.
            this.mahasiswaTableAdapter.Fill(this.data_MahasiswaDataSet1.Mahasiswa);
            koneksi.Open();
            SqlDataAdapter dataAdapter1 = new SqlDataAdapter(new SqlCommand("Select m.nim, m.nama_mahasiswa, m.alamat, " +
                "m.jenis_kelamin, m.tgl_lahir,p.nama_prodi From dbo.Mahasiswa, " +
                "join dbo.Prodi p on m.id_prodi = p.id_prodi", koneksi));
            DataSet ds = new DataSet();
            dataAdapter1.Fill(ds);

            this.customersBindingSource.DataSource = ds.Tables[0];
            this.txtNIM.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "NIM", true));
            this.txtNama.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_mahasiswa", true));
            this.txtAlamat.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "alamat", true));
            this.cbxJenisKelamin.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "jenis_kelamin", true));
            this.dtTanggalLahir.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "tgl_lahir", true));
            this.cbxProdi.DataBindings.Add(
                new Binding("Text", this.customersBindingSource, "nama_prodi", true));
            koneksi.Close();
        }

        private void FormDataMahasiswa_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormHalamanUtama fm = new FormHalamanUtama();
            fm.Show();
            this.Hide();
        }

    }
}
