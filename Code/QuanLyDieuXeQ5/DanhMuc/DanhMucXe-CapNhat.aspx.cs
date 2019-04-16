using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucTinh_CapNhat : System.Web.UI.Page
{
    string sIDXe = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIDXe = StaticData.ValidParameter(Request.QueryString["idXe"].Trim());
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
            LoadMaXe();
            LoadThongTinTinh();
        }
    }

    private void LoadMaXe()
    {
        string MaXe = "";
        string sql = "select isnull(max(IDXe),0)+1 as 'MaXe' from tb_Xe";
        DataTable table = Connect.GetTable(sql);
        MaXe = table.Rows[0]["MaXe"].ToString();

        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaXe.Value = "MaXe" + MaXe + "";
    }
    private void LoadThongTinTinh()
    {
        if (sIDXe != "")
        {
            string sql = "select * from tb_Xe where idXe ='" + sIDXe + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN TỈNH";
                btLuu.Text = "SỬA";
                txtMaXe.Value = table.Rows[0]["MaXe"].ToString();
                txtTenXe.Value = table.Rows[0]["TenXe"].ToString();
                txtBienSoXe.Value = table.Rows[0]["BienSoXe"].ToString();
                txtTenTaiXe.Value = StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[0]["idNguoiDung"].ToString());
                txtSDTTaiXe.Value = StaticData.getField("tb_NguoiDung", "SoDienThoai", "idNguoiDung", table.Rows[0]["idNguoiDung"].ToString());
                hdIdTaiXe.Value = table.Rows[0]["idNguoiDung"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaXe = "";
        string TenXe = "";
        string BienSoXe = "";
        string TenTaiXe = "";

        MaXe = txtMaXe.Value.Trim();
     //   TenXe = txtTenXe.Value.Trim();
        //Tên tỉnh
        if (txtBienSoXe.Value.Trim() != "")
        {
            BienSoXe = txtBienSoXe.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập biển số xe!')</script>");
            return;
        }
        TenTaiXe = hdIdTaiXe.Value.Trim();
        //////////
        if (sIDXe == "")
        {
            string sqlInsertTinh = "insert into tb_Xe(MaXe,TenXe,BienSoXe,IDNguoiDung)";
            sqlInsertTinh += " values(N'" + MaXe + "',N'" + TenXe + "',N'" + BienSoXe + "',N'" + TenTaiXe + "')";
            bool ktInsertTinh = Connect.Exec(sqlInsertTinh);
            if (ktInsertTinh)
            {
                Response.Redirect("DanhMucXe.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm xe!')</script>");
            }

        }
        else
        {

            string sqlUpdateTinh = "";
            sqlUpdateTinh += "update tb_Xe set";
            sqlUpdateTinh += " MaXe = N'" + MaXe + "'";
            sqlUpdateTinh += " ,TenXe = N'" + TenXe + "'";
            sqlUpdateTinh += " ,BienSoXe = N'" + BienSoXe + "'";
            sqlUpdateTinh += " ,IDNguoiDung = N'" + TenTaiXe + "'";
            sqlUpdateTinh += " where idXe ='" + sIDXe + "'";
            bool ktUpdateTinh = Connect.Exec(sqlUpdateTinh);
            if (ktUpdateTinh)
            {
                if (Page != "")
                    Response.Redirect("DanhMucXe.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucXe.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucXe.aspx");
    }
}