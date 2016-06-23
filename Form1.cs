/*=========================
FileName: FinalProject
FileType: Visual C#
Author: Karim El-Tabche & Kyle Martin
Created On: 6/14/2016 4:21:09 PM
Last Modified On: 6/22/2016 1:03 PM
Description: This is a Shopping Cart program
             that uses XML and LINQ to store 
             and display products. It allows
             you to search for a specific 
             item in database and add it
             to cart and also remove items
             from cart. After done adding 
             items, you can calculate 
             your total and checkout. 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

namespace FinalProject
{
    public partial class frmShoppingCart : Form
    {

        XmlDocument xdoc = new XmlDocument(); //Create new xml document called xdoc
        string selected;//assign selected item to a variable called selected

        Product prod = new Product();// Create an object of product 
        string[] product = new[] { "", "" };

        List<string> listProd = new List<string>();

        public frmShoppingCart()
        {
            InitializeComponent();
        }

        public frmShoppingCart(ref List<string> prod)
        {
            InitializeComponent();
        }
       

        private void frmShoppingCart_Load(object sender, EventArgs e)
        {
            xdoc.Load("database\\Products.xml");//Load the xml document
            
            XmlNodeList list = xdoc.GetElementsByTagName("name");//Get the products name

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Search through the database for keyword typed in the search textbox
            //And displays results in the listbox

           
            lstSearch.Items.Clear();//Clears items in list 
            xdoc.Load("database\\Products.xml");

            XmlNodeList productPrice = xdoc.GetElementsByTagName("price");

            string search = txtSearch.Text;

            if (txtSearch.Text == "") // checks to see if search text box is empty before performing search
            {
                MessageBox.Show("Please enter keyword to search for");
            }
            else
            {

                for (int i = 0; i < productPrice.Count; i++)
                {
                    product[0] = xdoc.GetElementsByTagName("name")[i].InnerText; // Assign 
                    product[1] = xdoc.GetElementsByTagName("price")[i].InnerText;

                    if (product[0].ToLower().Contains(search))//Checks if items are found in database
                    {
                        lstSearch.Items.Add(product[0].PadRight(50) + "$" + product[1]);//Display items in listbox
                    }

                }
                if (lstSearch.Items.Count < 1)
                {
                    MessageBox.Show("Sorry, Item not found");//If item is not found display a message
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Adds every item selected to the cart listbox
            
            selected = lstSearch.SelectedItem.ToString(); 
            prod.Price= double.Parse(selected.Substring(selected.LastIndexOf("$")+1)); //Grabs the price from each item and store it in a variable
            prod.Total += prod.Price; // Adds price to total every time an item is added

            listProd.Add(lstSearch.SelectedItem.ToString());

            MessageBox.Show(selected.Substring(0, selected.IndexOf("  ")) + " has been added to your cart");

        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //menu bar exit button closes application
        {
            Application.Exit();
        }

        private void checkoutToolStripMenuItem_Click(object sender, EventArgs e) // menu bar checkout button takes you to shopping cart form2
        {
            frmCartProcessing form2 = new frmCartProcessing(ref listProd, prod.Total);
            form2.ShowDialog();
        }
    }
}
