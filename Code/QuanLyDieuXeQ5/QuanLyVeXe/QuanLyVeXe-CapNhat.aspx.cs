using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyVeXe_QuanLyVeXe_CapNhat : System.Web.UI.Page
{
    string sIdNguoiDung = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdNguoiDung = StaticData.ValidParameter(Request.QueryString["idNguoiDung"].Trim());
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
            //LoadQuyen();
            LoadThongTinNguoiDung();
        }
    }
    //private void LoadQuyen()
    //{
    //    slQuyen.Items.Add(new ListItem("Đã Thanh Toán", "0"));
    //    slQuyen.Items.FindByText("Đã Thanh Toán").Selected = true;
    //}
    private void LoadThongTinNguoiDung()
    {
        if (sIdNguoiDung != "")
        {
            string sql = "select * from tb_NguoiDung where idNguoiDung='" + sIdNguoiDung + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN NGƯỜI DÙNG";
                btLuu.Text = "SỬA";
                txtHoTen.Value = table.Rows[0]["HoTen"].ToString();
                txtSoDienThoai.Value = table.Rows[0]["SoDienThoai"].ToString();
               // txtEmail.Value = table.Rows[0]["Email"].ToString();
                txtDiaChi.Value = table.Rows[0]["DiaChi"].ToString();
                //slQuyen.Value = table.Rows[0]["MaQuyen"].ToString();
                txtTenDangNhap.Value = table.Rows[0]["TenDangNhap"].ToString();
                //txtTenDangNhap.Disabled = true;
                txtMatKhau.Value = table.Rows[0]["MatKhau"].ToString();
                txtChiNhanhGui.Value = StaticData.getField("tb_ChiNhanh", "TenChiNhanh", "IDChiNhanh", table.Rows[0]["ChiNhanh"].ToString());
                hdIDChiNhanhGui.Value = table.Rows[0]["ChiNhanh"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string HoTen = "";
        string SoDienThoai = "";
        string Email = "";
        string DiaChi = "";
        string idChiNhanhGui = "";
        string MaQuyen = "";
        string TenDangNhap = "";
        string MatKhau = "";
        //Họ tên
        if (txtHoTen.Value.Trim() != "")
        {
            HoTen = txtHoTen.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập họ tên!')</script>");
            return;
        }
        //Số điện thoại
        SoDienThoai = txtSoDienThoai.Value.Trim();
        //Email
        //Email = txtEmail.Value.Trim();
        idChiNhanhGui = hdIDChiNhanhGui.Value.Trim();

        //Địa chỉ
        DiaChi = txtDiaChi.Value.Trim();
        //Quyền
        //if(slQuyen.Value.Trim() != "" && slQuyen.Value.Trim() != "0")
        //{
        //    MaQuyen = slQuyen.Value.Trim();
        //}
        //else
        //{
        //    Response.Write("<script>alert('Bạn chưa chọn quyền!')</script>");
        //    return;
        //}
        //Tên đăng nhập
        if (txtTenDangNhap.Value.Trim() != "")
        {
            if (sIdNguoiDung == "")
            {
                string sqlTenDangNhapND = "select idNguoiDung from tb_NguoiDung where TenDangNhap ='" + txtTenDangNhap.Value.Trim() + "'";
                DataTable tbTenDangNhapND = Connect.GetTable(sqlTenDangNhapND);
             //   string sqlTenDangNhapKH = "select idKhachHang from tb_KhachHang where TenDangNhap='" + txtTenDangNhap.Value.Trim() + "'";
             //   DataTable tbTenDangNhapKH = Connect.GetTable(sqlTenDangNhapKH);
                if (tbTenDangNhapND.Rows.Count > 0)
                {
                    Response.Write("<script>alert('Tên đăng nhập đã tồn tại!')</script>");
                    return;
                }
                else
                {
                    TenDangNhap = StaticData.ValidParameter(txtTenDangNhap.Value.Trim());
                }
            }
            else
            {
                //TenDangNhap = StaticData.ValidParameter(txtTenDangNhap.Value.Trim());
                string sqlTenDangNhapND = "select idNguoiDung from tb_NguoiDung where TenDangNhap='" + txtTenDangNhap.Value.Trim() + "' and idNguoiDung!='" + sIdNguoiDung + "'";
                DataTable tbTenDangNhapND = Connect.GetTable(sqlTenDangNhapND);
                //string sqlTenDangNhapKH = "select idKhachHang from tb_KhachHang where TenDangNhap='" + txtTenDangNhap.Value.Trim() + "'";
                //DataTable tbTenDangNhapKH = Connect.GetTable(sqlTenDangNhapKH);
                if (tbTenDangNhapND.Rows.Count > 0 )
                {
                    Response.Write("<script>alert('Tên đăng nhập đã tồn tại!')</script>");
                    return;
                }
                else
                {
                    TenDangNhap = StaticData.ValidParameter(txtTenDangNhap.Value.Trim());
                }
            }
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên đăng nhập!')</script>");
            return;
        }
        //Mật khẩu
        if (txtMatKhau.Value.Trim() != "")
        {
            MatKhau = txtMatKhau.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập mật khẩu!')</script>");
            return;
        }
        //////////
        if (sIdNguoiDung == "")
        {
            string sqlInsertNguoiDung = "insert into tb_NguoiDung(HoTen,SoDienThoai,Email,DiaChi,MaQuyen,TenDangNhap,MatKhau,ChiNhanh)";
            sqlInsertNguoiDung += " values(N'" + HoTen + "','" + SoDienThoai + "','" + Email + "',N'" + DiaChi + "','" + MaQuyen + "','" + TenDangNhap + "','" + MatKhau + "','" + idChiNhanhGui + "')";
            bool ktInsertNguoiDung = Connect.Exec(sqlInsertNguoiDung);
            if (ktInsertNguoiDung)
            {
                Response.Redirect("DanhMucNguoiDung.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm người dùng!')</script>");
            }

        }
        else
        {

            string sqlUpdateKNguoiDung = "";
            sqlUpdateKNguoiDung += "update tb_NguoiDung set";
            sqlUpdateKNguoiDung += " HoTen = N'" + HoTen + "'";
            sqlUpdateKNguoiDung += " ,SoDienThoai = '" + SoDienThoai + "'";
            sqlUpdateKNguoiDung += " ,Email = '" + Email + "'";
            sqlUpdateKNguoiDung += " ,DiaChi = N'" + DiaChi + "'";
            sqlUpdateKNguoiDung += " ,MaQuyen = '" + MaQuyen + "'";
            sqlUpdateKNguoiDung += " ,MatKhau = '" + MatKhau + "'";
            sqlUpdateKNguoiDung += " ,TenDangNhap = '" + TenDangNhap + "'";
            sqlUpdateKNguoiDung += " ,ChiNhanh = '" + idChiNhanhGui + "'";
            sqlUpdateKNguoiDung += " where idNguoiDung ='" + sIdNguoiDung + "'";
            bool ktUpdateNguoiDung = Connect.Exec(sqlUpdateKNguoiDung);
            if (ktUpdateNguoiDung)
            {
                if (Page != "")
                    Response.Redirect("DanhMucNguoiDung.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucNguoiDung.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucNguoiDung.aspx");
    }
}