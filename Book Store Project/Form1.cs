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

namespace Book_Store_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Data Source=DESKTOP-BDSUH1M\SQLEXPRESS;Initial Catalog="SQL MURAT Y";Integrated Security=True
        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-BDSUH1M\SQLEXPRESS; Initial Catalog = SQL MURAT Y; Integrated Security = True");
        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select   Bookid,BookName,Writer,Pages,Price,Publisher,TypeName from tblkitaplar inner join tblturler on tblkitaplar.type=tblturler.typeid", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void Turler()
        {
            SqlCommand komut = new SqlCommand("Select * From TblTurler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbtur.ValueMember = "Typeid";
            cmbtur.DisplayMember = "TypeName";
            cmbtur.DataSource = dt;

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            Listele();
            Turler();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutekle = new SqlCommand("insert into TblKitaplar (BookName,Writer,Pages,Price,Publisher,Type) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglanti);
            komutekle.Parameters.AddWithValue("@p1", txtad.Text);
            komutekle.Parameters.AddWithValue("@p2", txtyazar.Text);
            komutekle.Parameters.AddWithValue("@p3", txtsayfa.Text);
            komutekle.Parameters.AddWithValue("@p4", txtfiyat.Text);
            komutekle.Parameters.AddWithValue("@p5", txtyayinevi.Text);
            komutekle.Parameters.AddWithValue("@p6",(cmbtur.SelectedIndex+1));
            komutekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Book Succesfuly addedd to the list.", "Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtyazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtsayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtfiyat.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtyayinevi.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("delete from TblKitaplar where bookid=@p1",baglanti);
            komutsil.Parameters.AddWithValue("@p1", txtid.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Selected book deleted from the list.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Listele();
        }

        private void btnguncel_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncel = new SqlCommand("update  TblKitaplar set bookname=@p1,writer=@p2,pages=@p3,price=@p4,publisher=@p5,type=@p6 where bookid=@p7", baglanti);
            komutguncel.Parameters.AddWithValue("@p1", txtad.Text);
            komutguncel.Parameters.AddWithValue("@p2", txtyazar.Text);
            komutguncel.Parameters.AddWithValue("@p3", txtsayfa.Text);
            komutguncel.Parameters.AddWithValue("@p4", txtfiyat.Text);
            komutguncel.Parameters.AddWithValue("@p5", txtyayinevi.Text);
            komutguncel.Parameters.AddWithValue("@p6", (cmbtur.SelectedIndex+1));
            komutguncel.Parameters.AddWithValue("@p7", txtid.Text);
            komutguncel.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Book Information Updated","Information",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Listele();
        }
    }
}
