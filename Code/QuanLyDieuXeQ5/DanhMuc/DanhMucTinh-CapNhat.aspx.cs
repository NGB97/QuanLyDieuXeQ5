using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucTinh_CapNhat : System.Web.UI.Page
{
    string sIdTinh = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdTinh = StaticData.ValidParameter(Request.QueryString["idTinh"].Trim());
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
            LoadThongTinTinh();
        }
    }
    private void LoadThongTinTinh()
    {
        if (sIdTinh != "")
        {
            string sql = "select * from tb_Tinh where idTinh='" + sIdTinh + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN TỈNH";
                btLuu.Text = "SỬA";
                txtTenTinh.Value = table.Rows[0]["TenTinh"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string TenTinh = "";
        //Tên tỉnh
        if (txtTenTinh.Value.Trim() != "")
        {
            TenTinh = txtTenTinh.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên tỉnh!')</script>");
            return;
        }
        //////////
        if (sIdTinh == "")
        {
            string sqlInsertTinh = "insert into tb_Tinh(TenTinh)";
            sqlInsertTinh += " values(N'" + TenTinh + "')";
            bool ktInsertTinh = Connect.Exec(sqlInsertTinh);
            if (ktInsertTinh)
            {
                Response.Redirect("DanhMucTinh.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm tỉnh!')</script>");
            }

        }
        else
        {

            string sqlUpdateTinh = "";
            sqlUpdateTinh += "update tb_Tinh set";
            sqlUpdateTinh += " TenTinh = N'" + TenTinh + "'";
            sqlUpdateTinh += " where idTinh ='" + sIdTinh + "'";
            bool ktUpdateTinh = Connect.Exec(sqlUpdateTinh);
            if (ktUpdateTinh)
            {
                if (Page != "")
                    Response.Redirect("DanhMucTinh.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucTinh.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucTinh.aspx");
    }
}