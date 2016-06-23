using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class frmCartProcessing : Form
    {
        Product prod = new Product();
        List<string> prodName = new List<string>();
        Tax t = new Tax();
        double totalAmount;
        double price;
        

        public frmCartProcessing(ref List<string> productName, double total)
        {
            InitializeComponent();

            prodName = productName;
            prod.Total = total;
        }

        private void frmCartProcessing_Load(object sender, EventArgs e)
        {

            foreach (string n in prodName) // loads selected items into listbox that displays product name and price
            {
                lstCart.Items.Add(n);
            }

            if(lstCart.Items.Count >= 1) //checks if there are any items in the cart
            {                            //if there is none, the checkout and remove button are disabled
                btnRemove.Enabled = true;
                btnCheckout.Enabled = true;
               
            }
            else
            {
                btnRemove.Enabled = false;
                btnCheckout.Enabled = false;
            }

            foreach(string n in prodName)
            {
               price += double.Parse(n.Substring(n.LastIndexOf("$") + 1));
            }


            prod.Total = price;
            totalAmount = t.TaxAdded(prod.Total);

            lblTotal.Text = totalAmount.ToString("C2"); // sets the label to the total of the order

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //code to remove selected item from cart
            if (lstCart.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose an item to be removed");
            }
            else
            {
                string select = lstCart.SelectedItem.ToString();

                prod.Price = double.Parse(select.Substring(select.LastIndexOf("$") + 1));
                prod.Total -= prod.Price;
                prodName.Remove(lstCart.SelectedItem.ToString());
                lstCart.Items.RemoveAt(lstCart.SelectedIndex);
                totalAmount = t.TaxAdded(prod.Total);             //item is removed and price is adjusted
                lblTotal.Text = totalAmount.ToString("C2");

            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            
            this.Hide(); // back button returns the user to form1
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstCart.Items.Count <= 0)
            {
                MessageBox.Show("Nothing in your cart. Add items to continue checkout");
            }
            else
            {

                frmCheckout form3 = new frmCheckout(prod.Total);

                form3.ShowDialog();
            }
        }

        private void clearCartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //menu strip clear button that clears content of cart and removes product from created list
            try 
            {
                lstCart.Items.Clear();
                foreach (string n in prodName.ToList())
                {
                    prodName.Remove(n);
                }
                prod.Total = 0;
                lblTotal.Text = prod.Total.ToString("c2");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
