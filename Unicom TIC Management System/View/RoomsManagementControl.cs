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
    public partial class RoomsManagementControl : UserControl
    {
        public RoomsManagementControl()
        {
            InitializeComponent();

            LoadRoomTypes();
            LoadRooms();
        }

        private int selectedRoomId = -1;

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete this room?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                RoomController.DeleteRoom(selectedRoomId);
                LoadRooms();
                MessageBox.Show("Room deleted.");
            }
        }

        private void RoomsManagementControl_Load(object sender, EventArgs e)
        {

        }

        private void LoadRoomTypes()
        {
            cmbRoomType.Items.Clear();
            cmbRoomType.Items.Add("Lab");
            cmbRoomType.Items.Add("Hall");
            cmbRoomType.SelectedIndex = 0;

            ClearFields();
        }

        private void LoadRooms()
        {
            dataGridViewRooms.DataSource = null;
            dataGridViewRooms.DataSource = RoomController.GetAllRooms();
            dataGridViewRooms.ClearSelection();
            ClearFields();
        }

        private void ClearFields()
        {
            txtRoomName.Text = "";
            cmbRoomType.SelectedIndex = 0;
            selectedRoomId = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomName.Text))
            {
                MessageBox.Show("Room name is required.");
                return;
            }

            Room room = new Room
            {
                RoomName = txtRoomName.Text.Trim(),
                RoomType = cmbRoomType.SelectedItem.ToString()
            };

            RoomController.AddRoom(room);
            LoadRooms();
            MessageBox.Show("Room added successfully.");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedRoomId == -1)
            {
                MessageBox.Show("Please select a room to edit.");
                return;
            }

            Room room = new Room
            {
                RoomID = selectedRoomId,
                RoomName = txtRoomName.Text.Trim(),
                RoomType = cmbRoomType.SelectedItem.ToString()
            };

            RoomController.UpdateRoom(room);
            LoadRooms();
            MessageBox.Show("Room updated successfully.");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridViewRooms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewRooms.Rows[e.RowIndex];
                selectedRoomId = Convert.ToInt32(row.Cells["RoomID"].Value);
                txtRoomName.Text = row.Cells["RoomName"].Value.ToString();
                cmbRoomType.SelectedItem = row.Cells["RoomType"].Value.ToString();
            }
        }

        private void dataGridViewRooms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtRoomName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
