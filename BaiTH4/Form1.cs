using BaiTH4.data;
using BaiTH4.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.XlsIO;

namespace BaiTH4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Connection db = new Connection();
       
       


        private void StateGroupBoxChiTiet(bool type)//Bat groupbox Chi Tiet
        {
            gbChiTiet.Enabled = type;
        }
        private void StateButtonThemSuaXoa(bool type)//Tat nut them sua xoa
        {
            btnThem.Enabled = type;
            btnSua.Enabled = type;
            btnXoa.Enabled = type;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            StateGroupBoxChiTiet(true);//Bat group Chi Tiet
            StateButtonThemSuaXoa(false);//Tat 3 nut Them Sua Xoa
            btnThem.Enabled = true;//Bat nut them len de biet hanh dong
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            StateGroupBoxChiTiet(true);//Bat group Chi Tiet
            StateButtonThemSuaXoa(false);//Tat 3 nut Them Sua Xoa
            btnSua.Enabled = true;
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            StateGroupBoxChiTiet(true);//Bat group Chi Tiet
            StateButtonThemSuaXoa(false);//Tat 3 nut Them Sua Xoa
            btnXoa.Enabled = true;

        }



        //check thong tin
        private bool isCheck()
        {
            if(txtMasp.Text.Trim() == "")
            {
                MessageBox.Show("Mã sản phẩm không được để trống");
                return false;
            }
            if(txtTensp.Text.Trim() == "")
            {
                MessageBox.Show("Tên sản phẩm không được để trống");
                return false;
            }
            if (dtpNgaysx.Value >DateTime.Now)
            {
                MessageBox.Show("Ngày sản xuất không được lớn hơn ngày hiện tại");
                return false;
            }
            TimeSpan diff = dtpNgayhh.Value.Subtract(dtpNgaysx.Value);// để láy thời gian cách nhau bao lâu giữa 2 khoảng
            if(diff.TotalSeconds < 30)
            {
                MessageBox.Show("Ngày hết hạn không được nhỏ hơn ngày sản xuất ");
            }
            if(txtDonvi.Text.Trim() == "")
            {
                MessageBox.Show("Đơn vị không được để trống");
            }
            if(txtDonGia.Text.Trim() == "")
            {
                MessageBox.Show("Đơn giá không được để trống");

            }
            return true;
        }
        
        private SanPham sanPham()
        {
            string maSP = txtMasp.Text.Trim();
            string tenSP = txtTensp.Text.Trim();
            string ngaySX = dtpNgaysx.Value.ToString("yyyy-MM-dd");
            string ngayHH = dtpNgayhh.Value.ToString("yyyy-MM-dd");
            string donVi = txtDonvi.Text.Trim();
            string donGia = txtDonGia.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            return new SanPham(maSP, tenSP, ngaySX, ngayHH, donVi, donGia, ghiChu);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled && isCheck())
            {
                SanPham sp = sanPham();
                string query = $"Insert into SanPham values('{sp.MaSP}',N'{sp.TenSP}','{sp.NgaySX}','{sp.NgayHH}','{sp.DonVi}',{sp.DonGia},N'{sp.GhiChu}')";


                if(MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information ) == DialogResult.Yes){
                    try
                    {
                        db.Excute(query);
                        MessageBox.Show("Them san pham thanh cong!");
                        Form1_Load(sender, e);
                    }
                     catch(Exception ex)
                    {
                        MessageBox.Show("Loi: " + ex.Message);
                    }

                }

                



            }
            if (btnSua.Enabled && isCheck())
            {
                MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                SanPham sp = sanPham();
                string query = $"Update SanPham set TenSP = N'{sp.TenSP}',NgaySX = '{sp.NgaySX}',NgayHH = '{sp.NgayHH}',DonVi = '{sp.DonVi}',DonGia = '{sp.DonGia}',GhiChu = N'{sp.GhiChu}' where MaSP = '{txtMasp.Text.Trim()}'";
                try
                {
                    db.Excute(query);
                    MessageBox.Show("Update thành công!", "Thông báo");
                    Form1_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }


            }
            if (btnXoa.Enabled )
            {
                SanPham sp = sanPham();
                string query = "DELETE FROM SanPham WHERE TenSP = " + sp.TenSP.ToString();
                if (MessageBox.Show("Bạn có muốn xóa  không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        db.Excute(query);
                        MessageBox.Show("Xóa sản phẩm thành công");
                        Form1_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            StateGroupBoxChiTiet(false);// trở về trạng thái ban đầu
            StateButtonThemSuaXoa(true);
           
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(char.IsControl(e.KeyChar) == false && !char.IsDigit(e.KeyChar)) //neu nhap chu cai(khong cho bam chu cai chi dc bam so)
            {
                e.Handled = true;//khong thuc hien su kien nua
            }
            //neu khong thao tac ban phim vao cac ky tu chu va thao tac vao cac ky
            //tu chu la sai( dung la phai thao tac vao cac ky tu so) thi no se khong duoc nhap
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvKetQua.DataSource = db.table("Select * from SanPham");
        }

        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMasp.Text = dgvKetQua.SelectedRows[0].Cells[0].Value.ToString();
            txtTensp.Text = dgvKetQua.SelectedRows[0].Cells[1].Value.ToString();
            dtpNgaysx.Value = DateTime.Parse(dgvKetQua.SelectedRows[0].Cells[2].Value.ToString());
            dtpNgayhh.Value = DateTime.Parse(dgvKetQua.SelectedRows[0].Cells[3].Value.ToString());
            txtDonvi.Text = dgvKetQua.SelectedRows[0].Cells[4].Value.ToString();
            txtDonGia.Text = dgvKetQua.SelectedRows[0].Cells[5].Value.ToString();
            txtGhiChu.Text = dgvKetQua.SelectedRows[0].Cells[6].Value.ToString();


        }
        private void ExportExcel(DataTable dataTable,string path)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                worksheet.ImportDataTable(dataTable, true, 1, 1);
                worksheet.UsedRange.AutofitColumns();
                workbook.SaveAs(path);
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Excel";
            saveFileDialog.Filter = "Excel(*.xlsx)|*.xlsx|Excel 2003(*.xls)|*.xls";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable dt = db.table("Select * from SanPham");
                    ExportExcel(dt, saveFileDialog.FileName);
                    MessageBox.Show("Xuất file thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtTensptim.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm muốn tìm!", "Thông báo");
                Form1_Load(sender, e);
            }
            else
            {
                string query = $"Select * from SanPham where TenSP = N'{txtTensptim.Text.Trim()}'";
                try
                {
                    dgvKetQua.DataSource = db.table(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void txtMasp_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
