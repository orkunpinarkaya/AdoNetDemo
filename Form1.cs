using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ProductDal _productdal = new ProductDal();
        private void Form1_Load(object sender, EventArgs e)
        {

            TabloGetir();
        }
        private void TabloGetir()
        {
            dgwProducts.DataSource = _productdal.GelAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productdal.Add(new Product
            {
                Name = tbxName.Text,
                UnitPrice = Convert.ToInt32(tbxUnitPrice.Text),
                StockAmount = Convert.ToInt32(tbxStockAmount.Text)

            });
            MessageBox.Show("product added");
            TabloGetir();
        }

        private void dgwProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgwProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxNameUpdate.Text = dgwProducts.CurrentRow.Cells[1].Value.ToString();
            tblUnitPriceUpdate.Text = dgwProducts.CurrentRow.Cells[2].Value.ToString();
            tbxStockAmountUpdate.Text = dgwProducts.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value),
                Name = tbxNameUpdate.Text,
                UnitPrice= Convert.ToDecimal(tblUnitPriceUpdate.Text),
                StockAmount = Convert.ToInt32(tbxStockAmountUpdate.Text)

            };
            _productdal.Update(product);
            TabloGetir();
            MessageBox.Show("Updated");

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgwProducts.CurrentRow.Cells[0].Value);
            _productdal.Delete(id);
            MessageBox.Show("deleted");
            TabloGetir();
        }
    }
}
