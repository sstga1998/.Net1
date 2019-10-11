using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Controller;
using WindowsFormsApp1.Modal;

namespace WindowsFormsApp1.View
{
   
    public partial class Form1 : Form
    {
        string pathStudentDataFile;
        string pathDirectoryImg;
        string pathAvatarImg;
        Image image;

        public Form1(string maSinhVien)
        {
            InitializeComponent();
            pathDirectoryImg = Application.StartupPath + "\\Img";
            pathAvatarImg = pathDirectoryImg + "\\avata.png";
            picAnhDaiDien.AllowDrop = true;
            pathStudentDataFile = Application.StartupPath + @"\Data\student.txt";
            if (File.Exists(pathAvatarImg))
            {
                FileStream fileStream = new FileStream(pathAvatarImg, FileMode.Open, FileAccess.Read);

                picAnhDaiDien.Image = Image.FromStream(fileStream);
                fileStream.Close();
            }

            bdsQuaTrinhHocTap.DataSource = null;
            dtgQuaTrinhHocTap.AutoGenerateColumns = false;


            //var student = StudentService.GetStudent(maSinhVien);
            var student = StudentService.GetStudent(pathStudentDataFile, maSinhVien);
            if (student == null)
                throw new Exception("Không tồn tại sinh viên này");
            else
            {
                txtMaSinhVien.Text = student.IDStudent;
                txtHo.Text = student.LastName;
                txtTen.Text = student.FirstName;
                txtQueQuan.Text = student.POB;
                dtpNgaySinh.Value = student.DOB;
                txt.Checked = student.Gender == GENDER.Female;
                if (student.ListHistoryLearning != null)
                {
                    bdsQuaTrinhHocTap.DataSource = student.ListHistoryLearning;
                }
                dtgQuaTrinhHocTap.DataSource = bdsQuaTrinhHocTap;
            }
        }

        private void LnkChonAnhDaiDien_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn ảnh đại diện";
            openFileDialog.Filter = "File ảnh(*.png,*.jpg)|*.png;*.jpg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                //picAnhDaiDien.Image = Image.FromFile(openFileDialog.FileName);
                image = Image.FromStream(fileStream);
                picAnhDaiDien.Image = image;
                fileStream.Close();
            }
        }

        private void TntCapNhap_Click(object sender, EventArgs e)
        {
            #region Cập nhật hình đại diện
            bool imageSave = false;
            if (image != null)
            {
               // string pathDirectory = Application.StartupPath + "Img";
                if (!Directory.Exists(pathDirectoryImg))
                {
                    Directory.CreateDirectory(pathDirectoryImg);
                }
                image.Save(pathAvatarImg);
                imageSave = true;
            }
            #endregion 
            if (imageSave)
            {
                MessageBox.Show("Đã cập nhật thông tin thành công", "Thông báo"
                    ,MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PicAnhDaiDien_DragDrop(object sender, DragEventArgs e)
        {
            var rs= (string[]) e.Data.GetData(DataFormats.FileDrop);
            var filePath = rs.FirstOrDefault();
            image = Image.FromFile(filePath);
            picAnhDaiDien.Image = image;
        }

        private void PicAnhDaiDien_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void PicAnhDaiDien_Click(object sender, EventArgs e)
        {

        }

        private void XoaAnhDaiDien_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Xóa ảnh đại diện");
            picAnhDaiDien.Image = Properties.Resources._10_avatar_128;
            File.Delete(pathAvatarImg);
        }
    }
}
