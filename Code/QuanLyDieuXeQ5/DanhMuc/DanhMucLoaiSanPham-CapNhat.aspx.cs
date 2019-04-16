using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucLoaiSanPham_CapNhat : System.Web.UI.Page
{
    string sIdLoaiSanPham = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string Page = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdLoaiSanPham = StaticData.ValidParameter(Request.QueryString["idLoaiSanPham"].Trim());
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
            LoadThongTinLoaiSanPham();
        }
    }
    private void LoadThongTinLoaiSanPham()
    {
        if (sIdLoaiSanPham != "")
        {
            string sql = "select * from tb_LoaiSanPham where idLoaiSanPham='" + sIdLoaiSanPham + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN LOẠI SẢN PHẨM";
                btLuu.Text = "SỬA";
                txtTenLoaiSanPham.Value = table.Rows[0]["TenLoaiSanPham"].ToString();
                txtDonViTinh.Value = table.Rows[0]["DonViTinh"].ToString();
                txtGhiChu.Value = table.Rows[0]["GhiChu"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string TenLoaiSanPham = "";
        string DonViTinh = "";
        string GhiChu = "";
        //Tên loại sản phẩm
        if (txtTenLoaiSanPham.Value.Trim() != "")
        {
            TenLoaiSanPham = txtTenLoaiSanPham.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên loại sản phẩm!')</script>");
            return;
        }
        //Đơn vị tính
        DonViTinh = txtDonViTinh.Value.Trim();
        //Ghi chú
        GhiChu = txtGhiChu.Value.Trim();
        //////////
        if (sIdLoaiSanPham == "")
        {
            string sqlInsertLSP = "insert into tb_LoaiSanPham(TenLoaiSanPham,DonViTinh,GhiChu)";
            sqlInsertLSP += " values(N'" + TenLoaiSanPham + "',N'" + DonViTinh + "',N'" + GhiChu + "')";
            bool ktInsertLSP = Connect.Exec(sqlInsertLSP);
            if (ktInsertLSP)
            {
                Response.Redirect("DanhMucLoaiSanPham.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm loại sản phẩm!')</script>");
            }

        }
        else
        {

            string sqlUpdateLSP = "";
            sqlUpdateLSP += "update tb_LoaiSanPham set";
            sqlUpdateLSP += " TenLoaiSanPham = N'" + TenLoaiSanPham + "'";
            sqlUpdateLSP += " ,DonViTinh = N'" + DonViTinh + "'";
            sqlUpdateLSP += " ,GhiChu = N'" + GhiChu + "'";
            sqlUpdateLSP += " where idLoaiSanPham ='" + sIdLoaiSanPham + "'";
            bool ktUpdateLSP = Connect.Exec(sqlUpdateLSP);
            if (ktUpdateLSP)
            {
                if (Page != "")
                    Response.Redirect("DanhMucLoaiSanPham.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucLoaiSanPham.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucLoaiSanPham.aspx");
    }
}