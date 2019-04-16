using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_DangNhap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btDangNhap_Click(object sender, EventArgs e)
    {
        try
        {
            //Response.Redirect("Default.aspx");
            string Username = txtTenDangNhap.Value.Trim();
            string Password = txtMatKhau.Value.Trim();
            if (Username == "")
            {
                Response.Write("<script>alert('Bạn chưa nhập tên đăng nhập !')</script>");
                return;
            }
            if (Password == "")
            {
                Response.Write("<script>alert('Bạn chưa nhập mật khẩu !')</script>");
                return;
            }
            DataTable tbCheckUsername = Connect.GetTable("select top 1 * from tb_NguoiDung where TenDangNhap='" + StaticData.ValidParameter(Username) + "'");
            if (tbCheckUsername.Rows.Count > 0)
            {
                DataTable tbCheckPassword = Connect.GetTable("select top 1 * from tb_NguoiDung where TenDangNhap='" + StaticData.ValidParameter(Username) + "' and MatKhau='" + StaticData.ValidParameter(Password) + "'");
                if (tbCheckPassword.Rows.Count > 0)
                {
                    HttpCookie cookie_AdminWebsiteLuyenThi_Login = new HttpCookie("QuanLyCongNoAnhKiet_Login", Username);
                    cookie_AdminWebsiteLuyenThi_Login.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie_AdminWebsiteLuyenThi_Login);
                    Response.Redirect("/Home/Default.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Mật khẩu chưa đúng !')</script>");
                    return;
                }
            }
            else
            {
                //Response.Write("<script>alert('Tên đăng nhập chưa đúng !')</script>");
                //return;
                //Đăng nhập khách hàng
                DataTable tbCheckUsername_KH = Connect.GetTable("select top 1 * from tb_KhachHang where TenDangNhap='" + StaticData.ValidParameter(Username) + "'");
                if (tbCheckUsername_KH.Rows.Count > 0)
                {
                    DataTable tbCheckPassword_KH = Connect.GetTable("select top 1 * from tb_KhachHang where TenDangNhap='" + StaticData.ValidParameter(Username) + "' and MatKhau='" + StaticData.ValidParameter(Password) + "'");
                    if (tbCheckPassword_KH.Rows.Count > 0)
                    {
                        HttpCookie cookie_AdminWebsiteLuyenThi_Login = new HttpCookie("QuanLyCongNoAnhKiet_Login", Username);
                        cookie_AdminWebsiteLuyenThi_Login.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Add(cookie_AdminWebsiteLuyenThi_Login);
                        Response.Redirect("/QuanLyDonHang/QuanLyDonHang.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('Mật khẩu chưa đúng !')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('Tên đăng nhập chưa đúng !')</script>");
                    return;
                }
            }
        }
        catch
        {
            Response.Write("<script>alert('Vui lòng kiểm tra thông tin nhập !')</script>");
            return;
        }
    }
    
}