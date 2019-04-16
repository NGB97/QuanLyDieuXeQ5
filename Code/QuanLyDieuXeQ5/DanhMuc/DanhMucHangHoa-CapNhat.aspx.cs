using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucLoaiCuoc_CapNhat : System.Web.UI.Page
{
    string sIDHangHoa = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
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
        try
        {
            sIDHangHoa = StaticData.ValidParameter(Request.QueryString["idHangHoa"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
            LoadMaHangHoa();    
            LoadThongTinLoaiCuoc();
        }
    }
    private void LoadMaHangHoa()
    {
        string MaHangHoa = "";
        string sql = "select isnull(max(IDHangHoa),0)+1 as 'MaHangHoa' from tb_HangHoa";
        DataTable table = Connect.GetTable(sql);
        MaHangHoa = table.Rows[0]["MaHangHoa"].ToString();

        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaHangHoa.Value = "MaHangHoa" + MaHangHoa + "";

    }
    private void LoadThongTinLoaiCuoc()
    {
        if (sIDHangHoa != "")
        {
            string sql = "select * from tb_HangHoa where idHangHoa = '" + sIDHangHoa + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA LOẠI CƯỚC";
                btLuu.Text = "SỬA";
                txtMaHangHoa.Value = table.Rows[0]["MaHangHoa"].ToString();
                txtTenHangHoa.Value = table.Rows[0]["TenHangHoa"].ToString();
                txtGiaCuoc.Value = (table.Rows[0]["GiaCuoc"].ToString() == "" ? "" : double.Parse(table.Rows[0]["GiaCuoc"].ToString()).ToString("N0").Replace(",","."));
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaHangHoa = "";
        string TenHangHoa = "";
        string GiaCuoc = "";
        //Tên kho
        MaHangHoa = txtMaHangHoa.Value.Trim();
       
        //Vị trí kho
        TenHangHoa = txtTenHangHoa.Value.Trim();
        if(TenHangHoa == "")
        {
            Response.Write("<script>alert('Bạn chưa nhập tên loại cước!')</script>");
            return;
        }
        //Ghi chú
        GiaCuoc = txtGiaCuoc.Value.Trim().Replace(".","");
        //////////
        if (sIDHangHoa == "")
        {
            string sqlInsertKho = "insert into tb_HangHoa(MaHangHoa,TenHangHoa,GiaCuoc)";
            sqlInsertKho += " values(N'" + MaHangHoa + "',N'" + TenHangHoa + "','" + GiaCuoc + "')";
            bool ktInsertKho = Connect.Exec(sqlInsertKho);
            if (ktInsertKho)
            {
                Response.Redirect("DanhMucHangHoa.aspx");
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm loại cước!')</script>");
            }

        }
        else
        {

            string sqlUpdateKho = "";
            sqlUpdateKho += "update tb_HangHoa set";
            sqlUpdateKho += " MaHangHoa = N'" + MaHangHoa + "'";
            sqlUpdateKho += " ,TenHangHoa = N'" + TenHangHoa + "'";
            sqlUpdateKho += " ,GiaCuoc = '" + GiaCuoc + "'";
            sqlUpdateKho += " where idHangHoa ='" + sIDHangHoa + "'";
            bool ktUpdateKho = Connect.Exec(sqlUpdateKho);
            if (ktUpdateKho)
            {
                if (Page != "")
                    Response.Redirect("DanhMucHangHoa.aspx?Page=" + Page);
                else
                    Response.Redirect("DanhMucHangHoa.aspx");
            }
            else
            {
                Response.Write("<script>alert('Không thể cập nhật mã loại cước này. !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucHangHoa.aspx");
    }
}