using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FallLab2
{
    public partial class StudentLoginForm : Form
    {
        public StudentLoginForm()
        {
            InitializeComponent();
            //txtbxAdminID.Text = "admin";
           // txtbxPassword.Text = "123";
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            int ID=-1;
            bool validID = false;

            if(txtbxAdminID.Text == "admin" && txtbxPassword.Text == "123")
            {
                //Launch the form
                //MessageBox.Show("Login Information Is Correct!");
                Admin form = new Admin( this);
                txtbxAdminID.Text = "";
                txtbxPassword.Text = "";
                StudentIDTextBox.Text = "";
                form.Show();
                return;
            }
            else if (StudentIDTextBox.Text.Length > 0)
            {
                //Determine of the value is numeric. If so, return it in sd.startPos
                if (int.TryParse(StudentIDTextBox.Text, out ID))
                {
                    //ID is numeric, now check if it is in the database
                    string connectionString = null;
                    string sql = null;
                    OleDbConnection oledbCnn;
                    OleDbDataAdapter adapter;
                    OleDbCommand command;
                    string cmd = "";

                    //connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Projects\Fiverr\shawnembry\ProjectZZ\ChattBankMDB.mdb";
                    connectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Reg" +
                                        "istry Path =; Jet OLEDB:Database L" +
                                        "ocking Mode=1;Data Source=E:\\Projects\\Fiverr\\shawnembry\\Labs\\Lab2\\FallLab2\\RegistrationMDB.mdb;J" +
                                        "et OLEDB:Engine Type=5;Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:System datab" +
                                        "ase=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=S" +
                                        "hare Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet " +
                                        "OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repai" +
                                        "r=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
                    oledbCnn = new OleDbConnection(connectionString);

                    cmd = "Select * from Students where ID = " + ID.ToString();
                    command = new OleDbCommand(cmd, oledbCnn);
                    adapter = new OleDbDataAdapter(command);
                    DataTable td = new DataTable();
                    try {    
                        adapter.Fill(td);
                        foreach (DataRow row in td.Rows)
                        {
                            //Find the customer to match
                            if ((int)row.ItemArray[0] == ID)
                            {
                                validID = true;
                                break;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        oledbCnn.Close();                        
                    }

                }
            }

            if (validID)
            {
                //Launch the form
                //MessageBox.Show("Login Information Is Correct!");
                CIS2342.Form1 form = new CIS2342.Form1(ID, this);
                txtbxAdminID.Text = "";
                txtbxPassword.Text = "";
                StudentIDTextBox.Text = "";
                form.Show();
            }
            else
            {
                MessageBox.Show("Login Information Is Wrong");
            }
        }

        private void StudentIDTextBox_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
