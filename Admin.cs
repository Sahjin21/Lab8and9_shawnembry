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
    public partial class Admin : Form
    {
        Form _parentForm;
        List<CIS2342.Student> _students = new List<CIS2342.Student>();
        List<CIS2342.Instructor> _instructors = new List<CIS2342.Instructor>();
        List<CIS2342.Course> _courses = new List<CIS2342.Course>();
        List<CIS2342.Section> _sections = new List<CIS2342.Section>();

        public Admin(Form parentForm)
        {
            _parentForm = parentForm;
            _parentForm.Hide();

            InitializeComponent();
            loadStudents();
            loadInstructors();
            loadCourses();
            loadSections();
        }

        private void loadStudents()
        {           
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";
            List<int> stdntIDs = new List<int>();
            int ID;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select ID from Students";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ID = (int)row.ItemArray[0];
                    stdntIDs.Add(ID);
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

            foreach (var valID in stdntIDs)
            {

                CIS2342.Student stdnt = new CIS2342.Student();
                stdnt.SelectDB(valID.ToString());
                _students.Add(stdnt);

                //Add to listview  lstvwStudents
                ListViewItem lvi = new ListViewItem();
                lvi.Text = stdnt.StudentID;
                lvi.SubItems.Add(stdnt.FirstName);
                lvi.SubItems.Add(stdnt.LastName);
                lstvwStudents.Items.Add(lvi);
            }
        }

        private void loadInstructors()
        {           
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";
            List<int> instructorIDs = new List<int>();
            int ID;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select ID from Instructors";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ID = (int)row.ItemArray[0];
                    instructorIDs.Add(ID);
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

            foreach (var valID in instructorIDs)
            {

                CIS2342.Instructor instruct = new CIS2342.Instructor();
                instruct.SelectDB(valID.ToString());
                _instructors.Add(instruct);

                //Add to listview  lstvwInstructors
                ListViewItem lvi = new ListViewItem();
                lvi.Text = instruct.InstructorID;
                lvi.SubItems.Add(instruct.FirstName);
                lvi.SubItems.Add(instruct.LastName);
                lstvwInstructors.Items.Add(lvi);
            }
        }

        private void loadCourses()
        {                    
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";
            List<string> courseIDs= new List<string>();
            string ID;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select CourseID from Courses";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ID = (string)row.ItemArray[0];
                    courseIDs.Add(ID);
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

            foreach (var valID in courseIDs)
            {
                CIS2342.Course course = new CIS2342.Course();
                course.SelectDB(valID);
                _courses.Add(course);

                //Add to listview  lstvwCourses
                ListViewItem lvi = new ListViewItem();
                lvi.Text = course.CourseID; 
                lvi.SubItems.Add(course.CourseName);                
                lstvwCourses.Items.Add(lvi);
            }
        }


        private void loadSections()
        {          
            string connectionString = null;
            string sql = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";
            List<int> crnVals = new List<int>();
            int CRN;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select CRN from Sections";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    CRN = (int)row.ItemArray[0];
                    crnVals.Add(CRN);                    
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

            foreach (var valCRN in crnVals)
            {
                CIS2342.Section section = new CIS2342.Section();
                section.selectDB(valCRN.ToString());

                _sections.Add(section);                

                //Add to listview  lstvwSections
                ListViewItem lvi = new ListViewItem();
                lvi.Text = section.Crn;
                lvi.SubItems.Add(section.CourseID);
                lstvwSections.Items.Add(lvi);
            }
        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Logoff?", "Admin", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                _parentForm.Show();
            }
        }

        private void lstvwStudents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;
            string ID;      

            item = lstvwStudents.Items.IndexOf(lstvwStudents.SelectedItems[0]);
            ID = (string)lstvwStudents.Items[item].Text;
            foreach (var student in _students)
            {
                if (student.StudentID == ID)
                {
                    txtbxStdntFirstName.Text = student.FirstName;
                    txtbxStdntLastName.Text = student.LastName;
                    txtbxStdntID.Text = student.StudentID;
                    txtbxStdntEmail.Text = student.Email;
                    txtbxStdntGPA.Text = student.GPA.ToString(".0#");

                    txtbxStdntStreet.Text = student.Addr.Street;
                    txtbxStdntCity.Text = student.Addr.City;
                    txtbxStdntState.Text = student.Addr.State;
                    txtbxStdntZip.Text = student.Addr.Zip.ToString();
                }
            }
        }

        // Get a valid student ID for a new student
        private int getNewStudentID()
        {
            string connectionString = null;          
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";           
            int ID;
            int maxID = 0;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select ID from Students";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ID = (int)row.ItemArray[0];
                    if (ID > maxID)
                    {
                        maxID = ID;
                    }
                }
                maxID++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                oledbCnn.Close();
            }
            return maxID;
        }

        //Validate that the textboxes have legitimate data
        private bool validateStudentData()
        {
            int intVal;
            float fVal;
            if (txtbxStdntFirstName.Text.Length == 0)
                return false;

            if (txtbxStdntLastName.Text.Length == 0)
                return false;


            if (txtbxStdntEmail.Text.Length == 0)
                return false;

            if (txtbxStdntGPA.Text.Length == 0)
                return false;

            if (txtbxStdntStreet.Text.Length == 0)
                return false;

            if (txtbxStdntCity.Text.Length == 0)
                return false;

            if (txtbxStdntState.Text.Length == 0)
                return false;

            if (txtbxStdntZip.Text.Length == 0)
                return false;

            //Check that numbers are valid
            if (!float.TryParse(txtbxStdntGPA.Text, out fVal))
            {
                return false;
            }


            //Check that numbers are valid
            if (!int.TryParse(txtbxStdntZip.Text, out intVal))
            {
                return false;
            }

            return true;
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want To Add A Student?", "Add Student", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
           
            if (validateStudentData() == false)
            {
                MessageBox.Show("Entered Data Is Not Valid. Note: ID will be automatically generated.", "Error");
                return;
            }

            int ID = getNewStudentID();

            // Next, insert the data
            CIS2342.Student student = new CIS2342.Student();

            student.FirstName = txtbxStdntFirstName.Text;
            student.LastName = txtbxStdntLastName.Text;
            student.StudentID = ID.ToString();
            student.Email = txtbxStdntEmail.Text;
            student.GPA = float.Parse(txtbxStdntGPA.Text); // = student.GPA.ToString(".0#");

            student.Addr.Street = txtbxStdntStreet.Text;
            student.Addr.City = txtbxStdntCity.Text;
            student.Addr.State = txtbxStdntState.Text;
            student.Addr.Zip = int.Parse(txtbxStdntZip.Text);

            student.InsertDB();
            _students.Add(student);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = student.StudentID;
            lvi.SubItems.Add(student.FirstName);
            lvi.SubItems.Add(student.LastName);
            lstvwStudents.Items.Add(lvi);
        }


        private void lstvwInstructors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;
            string ID;            

            item = lstvwInstructors.Items.IndexOf(lstvwInstructors.SelectedItems[0]);
            ID = (string)lstvwInstructors.Items[item].Text;
            foreach (var instructor in _instructors)
            {
                if (instructor.InstructorID == ID)
                {
                    txtbxInstrcFirstName.Text = instructor.FirstName;
                    txtbxInstrcLastName.Text = instructor.LastName;
                    txtbxInstrcID.Text = instructor.InstructorID;
                    txtbxInstrcEmail.Text = instructor.Email;
                    txtbxInstrcOffice.Text = instructor.Office;

                    txtbxInstrcStreet.Text = instructor.Addr.Street;
                    txtbxInstrcCity.Text = instructor.Addr.City;
                    txtbxInstrcState.Text = instructor.Addr.State;
                    txtbxInstrcZip.Text = instructor.Addr.Zip.ToString();
                }
            }
        }

        private void lstvwCourses_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;
            string ID;

            item = lstvwCourses.Items.IndexOf(lstvwCourses.SelectedItems[0]);
            ID = (string)lstvwCourses.Items[item].Text;
            foreach (var course in _courses)
            {
                if (course.CourseID == ID)
                {
                    txtbxCourseID.Text = course.CourseID;
                    txtbxCourseName.Text = course.CourseName;
                    txtbxCourseDescription.Text = course.Description;
                    txtbxCourseCreditHours.Text = course.CreditHours;                   
                }
            }
        }

        private void lstvwSections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int item;
            string CRN;

            item = lstvwSections.Items.IndexOf(lstvwSections.SelectedItems[0]);
            CRN = (string)lstvwSections.Items[item].Text;
            foreach (var section in _sections)
            {
                if (section.Crn == CRN)
                {
                    txtbxSectionCRN.Text = section.Crn;
                    txtbxSectionCourseID.Text = section.CourseID;
                    txtbxSectionTimeDays.Text = section.TimeDays;
                    txtbxSectionRoomNo.Text = section.RoomNo;
                    txtbxSectionInstructor.Text = section.InstructorID;                 
                }
            }
        }

        // Get a valid  instructor ID for a new instructor
        private int getNewInstructorID()
        {
            string connectionString = null;        
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";          
            int ID;
            int maxID = 0;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select ID from Instructors";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    ID = (int)row.ItemArray[0];
                    if (ID > maxID)
                    {
                        maxID = ID;
                    }
                }
                maxID++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                oledbCnn.Close();
            }
            return maxID;
        }

        //Validate that the textboxes have legitimate data
        private bool validateInstructorData()
        {
            int intVal;           
            
            if (txtbxInstrcFirstName.Text.Length == 0)
                return false;

            if (txtbxInstrcLastName.Text.Length == 0)
                return false;


            if (txtbxInstrcEmail.Text.Length == 0)
                return false;

            if (txtbxInstrcOffice.Text.Length == 0)
                return false;

            if (txtbxInstrcStreet.Text.Length == 0)
                return false;

            if (txtbxInstrcCity.Text.Length == 0)
                return false;

            if (txtbxInstrcState.Text.Length == 0)
                return false;

            if (txtbxInstrcZip.Text.Length == 0)
                return false;

            //Check that numbers are valid
            if (!int.TryParse(txtbxInstrcZip.Text, out intVal))
            {
                return false;
            }

            return true;
        }

        private void btnAddInstructor_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want To Add An Instructor?", "Add Instructor", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (validateInstructorData() == false)
            {
                MessageBox.Show("Entered Data Is Not Valid. Note: ID will be automatically generated.", "Error");
                return;
            }

            int ID = getNewInstructorID();

            // Next, insert the data
            CIS2342.Instructor instructor = new CIS2342.Instructor();

            instructor.FirstName = txtbxInstrcFirstName.Text;
            instructor.LastName = txtbxInstrcLastName.Text;
            instructor.InstructorID = ID.ToString();
            instructor.Email = txtbxInstrcEmail.Text;
            instructor.Office = txtbxInstrcOffice.Text; 

            instructor.Addr.Street = txtbxInstrcStreet.Text;
            instructor.Addr.City = txtbxInstrcCity.Text;
            instructor.Addr.State = txtbxInstrcState.Text;
            instructor.Addr.Zip = int.Parse(txtbxInstrcZip.Text);

            instructor.InsertDB();
            _instructors.Add(instructor);
            ListViewItem lvi = new ListViewItem();
            lvi.Text = instructor.InstructorID;
            lvi.SubItems.Add(instructor.FirstName);
            lvi.SubItems.Add(instructor.LastName);
            lstvwInstructors.Items.Add(lvi);
        }

        //Validate that the textboxes have legitimate data
        private bool validateCourseData()
        {
            int intVal;

            if (txtbxCourseID.Text.Length == 0)
                return false;

            if (txtbxCourseName.Text.Length == 0)
                return false;

            if (txtbxCourseDescription.Text.Length == 0)
                return false;
           
            if (txtbxCourseCreditHours.Text.Length == 0)
                return false;           

            //Check that numbers are valid
            if (!int.TryParse(txtbxCourseCreditHours.Text, out intVal))
            {
                return false;
            }

            return true;
        }

        // Make sure there is no conflict between the newCourseID
        // and those that exist in the Courses table of the database
        private bool isCourseIDValid(string newCourseID)
        {
            string connectionString = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";            
            string courseID;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select CourseID from Courses";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    courseID = (string)row.ItemArray[0];
                    // If equal, then there is already a course with this ID, return false
                    if (courseID == newCourseID)
                    {
                        return false;
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
           
            return true;
        }

        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do You Want To Add A Course?", "Add Course", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (validateCourseData() == false)
            {
                MessageBox.Show("Entered Data Is Not Valid. Note: You Must Enter a Unique Course ID", "Error");
                return;
            }

            if(isCourseIDValid(txtbxCourseID.Text) == false)
            {
                MessageBox.Show("Course ID Already Exists. Note: You Must Enter a Unique Course ID", "Error");
                return;
            }         

            // Next, insert the data
            CIS2342.Course course = new CIS2342.Course();

            course.CourseID = txtbxCourseID.Text;
            course.CourseName = txtbxCourseName.Text;
            course.Description = txtbxCourseDescription.Text;
            course.CreditHours = txtbxCourseCreditHours.Text;

            course.InsertDB();
            _courses.Add(course);
            
            ListViewItem lvi = new ListViewItem();
            lvi.Text = course.CourseID;
            lvi.SubItems.Add(course.CourseName);
            lstvwCourses.Items.Add(lvi);            
        }

        //Validate that the textboxes have legitimate data
        private bool validateSectionData()
        {
            if (txtbxSectionCourseID.Text.Length == 0)
                return false;

            if (txtbxSectionTimeDays.Text.Length == 0)
                return false;

            if (txtbxSectionRoomNo.Text.Length == 0)
                return false;

            if (txtbxSectionInstructor.Text.Length == 0)
                return false;
            
            return true;
        }

        // Get a unique section CRN for a new section
        private int getNewSectionCRN()
        {
            string connectionString = null;
            OleDbConnection oledbCnn;
            OleDbDataAdapter adapter;
            OleDbCommand command;
            string cmd = "";
            int CRN;
            int maxCRN = 0;

            connectionString = @"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = E:\Projects\Fiverr\shawnembry\Labs\Lab2\FallLab2\RegistrationMDB.mdb";

            oledbCnn = new OleDbConnection(connectionString);

            cmd = "Select CRN from Sections";
            command = new OleDbCommand(cmd, oledbCnn);
            adapter = new OleDbDataAdapter(command);
            DataTable td = new DataTable();
            try
            {
                adapter.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    CRN = (int)row.ItemArray[0];
                    if (CRN > maxCRN)
                    {
                        maxCRN = CRN;
                    }
                }
                maxCRN++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                oledbCnn.Close();
            }
            return maxCRN;
        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            int CRN;
            DialogResult dialogResult = MessageBox.Show("Do You Want To Add A Section?", "Add Section", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (validateSectionData() == false)
            {
                MessageBox.Show("Entered Data Is Not Valid. Note: CRN will be automatically generated.", "Error");
                return;
            }

            CRN = getNewSectionCRN();

            // Next, insert the data
            CIS2342.Section section = new CIS2342.Section();

            section.Crn  = CRN.ToString();
            section.CourseID = txtbxSectionCourseID.Text;
            section.TimeDays = txtbxSectionTimeDays.Text;
            section.RoomNo = txtbxSectionRoomNo.Text;
            section.InstructorID = txtbxSectionInstructor.Text;

            section.InsertDB();
            _sections.Add(section);            

            ListViewItem lvi = new ListViewItem();
            lvi.Text = section.Crn;
            lvi.SubItems.Add(section.CourseID);
            lstvwSections.Items.Add(lvi);
        }
    }
}
