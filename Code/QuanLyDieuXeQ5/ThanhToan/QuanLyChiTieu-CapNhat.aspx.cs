using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyChiTieu_QuanLyChiTieu_CapNhat : System.Web.UI.Page
{
    string sIdChiTieu = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdChiTieu = StaticData.ValidParameter(Request.QueryString["idChiTieu"].Trim());
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
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "QTWS")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                else
                {
                    if(mQuyen.ToUpper() == "NVVP" && sIdChiTieu != "")
                    {
                        Response.Redirect("../Home/DangNhap.aspx");
                    }
                }
            }
            LoadThongTinChiTieu();
        }
    }
    private void LoadThongTinChiTieu()
    {
        if (sIdChiTieu != "")
        {
            string sql = "select * from tb_ChiTieu where idChiTieu='" + sIdChiTieu + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN CHI TIÊU";
                btLuu.Text = "SỬA";
                txtNgayChi.Value = DateTime.Parse(table.Rows[0]["NgayChi"].ToString()).ToString("dd/MM/yyyy");
                txtNoiDung.Value = table.Rows[0]["NoiDung"].ToString();
                txtSoTien.Value = double.Parse(table.Rows[0]["SoTien"].ToString()).ToString("#,##").Replace(",", ".");
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string NgayChi = "";
        string NoiDung = "";
        string SoTien = "";
        //Ngày chi
        if (txtNgayChi.Value.Trim() != "")
        {
            NgayChi = StaticData.ConvertDDMMtoMMDD(txtNgayChi.Value.Trim());
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập ngày chi!')</script>");
            return;
        }
        //Nội dung
        if (txtNoiDung.Value.Trim() != "")
        {
            NoiDung = txtNoiDung.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập nội dung!')</script>");
            return;
        }
        //Số tiền
        if (txtSoTien.Value.Trim().Replace(",","").Replace(".","") != "")
        {
            SoTien = txtSoTien.Value.Trim().Replace(",", "").Replace(".", "");
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập số tiền!')</script>");
            return;
        }
        //////////
        if (sIdChiTieu == "")
        {
            string sqlInsertChiTieu = "insert into tb_ChiTieu(NgayChi,NoiDung,SoTien)";
            sqlInsertChiTieu += " values('" + NgayChi + "',N'" + NoiDung + "','" + SoTien + "')";
            bool ktInsertChiTieu = Connect.Exec(sqlInsertChiTieu);
            if (ktInsertChiTieu)
            {
                Response.Redirect("QuanLyChiTieu.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm chi tiêu!')</script>");
            }

        }
        else
        {

            string sqlUpdateChiTieu = "";
            sqlUpdateChiTieu += "update tb_ChiTieu set";
            sqlUpdateChiTieu += " NgayChi = '" + NgayChi + "'";
            sqlUpdateChiTieu += " ,NoiDung = N'" + NoiDung + "'";
            sqlUpdateChiTieu += " ,SoTien = '" + SoTien + "'";
            sqlUpdateChiTieu += " where idChiTieu ='" + sIdChiTieu + "'";
            bool ktUpdateChiTieu = Connect.Exec(sqlUpdateChiTieu);
            if (ktUpdateChiTieu)
            {
                if (Page != "")
                    Response.Redirect("QuanLyChiTieu.aspx?Page=" + Page);
                else
                    Response.Redirect("QuanLyChiTieu.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("QuanLyChiTieu.aspx");
    }
}