using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unicom_TIC_Management_System.Controllers;
using Unicom_TIC_Management_System.Models;

namespace Unicom_TIC_Management_System.View
{
    public partial class TimetableManagementControl : UserControl
    {
        private int userId;
        private string role;

        public TimetableManagementControl(int userId, string role)
        {
            InitializeComponent();

            this.userId = userId;
            this.role = role;

        }

        private void TimetableManagementControl_Load(object sender, EventArgs e)
        {
            //LoadSubjects();
            //LoadRooms();
            //LoadTimetables();




            if (role == "Student" || role == "Lecturer" || role == "Staff")
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnClear.Visible = false;

                
                lblSubject.Visible = false;
                cmbSubject.Visible = false;
                lblRoom.Visible = false;
                cmbRoom.Visible = false;
                lblTimeSlot.Visible = false;
                txtTimeSlot.Visible = false;
                lblDate.Visible = false;
                datePicker.Visible = false;

                LoadTimetables();
            }
            else 
            {
                LoadSubjects();
                LoadRooms();
                LoadTimetables();
            }

            


        }

        private void LoadSubjects()
        {
            cmbSubject.DataSource = SubjectController.GetAllSubjects(); // Already working
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private void LoadRooms()
        {
            cmbRoom.DataSource = RoomController.GetAllRooms(); // Already working
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.ValueMember = "RoomID";
        }

        private void LoadTimetables()
        {
            dgvTimetable.DataSource = null;
            dgvTimetable.DataSource = TimetableController.GetAllTimetables();
            dgvTimetable.Columns["Date"].HeaderText = "Date";
            dgvTimetable.Columns["Date"].DisplayIndex = 0; // show first

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbSubject.SelectedIndex == -1 || cmbRoom.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtTimeSlot.Text))
            {
                MessageBox.Show("Please fill all fields including the date.");
                return;
            }

            var timetable = new Timetable
            {
                SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                RoomID = Convert.ToInt32(cmbRoom.SelectedValue),
                TimeSlot = txtTimeSlot.Text.Trim(),
                Date = datePicker.Value.ToString("yyyy-MM-dd")
            };

            TimetableController.AddTimetable(timetable);
            MessageBox.Show("Timetable added!");
            LoadTimetables();
            ClearFields();
        }


        private void dgvTimetable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvTimetable.Rows[e.RowIndex];
                cmbSubject.Text = row.Cells["SubjectName"].Value.ToString();
                cmbRoom.Text = row.Cells["RoomName"].Value.ToString();
                txtTimeSlot.Text = row.Cells["TimeSlot"].Value.ToString();
                datePicker.Value = DateTime.Parse(row.Cells["Date"].Value.ToString());
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTimetable.SelectedRows.Count > 0)
            {
                var timetable = new Timetable
                {
                    TimetableID = Convert.ToInt32(dgvTimetable.SelectedRows[0].Cells["TimetableID"].Value),
                    SubjectID = Convert.ToInt32(cmbSubject.SelectedValue),
                    RoomID = Convert.ToInt32(cmbRoom.SelectedValue),
                    TimeSlot = txtTimeSlot.Text.Trim(),
                    Date = datePicker.Value.ToString("yyyy-MM-dd")
                };

                TimetableController.UpdateTimetable(timetable);
                MessageBox.Show("Timetable updated!");
                LoadTimetables();
                ClearFields();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTimetable.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTimetable.SelectedRows[0].Cells["TimetableID"].Value);
                TimetableController.DeleteTimetable(id);
                LoadTimetables();
                ClearFields();
            }
        }

        

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }


        private void ClearFields()
        {
            cmbSubject.SelectedIndex = -1;
            cmbRoom.SelectedIndex = -1;
            txtTimeSlot.Text = "";
        }
    }
}
