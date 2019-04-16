using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucKhachHang_CapNhat : System.Web.UI.Page
{
    string sIdChiNhanh = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdChiNhanh = StaticData.ValidParameter(Request.QueryString["idChiNhanh"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
            if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            {
                mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
                mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                if (mQuyen.ToUpper() != "ADMIN")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }
            LoadMaChiNhanh();
            LoadThongTinKhachHang();
        }
    }
    private void LoadThongTinKhachHang()
    {
        if (sIdChiNhanh != "")
        {
            string sql = "select * from tb_ChiNhanh where idChiNhanh='" + sIdChiNhanh + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN KHÁCH HÀNG";
                btLuu.Text = "SỬA";
              txtTenChiNhanh.Value = table.Rows[0]["TenChiNhanh"].ToString();
                txtMaChiNhanh.Value = table.Rows[0]["MaChiNhanh"].ToString();
                txtSoDienThoai.Value = table.Rows[0]["SoDienThoai"].ToString();
                txtDiaChi.Value = table.Rows[0]["DiaChi"].ToString();
              //  txtEmail.Value = table.Rows[0]["Email"].ToString();
              
             
               // if (table.Rows[0]["GiaCuocNoiThanh"].ToString() != "")
               ////     txtGiaCuocNoiThanh.Value = double.Parse(table.Rows[0]["GiaCuocNoiThanh"].ToString()).ToString("#,##").Replace(",", ".");
               // if (table.Rows[0]["GiaCuocNgoaiThanh"].ToString() != "")
               //     txtGiaCuocNgoaiThanh.Value = double.Parse(table.Rows[0]["GiaCuocNgoaiThanh"].ToString()).ToString("#,##").Replace(",", ".");
               // if (table.Rows[0]["GiaCuocDiTinh"].ToString() != "")
               //     txtGiaCuocDiTinh.Value = double.Parse(table.Rows[0]["GiaCuocDiTinh"].ToString()).ToString("#,##").Replace(",", ".");
               // if (table.Rows[0]["GiaCuocHuyen"].ToString() != "")
               //     txtGiaCuocHuyen.Value = double.Parse(table.Rows[0]["GiaCuocHuyen"].ToString()).ToString("#,##").Replace(",", ".");
               // txtTenDangNhap.Value = table.Rows[0]["TenDangNhap"].ToString();
               // //txtTenDangNhap.Disabled = true;
               // txtMatKhau.Value = table.Rows[0]["MatKhau"].ToString();
            }
        }
    }
    private void LoadMaChiNhanh()
    {
        string MaChiNhanh = "";
        string sql = "select isnull(max(IDChiNhanh),0)+1 as 'MaChiNhanh' from tb_ChiNhanh";
        DataTable table = Connect.GetTable(sql);
        MaChiNhanh = table.Rows[0]["MaChiNhanh"].ToString();

        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaChiNhanh.Value = "MaChiNhanh" + MaChiNhanh + "";
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string TenChiNhanh = "";
        string MaChiNhanh = "";
        string SoDienThoai = "";
        string DiaChi = "";


        string LoaiKhach = "";
        string Email = "";
        string NguoiLienHe = "";
        string KhachSi = ckbKhachSi.Checked.ToString();
        string KhachLe = ckbKhachLe.Checked.ToString();
        //string GiaCuocNoiThanh = "";
        //string GiaCuocNgoaiThanh = "";
        //string GiaCuocDiTinh = "";
        //string GiaCuocHuyen = "";
        //string TenDangNhap = "";
        //string MatKhau = "";
     //   string isImportExcel = ckbImportExcel.Checked.ToString();
        //Tên khách hàng
        MaChiNhanh = txtMaChiNhanh.Value.Trim();
        if (txtTenChiNhanh.Value.Trim() != "")
        {
            TenChiNhanh = txtTenChiNhanh.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên khách hàng!')</script>");
            return;
        }
        //Số điện thoại
        SoDienThoai = txtSoDienThoai.Value.Trim();
        //Email
       // Email = txtEmail.Value.Trim();
        //Địa chỉ
        DiaChi = txtDiaChi.Value.Trim();
      
        if (sIdChiNhanh == "")
        {
            string sqlInsertKhachHang = "insert into tb_ChiNhanh(MaChiNhanh,TenChiNhanh,SoDienThoai,DiaChi)";
            sqlInsertKhachHang += " values(N'" + MaChiNhanh + "',N'" + TenChiNhanh + "','" + SoDienThoai + "',N'" + DiaChi + "'";
           
            sqlInsertKhachHang += ")";
            bool ktInsertNguoiDung = Connect.Exec(sqlInsertKhachHang);
            if (ktInsertNguoiDung)
            {
                Response.Redirect("DanhMucChiNhanh.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm khách hàng!')</script>");
            }

        }
        else
        {

            string sqlUpdateKhachHang = "";
            sqlUpdateKhachHang += "update tb_ChiNhanh set";
            sqlUpdateKhachHang += " MaChiNhanh = N'" + MaChiNhanh + "'";
            sqlUpdateKhachHang += " ,TenChiNhanh = N'" + TenChiNhanh + "'";
            sqlUpdateKhachHang += " ,SoDienThoai = '" + SoDienThoai + "'";
        
            sqlUpdateKhachHang += " ,DiaChi = N'" + DiaChi + "'";
     
          //  sqlUpdateKhachHang += " ,TenDangNhap = '" + TenDangNhap + "'";
            sqlUpdateKhachHang += " where idChiNhanh ='" + sIdChiNhanh + "'";
            bool ktUpdateNguoiDung = Connect.Exec(sqlUpdateKhachHang);
            if (ktUpdateNguoiDung)
            {
                if (Page != "")
                    Response.Redirect("DanhMucChiNhanh.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucChiNhanh.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucChiNhanh.aspx");
    }
}