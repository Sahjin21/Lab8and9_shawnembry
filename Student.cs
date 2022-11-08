using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/************************
 * Shawn Embry
 * CIS 2342 C# II
 * Lab 5
 * 10-06-2020
 * **********************/
namespace CIS2342
{
    class Student:Person
    {
        //properties
        public string StudentID { get; set; }
        public double GPA { get; set; }

        Schedule sched = new Schedule();


        public Student()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Street = "";
            City = "";
            State = "";
            Zip = 0;
            this.StudentID = "";
            this.GPA = 0;
        }
        public Student(string StudentID, string firstName, string lastName, string email, double GPA, Address a, Schedule s): base(firstName, lastName, email, a, s)
        
        {
            //Inherited from Person
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            //Member of Student Class
            this.StudentID = StudentID;
            this.GPA = GPA;
        }

        //behaviors
        public string GetStudentID() { return StudentID; }
        public void GetStudentID(string id) { StudentID = id; }
        public double GetGPA() { return GPA; }
        public void GetGPA(double GPA) { this.GPA = GPA; }

   

        //++++++++++++++++  DATABASE Data Elements +++++++++++++++++
        public System.Data.OleDb.OleDbDataAdapter OleDbDataAdapter2;
        public System.Data.OleDb.OleDbCommand OleDbSelectCommand2;
        public System.Data.OleDb.OleDbCommand OleDbInsertCommand2;
        public System.Data.OleDb.OleDbCommand OleDbUpdateCommand2;
        public System.Data.OleDb.OleDbCommand OleDbDeleteCommand2;
        public System.Data.OleDb.OleDbConnection OleDbConnection2;
        public string cmd;
        public void StudentDisplay()
        {
            Console.WriteLine("Person StudentID = " + StudentID);
            Console.WriteLine("Person Firstname = " + FirstName);
            Console.WriteLine("Person Last Name = " + LastName);
            Console.WriteLine("Person Email = " + Email);          
            Console.WriteLine("Person GPA = " + GPA);
            Addr.Display();
            //Console.WriteLine(Sch.Crn);
            Sch.Display();
        }
        public void DBSetup()
        {
            // +++++++++++++++++++++++++++  DBSetup function +++++++++++++++++++++++++++
            // This DBSetup() method instantiates all the DB objects needed to access a DB, 
            // including OleDbDataAdapter, which contains 4 other objects(OlsDbSelectCommand, 
            // oleDbInsertCommand, oleDbUpdateCommand, oleDbDeleteCommand.) And each
            // Command object contains a Connection object and an SQL string object.
            OleDbDataAdapter2 = new System.Data.OleDb.OleDbDataAdapter();
            OleDbSelectCommand2 = new System.Data.OleDb.OleDbCommand();
            OleDbInsertCommand2 = new System.Data.OleDb.OleDbCommand();
            OleDbUpdateCommand2 = new System.Data.OleDb.OleDbCommand();
            OleDbDeleteCommand2 = new System.Data.OleDb.OleDbCommand();
            OleDbConnection2 = new System.Data.OleDb.OleDbConnection();


            OleDbDataAdapter2.DeleteCommand = OleDbDeleteCommand2;
            OleDbDataAdapter2.InsertCommand = OleDbInsertCommand2;
            OleDbDataAdapter2.SelectCommand = OleDbSelectCommand2;
            OleDbDataAdapter2.UpdateCommand = OleDbUpdateCommand2;

            // The highlighted text below should be changed to the location of your own database

            OleDbConnection2.ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Reg" +
                                "istry Path =; Jet OLEDB:Database L" +
                                "ocking Mode=1;Data Source=E:\\Projects\\Fiverr\\shawnembry\\Labs\\Lab2\\FallLab2\\RegistrationMDB.mdb;J" +
                                "et OLEDB:Engine Type=5;Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:System datab" +
                                "ase=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=S" +
                                "hare Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet " +
                                "OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repai" +
                                "r=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
                                /*
                                "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Reg" +
                                "istry Path =; Jet OLEDB:Database L" +
                                "ocking Mode=1;Data Source=C:\\Users\\kerds\\OneDrive\\School\\C# II\\5\\RegistrationMDB.mdb;J" +
                                "et OLEDB:Engine Type=5;Provider=Microsoft.Jet.OLEDB.4.0;Jet OLEDB:System datab" +
                                "ase=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Mode=S" +
                                "hare Deny None;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet " +
                                "OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repai" +
                                "r=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";
                                */

        }  //end DBSetup()

        //OleDbConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=RegistrationMDB.mdb;";

        public void GetSchedule(string id)
        {
            DBSetup();
            cmd = "Select * from StudentSchedule where StudentID = " + id;
            OleDbDataAdapter2.SelectCommand.CommandText = cmd;
            OleDbDataAdapter2.SelectCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {

                //Schedule sched = new Schedule();
                sched.clearSections();

                OleDbConnection2.Open();
                System.Data.OleDb.OleDbDataReader dr;
                dr = OleDbDataAdapter2.SelectCommand.ExecuteReader();
                int crnVal;
                while (dr.Read())
                {
                    crnVal = (int)dr.GetValue(1);

                    //Next create a new section
                    Section sect = new Section();
                    sect.selectDB(crnVal.ToString());
                    sched.AddSection(sect);
                    
                }
                sched.StudentID = id;
                //dr.Read();
                //StudentID = id;
                //Sch.Crn = dr.GetValue(1) + "";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }
            //end GetSchedule
        }

        private bool doesCRNExist(int crnVal)
        {
            DBSetup();
            cmd = "Select * from StudentSchedule where StudentID = " + StudentID;
            OleDbDataAdapter2.SelectCommand.CommandText = cmd;
            OleDbDataAdapter2.SelectCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                //Schedule sched = new Schedule();
                sched.clearSections();

                OleDbConnection2.Open();
                System.Data.OleDb.OleDbDataReader dr;
                dr = OleDbDataAdapter2.SelectCommand.ExecuteReader();
                int val;
                while (dr.Read())
                {
                    val = (int)dr.GetValue(1);
                    if(crnVal == val)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }
            return false;
        }
        public void addSectionDB(string crnNum)
        {
            // If the CRN number exists for the student, no inserting necessary.
            if (doesCRNExist(int.Parse(crnNum))) {
                return;
            }
            // Insert code
            DBSetup();
            cmd = "INSERT into StudentSchedule values('" + StudentID + "'," + "'" + crnNum + "')";

            OleDbDataAdapter2.InsertCommand.CommandText = cmd;
            OleDbDataAdapter2.InsertCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                OleDbConnection2.Open();
                int n = OleDbDataAdapter2.InsertCommand.ExecuteNonQuery();
                if (n == 1)
                    Console.WriteLine("Data Inserted");
                else
                    Console.WriteLine("ERROR: Inserting Data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }


            //Next create a new section for the newly added CRN
            Section sect = new Section();
            sect.selectDB(crnNum);
            sched.AddSection(sect);
        }

        public void SelectDB(string id)
        { //++++++++++++++++++++++++++  SELECT +++++++++++++++++++++++++
            float num;
            DBSetup();
            cmd = "Select * from Students where ID = " + id;
            OleDbDataAdapter2.SelectCommand.CommandText = cmd;
            OleDbDataAdapter2.SelectCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                OleDbConnection2.Open();
                System.Data.OleDb.OleDbDataReader dr;
                dr = OleDbDataAdapter2.SelectCommand.ExecuteReader();

                dr.Read();
                StudentID = id;
                FirstName = dr.GetValue(1) + "";
                LastName = dr.GetValue(2) + "";
                Addr.Street = dr.GetValue(3) + "";
                Addr.City = dr.GetValue(4) + "";
                Addr.State = dr.GetValue(5) + "";
                Addr.Zip = (Int32.Parse(dr.GetValue(6) + ""));
                Email = dr.GetValue(7) + "";
                GPA = (float)dr.GetValue(8); // (Decimal.Parse((Decimal)dr.GetValue(8)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }
            GetSchedule(id);
            
        } //end SelectDB()


        
        public void InsertDB()
        {
            // +++++++++++++++++++++++++++  INSERT +++++++++++++++++++++++++++++++

            DBSetup();
            cmd = "INSERT into Students values('"
                 + GetStudentID()
                 + "',"
                 + "'"
                 + GetFirstName()
                 + "',"
                 + "'"
                 + GetLastName()
                 + "',"
                 + "'"
                 + Addr.GetStreet()
                 + "',"
                 + "'"
                 + Addr.GetCity()
                 + "',"
                 + "'"
                 + Addr.GetState()
                 + "',"
                 + "'"
                 + Addr.GetZip()
                 + "',"
                 + "'"
                 + GetEmail()
                 + "',"
                 + "'"
                 + GetGPA()
                 + "')";

            OleDbDataAdapter2.InsertCommand.CommandText = cmd;
            OleDbDataAdapter2.InsertCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                OleDbConnection2.Open();
                int n = OleDbDataAdapter2.InsertCommand.ExecuteNonQuery();
                if (n == 1)
                    Console.WriteLine("Data Inserted");
                else
                    Console.WriteLine("ERROR: Inserting Data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }
        } //end InsertDB()

        public void UpdateDB(string id)
        {
            //++++++++++++++++++++++++++  UPDATE  +++++++++++++++++++++++++


            cmd = "Update Students set FirstName = '" + GetFirstName() + "'," +
             " LastName = '" + GetLastName() + "', " +
             " Street = '" + Addr.GetStreet() + "', " +
             " City = '" + Addr.GetCity() + "', " +
             " State = '" + Addr.GetState() + "', " +
             " Zip = '" + Addr.GetZip() + "', " +
             " GPA = '" + GetGPA() + "', " +
             " Email = '" + GetEmail() + "' where ID = " + id;

            //" Office = '" + GetOffice() + "' where ID = " + id;

            Console.WriteLine("Student ID is = " + id);
            OleDbDataAdapter2.UpdateCommand.CommandText = cmd;
            OleDbDataAdapter2.UpdateCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                OleDbConnection2.Open();
                int n = OleDbDataAdapter2.UpdateCommand.ExecuteNonQuery();
                if (n == 1)
                    Console.WriteLine("Data Updated");
                else
                    Console.WriteLine("ERROR: Updating Data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                OleDbConnection2.Close();
            }
        } //end UpdateDB()

        public void DeleteDB(string id)
        {
            //++++++++++++++++++++++++++  DELETE  +++++++++++++++++++++++++

            cmd = "Delete from Students where ID = " + id;
            Console.WriteLine("Student ID is = " + id);
            OleDbDataAdapter2.DeleteCommand.CommandText = cmd;
            OleDbDataAdapter2.DeleteCommand.Connection = OleDbConnection2;
            Console.WriteLine(cmd);
            try
            {
                OleDbConnection2.Open();
                int n = OleDbDataAdapter2.DeleteCommand.ExecuteNonQuery();
                if (n == 1)
                    Console.WriteLine("Data Deleted");
                else
                    Console.WriteLine("ERROR: Deleting Data");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
{
    OleDbConnection2.Close();
}
        } //end DelectDB()
    }
}
