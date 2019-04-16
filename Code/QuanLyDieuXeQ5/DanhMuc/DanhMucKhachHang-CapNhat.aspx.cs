using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucKho_CapNhat : System.Web.UI.Page
{
    string sIdKhachHang = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdKhachHang = StaticData.ValidParameter(Request.QueryString["idKhachHang"].Trim());
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
            LoadMaKhachHang();
            LoadThongTinKho();
        }
    }

    private void LoadMaKhachHang()
    {
        string MaKhachHang = "";
        // string _Ngay = "";
        string sql = "select isnull(max(IDKhachHang),0)+1 as 'MaKhachHang' from tb_KhachHang";
        DataTable table = Connect.GetTable(sql);
        MaKhachHang = table.Rows[0]["MaKhachHang"].ToString();
        //_Ngay = DateTime.Now.ToString("yyyy-MM-dd");
        string[] ngay = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
        string MaDonHang = ngay[0] + ngay[1] + ngay[2];
        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaKhachHang.Value = MaDonHang + "00" + MaKhachHang + "";
    }

    private void LoadThongTinKho()
    {
        if (sIdKhachHang != "")
        {
            string sql = "select * from tb_KhachHang where idKhachHang ='" + sIdKhachHang + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN KHO";
                btLuu.Text = "SỬA";
                txtMaKhachHang.Value = table.Rows[0]["MaKhachHang"].ToString();
                txtTenKhachHang.Value = table.Rows[0]["TenKhachHang"].ToString();
                txtSoDienThoai.Value = table.Rows[0]["SoDienThoai"].ToString();
                txtEmail.Value = table.Rows[0]["Email"].ToString();
                txtDiaChi.Value = table.Rows[0]["DiaChi"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaKhachHang = "";
        string TenKhachHang = "";
        string SoDienThoai = "";
        string Email = "";
        string DiaChi = "";
        //Tên kho
        MaKhachHang = txtMaKhachHang.Value.Trim();
        if (txtTenKhachHang.Value.Trim() != "")
        {
            TenKhachHang = txtTenKhachHang.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên kho!')</script>");
            return;
        }
        //Vị trí kho
        SoDienThoai = txtSoDienThoai.Value.Trim();
        //Ghi chú
        Email = txtEmail.Value.Trim();
        DiaChi = txtDiaChi.Value.Trim();
        //////////
        if (sIdKhachHang == "")
        {
            string sqlInsertKho = "insert into tb_KhachHang(MaKhachHang,TenKhachHang,SoDienThoai,Email,DiaChi)";
            sqlInsertKho += " values(N'" + MaKhachHang + "',N'" + TenKhachHang + "',N'" + SoDienThoai + "',N'" + Email + "',N'" + DiaChi + "')";
            bool ktInsertKho = Connect.Exec(sqlInsertKho);
            if (ktInsertKho)
            {
                Response.Redirect("DanhMucKhachHang.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm kho!')</script>");
            }

        }
        else
        {

            string sqlUpdateKho = "";
            sqlUpdateKho += "update tb_KhachHang set";
            sqlUpdateKho += " MaKhachHang = N'" + MaKhachHang + "'";
            sqlUpdateKho += " ,TenKhachHang = N'" + TenKhachHang + "'";
            sqlUpdateKho += " ,SoDienThoai = N'" + SoDienThoai + "'";
            sqlUpdateKho += " ,Email = N'" + Email + "'";
            sqlUpdateKho += " ,DiaChi = N'" + DiaChi + "'";
            sqlUpdateKho += " where idKhachHang ='" + sIdKhachHang + "'";
            bool ktUpdateKho = Connect.Exec(sqlUpdateKho);
            if (ktUpdateKho)
            {
                if (Page != "")
                    Response.Redirect("DanhMucKhachHang.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucKhachHang.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucKhachHang.aspx");
    }
}