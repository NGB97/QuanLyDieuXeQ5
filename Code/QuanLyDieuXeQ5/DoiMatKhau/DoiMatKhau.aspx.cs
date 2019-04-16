using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DoiMatKhau_DoiMatKhau : System.Web.UI.Page
{
    string mTenDangNhap = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
        {
            mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MatKhauCu = txtMatKhauCu.Value.Trim();
        string MatKhauMoi = txtMatKhauMoi.Value.Trim();
        string NhapLai = txtNhapLai.Value.Trim();
        if(MatKhauCu == "" || MatKhauMoi == "" || NhapLai == "")
        {
            Response.Write("<script>alert('Bạn phải nhập đầy đủ thông tin!')</script>");
            return;
        }
        else
        {
            string mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
            if (mQuyen.ToUpper() != "KH")
            {
                string sqlCheckMatKhauCu = "select TenDangNhap from tb_NguoiDung where TenDangNhap='" + mTenDangNhap + "' and MatKhau='" + MatKhauCu + "'";
                DataTable tbCheckMatKhauCu = Connect.GetTable(sqlCheckMatKhauCu);
                if (tbCheckMatKhauCu.Rows.Count > 0)
                {
                    if (MatKhauMoi == NhapLai)
                    {
                        string sqlUpdateMatKhau = "update tb_NguoiDung set MatKhau='" + MatKhauMoi + "' where TenDangNhap='" + mTenDangNhap + "'";
                        bool ktUpdateMatKhau = Connect.Exec(sqlUpdateMatKhau);
                        if (ktUpdateMatKhau)
                        {
                            Response.Write("<script>alert('Đổi mật khẩu thành công!')</script>");
                            return;
                        }
                        else
                        {
                            Response.Write("<script>alert('Lỗi đổi mật khẩu!')</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Mật khẩu mới và nhập lại không giống nhau!')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Mật khẩu cũ chưa đúng!')</script>");
                    return;
                }
            }
            else
            {
                //Khách hàng
                string sqlCheckMatKhauCu = "select TenDangNhap from tb_KhachHang where TenDangNhap='" + mTenDangNhap + "' and MatKhau='" + MatKhauCu + "'";
                DataTable tbCheckMatKhauCu = Connect.GetTable(sqlCheckMatKhauCu);
                if (tbCheckMatKhauCu.Rows.Count > 0)
                {
                    if (MatKhauMoi == NhapLai)
                    {
                        string sqlUpdateMatKhau = "update tb_KhachHang set MatKhau='" + MatKhauMoi + "' where TenDangNhap='" + mTenDangNhap + "'";
                        bool ktUpdateMatKhau = Connect.Exec(sqlUpdateMatKhau);
                        if (ktUpdateMatKhau)
                        {
                            Response.Write("<script>alert('Đổi mật khẩu thành công!')</script>");
                            return;
                        }
                        else
                        {
                            Response.Write("<script>alert('Lỗi đổi mật khẩu!')</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Mật khẩu mới và nhập lại không giống nhau!')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Mật khẩu cũ chưa đúng!')</script>");
                    return;
                }
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DoiMatKhau.aspx");
    }
}