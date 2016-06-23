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
    public partial class frmCheckout : Form
    {
        Tax taxed = new Tax();
        double totalAmount, total;


        public frmCheckout(double t) // constructor that takes in the price from form2(CartProcessing)
        {
            InitializeComponent();
            total = t;
            totalAmount = taxed.TaxAdded(total);

        }

        private void rdbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCash.Checked) //if cash radio button is checked, cash options are displayed
            {
                grpCash.Visible = true;
                grpCredit.Visible = false;
            }
        }

        private void rdbCredit_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCredit.Checked) // if credit radio button is checked, credit options are displayed
            {
                grpCredit.Visible = true;
                grpCash.Visible = false;

                for(int i = 1; i < 13; ++i)
                {
                    cmbMonth.Items.Add(i.ToString());
                }

                for(int i = 2016; i < 2030; ++i)
                {
                    cmbYear.Items.Add(i.ToString());
                }
            }
        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            //when form loads neither radio button is selected and total is loaded into appropriate label
            rdbCash.Checked = false;
            rdbCredit.Checked = false;


            lblTotal.Text = totalAmount.ToString("C2");
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //code for submit button that includes entered data validation
            double cashPaid;
            double change;

            if (rdbCash.Checked)
            {
                try
                {
                    cashPaid = double.Parse(txtCash.Text);

                    if(cashPaid < totalAmount) //checks to make sure cash paid is greater than amount due
                    {
                        MessageBox.Show("Insufficient funds!");
                    }
                    else
                    {
                        change = cashPaid - totalAmount;
                        MessageBox.Show("Payment is processed.\nThank you for shopping at K&K Appliances Inc.\nYour change is " + change.ToString("C2"));
                        Application.Exit();
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Must be in numeric format");
                }
            }
            else if(rdbCredit.Checked)
            {
                string name;
                Int64 ccNum;
                int cvv;

                try
                {
                    name = txtName.Text;
                    cvv = int.Parse(txtCVV.Text);

                    if (cmbMonth.SelectedIndex == -1 || cmbYear.SelectedIndex == -1) //makes sure exp. date is selected
                    {
                        MessageBox.Show("Please enter your card expiration date");
                    }

                    else if(txtCredit.Text.Length != 16) //checks to make sure cc num is 16 digits
                    {
                        MessageBox.Show("Enter a valid Card number");
                    }

                    else if(txtCVV.Text.Length != 3) // checks to make sure CVV code is 3 digits
                    {
                        MessageBox.Show("Please enter a valid Security code number.\nIt is found on the back of the card");
                    }
                
                   else
                    {
                        //if all data is valid, values are set to entered data
                        ccNum = Int64.Parse(txtCredit.Text);
                        cvv = int.Parse(txtCVV.Text);
                        MessageBox.Show("Payment is processed,\nThank you for shopping at K&K Appliances Inc.");
                        Application.Exit();
                    }

                }

                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
