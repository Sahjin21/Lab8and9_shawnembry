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

/************************
 * Shawn Embry
 * CIS 2342 C# II
 * Lab 5
 * 10-06-2020
 * **********************/

namespace CIS2342
{
    public partial class Form1 : Form
    {
        int _studentID;
        Form _parentForm;
        Student stdnt;
        List<SectionInfo> _sections = new List<SectionInfo>();
        List<CourseInfo> _courses = new List<CourseInfo>();
        List<InstructorInfo> _instructors = new List<InstructorInfo>();
        List<int> _studentCRNs = new List<int>(); // The courses the on student's schedule
        
        class SectionInfo
        {
            public int CRN;
            public string CourseID;
            public string TimeDays;
            public string RoomNo;
            public int InstructorID;
            public string CourseName;
            public string InstructorsFirstName;
            public string InstructorsLastName;
        };

        struct CourseInfo
        {
            public string CourseID;
            public string CourseName;
            public string Description;
            public int CreditHours;
        };

        struct InstructorInfo
        {
            public int ID;
            public string FirstName;
            public string LastName;
            public string Office;
            public string Email;
        };
                

        public Form1(int studentID, Form parentForm)
        {
            _studentID = studentID;
            _parentForm = parentForm;
            _parentForm.Hide();
            stdnt = new Student();

            stdnt.SelectDB(studentID.ToString());
            
            InitializeComponent();
            loadStudentInformation();
            getSectionsFromDB();
            getCoursesDB();
            getInstructorsDB();
            getStudentScheduleDB();

            fillLstvwSections();
            fillLstvwSchedule();
        }

        
        private void loadStudentInformation()
        {
            PersonFNBox.Text = stdnt.FirstName;
            PersonLNBox.Text = stdnt.LastName;
            StudentIDTextBox.Text = stdnt.StudentID;
            StudentGPATextBox.Text = stdnt.GPA.ToString(".0#"); // The "0.#" for one decimal point accuracy display
            AddressStreetTextBox.Text = stdnt.Addr.GetStreet();
            AddressCityTextBox.Text = stdnt.Addr.GetCity();
            AddressStateTextBox.Text = stdnt.Addr.GetState();
            AddressZipTextBox.Text = stdnt.Addr.GetZip().ToString();
            PersonEmailTextBox.Text = stdnt.Email;
        }

        private void getSectionsFromDB()
        {
            //ID is numeric, now check if it is in the database
            SectionInfo secInfo;
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";
  
            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select * from Sections"; //
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    secInfo = new SectionInfo();
                    secInfo.CRN = (int)row.ItemArray[0];
                    secInfo.CourseID = (string)row.ItemArray[1];
                    secInfo.TimeDays = (string)row.ItemArray[2];
                    secInfo.RoomNo = (string)row.ItemArray[3];
                    secInfo.InstructorID = (int)row.ItemArray[4];
                    secInfo.CourseName = "";
                    secInfo.InstructorsFirstName = "";
                    secInfo.InstructorsLastName = "";
                    _sections.Add(secInfo);
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

        private void getCoursesDB()
        {
            //ID is numeric, now check if it is in the database
            CourseInfo courseInfo;
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select * from Courses"; //
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    courseInfo.CourseID = (string)row.ItemArray[0];
                    courseInfo.CourseName = (string)row.ItemArray[1];
                    courseInfo.Description = (string)row.ItemArray[2];
                    courseInfo.CreditHours = (int)row.ItemArray[3];
                    _courses.Add(courseInfo);                    
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

        private void getInstructorsDB()
        {
            //ID is numeric, now check if it is in the database
            InstructorInfo instructorInfo;
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select * from Instructors"; //
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    instructorInfo.ID = (int)row.ItemArray[0];
                    instructorInfo.FirstName = (string)row.ItemArray[1];
                    instructorInfo.LastName = (string)row.ItemArray[2];
                    instructorInfo.Office = (string)row.ItemArray[7];
                    instructorInfo.Email = (string)row.ItemArray[8];

                    _instructors.Add(instructorInfo);                    
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

        void getStudentScheduleDB()
        {
            //ID is numeric, now check if it is in the database
            int CRN;
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select * from StudentSchedule"; 
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    //Extrace the CRN numbers for this student
                    if(_studentID == (int)row.ItemArray[0])
                    {
                        CRN = (int)row.ItemArray[1];
                        _studentCRNs.Add(CRN);
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

        private void fillLstvwSections()
        {
            int i, j, k;           
       
            // Add data to the CustomerAcct form
            for (i = 0; i < _sections.Count; i++)
            {
                for(k=0; k < _courses.Count; k++)
                {
                    if(_sections[i].CourseID == _courses[k].CourseID)
                    {
                        _sections[i].CourseName = _courses[k].CourseName;
                    }
                }

                for(k=0; k < _instructors.Count; k++)
                {
                    if(_sections[i].InstructorID == _instructors[k].ID)
                    {
                        _sections[i].InstructorsFirstName = _instructors[k].FirstName;
                        _sections[i].InstructorsLastName = _instructors[k].LastName;
                    }
                }
                ListViewItem lvi = new ListViewItem();
                lvi.Text = _sections[i].CRN.ToString();
                lvi.SubItems.Add(_sections[i].CourseName);
                lvi.SubItems.Add(_sections[i].TimeDays);
                string instrName = _sections[i].InstructorsFirstName + " " + _sections[i].InstructorsLastName;
                lvi.SubItems.Add(instrName);
                lstVwSections.Items.Add(lvi);        
            }
        }

        private void fillLstvwSchedule()
        {

            //_studentCRNs
            foreach(var CRN in _studentCRNs)
            {
                //Get the course ID from the CRN
                foreach(var sec in _sections)
                {
                    if(sec.CRN == CRN)
                    {   
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = CRN.ToString();
                        lvi.SubItems.Add(sec.CourseName);
                        lstvwStudentsCourses.Items.Add(lvi);                     
                    }
                }
            }
        }

        private void PersonButton_Click(object sender, EventArgs e)
        {
            Person person1 = new Person("Derek", "Jones", "djones@yahoo.com", new Address("123 Misty", "Woodstock", "GA", 30188), new Schedule());
            person1.Display();
            Schedule s1 = new Schedule();
            s1.AddSection(new Section("456", "654", "T-F", "789", "987"));
            s1.Display();
        }

        private void StudentButton_Click(object sender, EventArgs e)
        {
            //DISPLAY STUDENT TEST
            Student student1 = new Student();
            student1.StudentID = "3";
            student1.SelectDB("3");

            //Insert value into database
            student1.addSectionDB("30114");

            //Schedule s1 = new Schedule();
            //s1.AddSection(new Section("456", "654", "T-F", "789", "987"));
            //s1.Display();
            student1.StudentDisplay();


            //INSERT STUDENT TEST
            //Address addr1 = new Address("123 Misty", "Woodstock", "GA", 30144);
            //Student student1 = new Student("17", "Jack", "Davis", "JackDavis@mail.com", 3.1, addr1, new Schedule());
            //student1.StudentDisplay();
            //Schedule s1 = new Schedule();
            //s1.AddSection(new Section(456, 654, "T-F", 789, 987));

            //UPDATE STUDENT TEST
            //Student student1 = new Student();
            //student1.SelectDB("3");
            //student1.Display();
            //student1.Email = "jeemale@gmail.com";
            //student1.UpdateDB("3");

            //DELETE STUDENT TEST
            //Instructor instructor1 = new Instructor();
            //instructor1.SelectDB("1");
            //instructor1.DeleteDB("1");   

        }

        private void InstructorButton_Click(object sender, EventArgs e)
        {

            //INSTRUCTOR SELECT DB TEST
            //Instructor instructor1 = new Instructor();
            //instructor1.SelectDB("3");
            //instructor1.InstructorDisplay();

            //INSERT INSTRUCTOR TEST
            //Instructor instructor1 = new Instructor();
            //Address addr1 = new Address("442 Lone Rd", "Kennesaw", "GA", 30144);
            //instructor1 = new Instructor("8", "Eric", "Smith", "JackJones@mail.com", "F1140", addr1, new Schedule());
            //instructor1.InsertDB();
            //instructor1.InstructorDisplay();

            //UPDATE INSTRUCTOR TEST
            Instructor instructor1 = new Instructor();
            instructor1.SelectDB("3");
            instructor1.InstructorDisplay();
            instructor1.Email = "jeemale@gmail.com";
            instructor1.UpdateDB("3");

            //DELETE INSTRUCTOR TEST
            //Instructor instructor1 = new Instructor();
            //instructor1.SelectDB("1");
            //instructor1.DeleteDB("1");           
        }
        private void AddressButton_Click(object sender, EventArgs e)
        {
            Address address1 = new Address();
            address1.Display();
        }

        private void CourseButton_Click(object sender, EventArgs e)
        {
            //SELECT COURSE TEST
            //Course course1 = new Course();
            //course1.SelectDB("CIST 6666");
            //course1.Display();

            //INSERT COURSE TEST
            //Course course1 = new Course();
            //course1 = new Course("CIST 6666", "InsertTest2", "Third Class", "1");
            //course1.InsertDB();
            //course1.Display();

            //UPDATE COURSE TEST
            Course course1 = new Course();
            course1.SelectDB("CIST 8888");
            course1.CreditHours = "2";
            course1.Display();
            course1.UpdateDB("CIST 8888");

            //DELETE COURSE TEST
            //Course course1 = new Course();
            //course1.SelectDB("6666");
            //course1.DeleteDB("6666");    
        }

        private void SectionButton_Click(object sender, EventArgs e)
        {
            //SELECT SECTION TEST
            //Section section1 = new Section();
            //section1.SelectDB("30101");
            //section1.Display();

            //INSERT SECTION TEST
            //Section section1 = new Section();
            //section1 = new Section("31108", "CIST 8888", "MW3-4pm", "FF148", "2");
            //section1.InsertDB();
            //section1.Display();

            //UPDATE SECTION TEST
            Section section1 = new Section();
            section1.selectDB("31108");
            section1.RoomNo = "F1148";
            section1.Display();
            section1.UpdateDB("31108");
        }

        private void ScheduleButton_Click(object sender, EventArgs e)
        {
            Schedule sectionSchedule = new Schedule();
            sectionSchedule.AddSection(new Section("123", "321", "M-W", "123", "21"));
            sectionSchedule.AddSection(new Section("456", "654", "T-F", "789", "987"));
            sectionSchedule.Display();
        }

        private void InstructorOfficeLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Logoff?", "Student Information", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                _parentForm.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lstVwSections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;           
            int CRN;
            string courseDetails = "";
            bool exitLoop = false;

            item = lstVwSections.Items.IndexOf(lstVwSections.SelectedItems[0]);
            CRN = int.Parse((string)lstVwSections.Items[item].Text);
            foreach (var section in _sections)
            {
                if (section.CRN == CRN)
                {
                    foreach(var course in _courses)
                    {
                        if(section.CourseID == course.CourseID)
                        {
                            //Fill in the textbox with the course details                        
                            courseDetails = "Course Name: " + course.CourseName + "\r\n";
                            courseDetails += "\r\n";                            
                            courseDetails += "Description: " + course.Description + "\r\n";
                            courseDetails += "\r\n";
                            courseDetails += "Course ID: " + course.CourseID + "\r\n";                           
                            courseDetails += "Credit Hours: " + course.CreditHours.ToString() + "\r\n";                         
                            courseDetails += "Time & Days: " + section.TimeDays + "\r\n";                          
                            courseDetails += "Room No: " + section.RoomNo + "\r\n";                        
                            courseDetails += "Instructor: " + section.InstructorsFirstName + " " + section.InstructorsLastName;
                            txtBxCourseDetails.Text = courseDetails;
                            break;
                        }
                    }
                }
                if (exitLoop)
                {
                    break;
                }
            }
        }

        private void txtBxCourseDetails_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstvwStudentsCourses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;
            int CRN;
            string courseDetails = "";
            bool exitLoop = false;

            item = lstvwStudentsCourses.Items.IndexOf(lstvwStudentsCourses.SelectedItems[0]);
            CRN = int.Parse((string)lstvwStudentsCourses.Items[item].Text);

            foreach (var section in _sections)
            {
                if (section.CRN == CRN)
                {
                    foreach (var course in _courses)
                    {
                        if (section.CourseID == course.CourseID)
                        {
                            //Fill in the textbox with the course details                        
                            courseDetails = "Course Name: " + course.CourseName + "\r\n";
                            courseDetails += "\r\n";
                            courseDetails += "Description: " + course.Description + "\r\n";
                            courseDetails += "\r\n";
                            courseDetails += "Course ID: " + course.CourseID + "\r\n";                            
                            courseDetails += "Credit Hours: " + course.CreditHours.ToString() + "\r\n";                            
                            courseDetails += "Time & Days: " + section.TimeDays + "\r\n";                            
                            courseDetails += "Room No: " + section.RoomNo + "\r\n";                            
                            courseDetails += "Instructor: " + section.InstructorsFirstName + " " + section.InstructorsLastName;
                            txtBxCourseDetails.Text = courseDetails;
                            break;
                        }
                    }
                }
                if (exitLoop)
                {
                    break;
                }
            }
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            int item;
            int CRN;
            string courseDetails = "";
            bool exitLoop = false;
            // Add course selected in lstvwSections
            

            if (lstVwSections.SelectedItems.Count > 0)
            {
                item = lstVwSections.Items.IndexOf(lstVwSections.SelectedItems[0]);
                CRN = int.Parse((string)lstVwSections.Items[item].Text);
                //Does the student already have this course?
                foreach( var valCRN in _studentCRNs)
                {
                    if(valCRN == CRN)
                    {
                        MessageBox.Show("You Already Have This Course.", "Error");
                        return;
                    }
                }

                courseDetails = "Do You Want To Add the Course \"" + getCourseNameFromCRN(CRN) + "\" To Your Schedule?";
                DialogResult dialogResult = MessageBox.Show(courseDetails, "Add Course", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Add the course to the student's schedule
                    //First I need an account number to use   
                    string connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

                    //string ConnString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Projects\Fiverr\shawnembry\ProjectZZ\ChattBankMDB.mdb";
                    //string SqlString = "Insert Into Customers (CustID, CustPassword, CustFirstName, CustLastName, CustAddress, CustEmail) Values (?,?,?,?,?,?)";
                    //string SqlString = "Insert Into Accounts (AcctNo, Cid, Type, Balance) Values (?,?,?,?)";
                    string SqlString = "Insert Into StudentSchedule (StudentID, CRN) Values (?,?)";
                    OleDbConnection conn = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand(SqlString, conn);                        
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("StudentID", _studentID);
                        cmd.Parameters.AddWithValue("CRN", CRN);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        //Add item to students list of CRN numbers
                        _studentCRNs.Add(CRN);
                        // Add item to listview control
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = CRN.ToString();
                        lvi.SubItems.Add(getCourseNameFromCRN(CRN));
                        lstvwStudentsCourses.Items.Add(lvi);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    finally
                    {
                        conn.Close();
                    }                   
                }                
            }
            else
            {
                MessageBox.Show("No Course Selected.   Doubleclick a CRN number to select a course.","Error" );
            }
        }

        private string getCourseNameFromCRN(int CRN)
        {
            foreach (var section in _sections)
            {
                if (section.CRN == CRN)
                {
                    return section.CourseName;
                }
            }
            return "";
        }
    }
}
