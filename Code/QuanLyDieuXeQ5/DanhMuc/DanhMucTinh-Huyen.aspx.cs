using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DanhMuc_DanhMucTinh_Huyen : System.Web.UI.Page
{
    string sIdTinh = "";
    string sIdHuyen = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sIdTinh = StaticData.ValidParameter(Request.QueryString["idTinh"].Trim());
            txtTenTinh.Value = StaticData.getField("tb_Tinh", "TenTinh", "idTinh", sIdTinh);
            txtTenTinh.Disabled = true;
        }
        catch { }
        try
        {
            sIdHuyen = StaticData.ValidParameter(Request.QueryString["idHuyen"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
            LoadNhanVienGiao();
            LoadLoaiCuoc();
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
            
            LoadThongTinHuyen();
            LoadHuyen(sIdTinh);
        }
    }
    private void LoadNhanVienGiao()
    {
        string strSql = "select * from tb_NguoiDung where MaQuyen='NVGN'";
        slNhanVienGiao.DataSource = Connect.GetTable(strSql);
        slNhanVienGiao.DataTextField = "HoTen";
        slNhanVienGiao.DataValueField = "idNguoiDung";
        slNhanVienGiao.DataBind();
        slNhanVienGiao.Items.Add(new ListItem("-- Chọn --", "0"));
        slNhanVienGiao.Items.FindByText("-- Chọn --").Selected = true;
    }
    private void LoadLoaiCuoc()
    {
        string strSql = "select * from tb_LoaiCuoc";
        slLoaiCuoc.DataSource = Connect.GetTable(strSql);
        slLoaiCuoc.DataTextField = "TenLoaiCuoc";
        slLoaiCuoc.DataValueField = "MaLoaiCuoc";
        slLoaiCuoc.DataBind();
        slLoaiCuoc.Items.Add(new ListItem("-- Chọn --", "0"));
        slLoaiCuoc.Items.FindByText("-- Chọn --").Selected = true;
    }
    private void LoadThongTinHuyen()
    {
        if (sIdTinh != "")
        {
            string sql = "select * from tb_Huyen where idHuyen='" + sIdHuyen + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                dvTitle.InnerHtml = "SỬA THÔNG TIN HUYỆN";
                btLuu.Text = "SỬA";
                txtTenHuyen.Value = table.Rows[0]["TenHuyen"].ToString();
                slNhanVienGiao.Value = table.Rows[0]["idNguoiDung"].ToString();
                slLoaiCuoc.Value = table.Rows[0]["MaLoaiCuoc"].ToString();
            }
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string TenHuyen = "";
        string idNguoiDung = "";
        string MaLoaiCuoc = "";
        //Tên huyên
        if (txtTenHuyen.Value.Trim() != "")
        {
            TenHuyen = txtTenHuyen.Value.Trim();
        }
        else
        {
            Response.Write("<script>alert('Bạn chưa nhập tên huyện!')</script>");
            return;
        }
        //Nhân viên giao
        idNguoiDung = slNhanVienGiao.Value.Trim();
        //Loại cước
        MaLoaiCuoc = slLoaiCuoc.Value.Trim();
        //////////
        if (sIdHuyen == "")
        {
            string sqlInsertHuyen = "insert into tb_Huyen(TenHuyen,idTinh,idNguoiDung,MaLoaiCuoc)";
            sqlInsertHuyen += " values(N'" + TenHuyen + "'";
            sqlInsertHuyen += ",'" + sIdTinh + "'";
            if (idNguoiDung != "" && idNguoiDung != "0")
                sqlInsertHuyen += ",'" + idNguoiDung + "'";
            else
                sqlInsertHuyen += ",null";
            if (MaLoaiCuoc != "" && MaLoaiCuoc != "0")
                sqlInsertHuyen += ",'" + MaLoaiCuoc + "')";
            else
                sqlInsertHuyen += ",null)";
            bool ktInsertHuyen = Connect.Exec(sqlInsertHuyen);
            if (ktInsertHuyen)
            {
                Response.Redirect("DanhMucTinh-Huyen.aspx?idTinh="+sIdTinh);
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm huyện!')</script>");
            }

        }
        else
        {

            string sqlUpdateHuyen = "";
            sqlUpdateHuyen += "update tb_Huyen set";
            sqlUpdateHuyen += " TenHuyen = N'" + TenHuyen + "'";
            if(idNguoiDung != "" && idNguoiDung != "0")
                sqlUpdateHuyen += ",idNguoiDung = '" + idNguoiDung + "'";
            else
                sqlUpdateHuyen += ",idNguoiDung = null";
            if (MaLoaiCuoc != "" && MaLoaiCuoc != "0")
                sqlUpdateHuyen += ",MaLoaiCuoc = '" + MaLoaiCuoc + "'";
            else
                sqlUpdateHuyen += ",MaLoaiCuoc = null";
            sqlUpdateHuyen += " where idHuyen ='" + sIdHuyen + "'";
            bool ktUpdateHuyen = Connect.Exec(sqlUpdateHuyen);
            if (ktUpdateHuyen)
            {
                Response.Redirect("DanhMucTinh-Huyen.aspx?idTinh="+sIdTinh);
            }
            else
            {
                Response.Write("<script>alert('Lỗi !')</script>");
            }
        }
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        Response.Redirect("DanhMucTinh-Huyen.aspx?idTinh="+sIdTinh);
    }
    private void LoadHuyen(string idTinh)
    {
        string sqlHuyen = "select * from tb_Huyen where idTinh='" + idTinh + "'";
        DataTable tbHuyen = Connect.GetTable(sqlHuyen);
        string html = @"<center><table class='table table-bordered table-striped' style='width:98%;radius:2px;'>
                            <tr>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên tỉnh
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tên quận huyện
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Nhân viên giao
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Loại cước
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;' ></th>
                            </tr>";
        for (int i = 0; i < tbHuyen.Rows.Count;i++ )
        {
            html += "<tr>";
            html += "   <td>"+ (i+1).ToString() +"</td>";
            html += "   <td>"+ StaticData.getField("tb_Tinh","TenTinh","idTinh",tbHuyen.Rows[i]["idTinh"].ToString()) +"</td>";
            html += "   <td>" + tbHuyen.Rows[i]["TenHuyen"].ToString() + "</td>";
            html += "   <td>" + StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", tbHuyen.Rows[i]["idNguoiDung"].ToString()) + "</td>";
            html += "   <td>" + StaticData.getField("tb_LoaiCuoc", "TenLoaiCuoc", "MaLoaiCuoc", tbHuyen.Rows[i]["MaLoaiCuoc"].ToString()) + "</td>";
            html += "   <td> <table BORDERCOLOR='#ffffff' border='1' style='background-color:white;border-color:white;width:70%;'><tr><td style='background-color:white;border-color:white;text-align:right;'> <a style='cursor:pointer' onclick='window.location=\"DanhMucTinh-Huyen.aspx?idTinh=" + sIdTinh + "&idHuyen=" + tbHuyen.Rows[i]["idHuyen"].ToString() + "\"'><img class='imgedit' src='../images/edit.png'/></a> ";
            html += "   </td><td style='background-color:white;' >    <a style='cursor:pointer' onclick='DeleteHuyen(\"" + tbHuyen.Rows[i]["idHuyen"].ToString() + "\")'> <img class='imgedit' src='../images/delete.png' /></a></td></tr></table></td>";
            html += "</tr>";
        }
        html += "</table>";
        dvDanhSachHuyen.InnerHtml = html;
    }
}