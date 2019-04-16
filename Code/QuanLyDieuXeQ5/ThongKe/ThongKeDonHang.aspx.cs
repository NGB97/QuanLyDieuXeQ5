using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ThongKe_ThongKeDonHang : System.Web.UI.Page
{
    int SoHoaDon = 0;
    double TongSoLuong = 0;
    double TongTienHang = 0;
    double TongTienCuoc = 0;

    string sTuNgay = "";
    string sDenNgay = "";
    string sMaDonHang = "";
    string sIdKho = "";
    string sIdNguoiDung = "";
    string sMaTinhTrang = "";
    string sIdKhachHang = "";

    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";

    string txtFistPage = "1";
    string txtPage1 = "";
    string txtPage2 = "";
    string txtPage3 = "";
    string txtPage4 = "";
    string txtPage5 = "";
    string txtLastPage = "";
    int Page = 0;
    int MaxPage = 0;
    int PageSize = 70;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Page = int.Parse(Request.QueryString["Page"].ToString());
        }
        catch
        {
            Page = 1;
        }

        if (!IsPostBack)
        {
            if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
            {
                mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
                mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
                mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "KH" && mQuyen.ToUpper() != "QTWS")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                if(mQuyen.ToUpper() == "KH")
                {
                    txtTenKhachHang.Disabled = true;
                }
            }
            LoadTinhTrang();
            LoadKho();
            LoadNhanVienGiao();
            try
            {
                if (Request.QueryString["TuNgay"].Trim() != "")
                {
                    sTuNgay = Request.QueryString["TuNgay"].Trim();
                    txtTuNgay.Value = sTuNgay;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["DenNgay"].Trim() != "")
                {
                    sDenNgay = Request.QueryString["DenNgay"].Trim();
                    txtDenNgay.Value = sDenNgay;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["MaDonHang"].Trim() != "")
                {
                    sMaDonHang = Request.QueryString["MaDonHang"].Trim();
                    txtMaDonHang.Value = sMaDonHang;
                }
            }
            catch { }


            try
            {
                if (Request.QueryString["idKho"].Trim() != "")
                {
                    sIdKho = Request.QueryString["idKho"].Trim();
                    slKho.Value = sIdKho;
                }
            }
            catch { }



            try
            {
                if (Request.QueryString["idNguoiDung"].Trim() != "")
                {
                    sIdNguoiDung = Request.QueryString["idNguoiDung"].Trim();
                    slNhanVienGiao.Value = sIdNguoiDung;
                }
            }
            catch { }


            try
            {
                if (Request.QueryString["MaTinhTrang"].Trim() != "")
                {
                    sMaTinhTrang = Request.QueryString["MaTinhTrang"].Trim();
                    slTinhTrang.Value = sMaTinhTrang;
                }
            }
            catch { }
            try
            {
                if (Request.QueryString["idKhachHang"].Trim() != "")
                {
                    sIdKhachHang = Request.QueryString["idKhachHang"].Trim();
                    hdIdKhachHang.Value = sIdKhachHang;
                    txtTenKhachHang.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", sIdKhachHang);
                }
            }
            catch { }
            LoadDonHang();
        }
    }
    private void LoadTinhTrang()
    {
        string strSql = "select * from tb_TinhTrang";
        slTinhTrang.DataSource = Connect.GetTable(strSql);
        slTinhTrang.DataTextField = "TenTinhTrang";
        slTinhTrang.DataValueField = "MaTinhTrang";
        slTinhTrang.DataBind();
        slTinhTrang.Items.Add(new ListItem("-- Chọn tình trạng --", "0"));
        slTinhTrang.Items.FindByText("-- Chọn tình trạng --").Selected = true;
    }
    private void LoadKho()
    {
        string strSql = "select * from tb_Kho";
        slKho.DataSource = Connect.GetTable(strSql);
        slKho.DataTextField = "TenKho";
        slKho.DataValueField = "idKho";
        slKho.DataBind();
        slKho.Items.Add(new ListItem("-- Chọn kho --", "0"));
        slKho.Items.FindByText("-- Chọn kho --").Selected = true;
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



    #region paging
    private void SetPage()
    {
        string sql = "select count(idDonHang) from tb_DonHang where '1'='1'";
        if (sMaDonHang != "")
            sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdKho != "" && sIdKho != "0")
            sql += " and idKho = '" + sIdKho + "'";

        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and idNguoiDung = '" + sIdNguoiDung + "'";


        if (sMaTinhTrang != "" && sMaTinhTrang != "0")
            sql += " and MaTinhTrang = '" + sMaTinhTrang + "'";
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
        if(mQuyen.ToUpper() == "KH")
            sql += " and idKhachHang = '" + mIdKhachHang + "'";
        if (sIdKhachHang != "" && sIdKhachHang != "")
            sql += " and idKhachHang='" + sIdKhachHang + "'";
        DataTable tbTotalRows = Connect.GetTable(sql);
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
        SoHoaDon = TotalRows;
        if (TotalRows % PageSize == 0)
            MaxPage = TotalRows / PageSize;
        else
            MaxPage = TotalRows / PageSize + 1;
        txtLastPage = MaxPage.ToString();
        if (Page == 1)
        {
            for (int i = 1; i <= MaxPage; i++)
            {
                if (i <= 5)
                {
                    switch (i)
                    {
                        case 1: txtPage1 = i.ToString(); break;
                        case 2: txtPage2 = i.ToString(); break;
                        case 3: txtPage3 = i.ToString(); break;
                        case 4: txtPage4 = i.ToString(); break;
                        case 5: txtPage5 = i.ToString(); break;
                    }
                }
                else
                    return;
            }
        }
        else
        {
            if (Page == 2)
            {
                for (int i = 1; i <= MaxPage; i++)
                {
                    if (i == 1)
                        txtPage1 = "1";
                    if (i <= 5)
                    {
                        switch (i)
                        {
                            case 2: txtPage2 = i.ToString(); break;
                            case 3: txtPage3 = i.ToString(); break;
                            case 4: txtPage4 = i.ToString(); break;
                            case 5: txtPage5 = i.ToString(); break;
                        }
                    }
                    else
                        return;
                }
            }
            else
            {
                int Cout = 1;
                if (Page <= MaxPage)
                {
                    for (int i = Page; i <= MaxPage; i++)
                    {
                        if (i == Page)
                        {
                            txtPage1 = (Page - 2).ToString();
                            txtPage2 = (Page - 1).ToString();
                        }
                        if (Cout <= 3)
                        {
                            if (i == Page)
                                txtPage3 = i.ToString();
                            if (i == (Page + 1))
                                txtPage4 = i.ToString();
                            if (i == (Page + 2))
                                txtPage5 = i.ToString();
                            Cout++;
                        }
                        else
                            return;
                    }
                }
                else
                {
                    //Page = MaxPage;
                    SetPage();
                }
            }
        }
    }
    #endregion
    private void LoadDonHang()
    {
        string sql = "";
        sql += @"select * from
            (
	            SELECT ROW_NUMBER() OVER
                  (
                        ORDER BY idDonHang desc
                  )AS RowNumber
	              ,*
                  FROM tb_DonHang where 1 = 1
            ";
        if (sMaDonHang != "")
            sql += " and MaDonHang like N'%" + sMaDonHang + "%'";
        if (sIdKho != "" && sIdKho != "0")
            sql += " and idKho = '" + sIdKho + "'";

        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            sql += " and idNguoiDung = '" + sIdNguoiDung + "'";


        if (sMaTinhTrang != "" && sMaTinhTrang != "0")
            sql += " and MaTinhTrang = '" + sMaTinhTrang + "'";
        if(sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 23:59:59'";
        if (mQuyen.ToUpper() == "KH")
            sql += " and idKhachHang = '" + mIdKhachHang + "'";
        if (sIdKhachHang != "" && sIdKhachHang != "")
            sql += " and idKhachHang='" + sIdKhachHang + "'";
        sql += ") as tb1 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã đơn hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày lập
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Khách hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Người nhận
                                </th>
                               
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Địa chỉ người nhận
                                </th>

                            <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Nhân viên giao hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Số điện thoại
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tổng số lượng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tổng tiền hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tổng tiền cước
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Tình trạng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                </th>
                            </tr>";
        for (int i = 0; i < table.Rows.Count; i++)
        {
            string sqlCTDH = "select TongSoLuong=sum(isnull(SoLuong,0)),TongTienHang=sum(isnull(TienHang,0)),TongTienCuoc=sum(isnull(TienCuoc,0)) from tb_ChiTietDonHang where idDonHang='" + table.Rows[i]["idDonHang"].ToString() + "'";
            DataTable tbCTDH = Connect.GetTable(sqlCTDH);
            html += "       <tr>";
            html += "       <td>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
            html += "       <td>" + table.Rows[i]["MaDonHang"].ToString() + "</td>";
            if (table.Rows[i]["NgayLap"].ToString() != "")
                html += "       <td>" + DateTime.Parse(table.Rows[i]["NgayLap"].ToString()).ToString("dd/MM/yyyy") + "</td>";
            else
                html += "       <td></td>";
            html += "       <td>" + StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[i]["idKhachHang"].ToString()) + "</td>";
            html += "       <td>" + table.Rows[i]["NguoiNhan"].ToString() + "</td>";
            //if (table.Rows[i]["ThoiDiemDuKienGiao"].ToString() != "")
            //    html += "<td>" + DateTime.Parse(table.Rows[i]["ThoiDiemDuKienGiao"].ToString()).ToString("dd/MM/yyyy hh:mm") + "</td>";
            //else
            //    html += "<td></td>";
            //html += "       <td>" + StaticData.getField("tb_Kho", "TenKho", "idKho", table.Rows[i]["idKho"].ToString()) + "</td>";
            html += "       <td>" + table.Rows[i]["DiaChiNguoiNhan"].ToString() + "</td>";
            html += "       <td>" + StaticData.getField("tb_NguoiDung", "HoTen", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</td>";
            html += "       <td>" + StaticData.getField("tb_NguoiDung", "SoDienThoai", "idNguoiDung", table.Rows[i]["idNguoiDung"].ToString()) + "</td>";
            if (tbCTDH.Rows.Count > 0)
            {
                if (tbCTDH.Rows[0]["TongSoLuong"].ToString() != "")
                {
                    html += "<td>" + tbCTDH.Rows[0]["TongSoLuong"].ToString() + "</td>";
                    TongSoLuong += int.Parse(tbCTDH.Rows[0]["TongSoLuong"].ToString());
                }
                else
                    html += "<td>0</td>";
                if (tbCTDH.Rows[0]["TongTienHang"].ToString() != "")
                {
                    html += "<td>" + double.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
                    TongTienHang += double.Parse(tbCTDH.Rows[0]["TongTienHang"].ToString());
                }
                else
                    html += "<td>0</td>";
                if (tbCTDH.Rows[0]["TongTienCuoc"].ToString() != "")
                {
                    html += "<td>" + double.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString()).ToString("#,##").Replace(",", ".") + "</td>";
                    TongTienCuoc += double.Parse(tbCTDH.Rows[0]["TongTienCuoc"].ToString());
                }
                else
                    html += "<td>0</td>";
            }
            else
            {
                html += "<td></td>";
                html += "<td></td>";
                html += "<td></td>";
            }
            html += "       <td>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", table.Rows[i]["MaTinhTrang"].ToString()) + "</td>";
            html += "       <td><a style='cursor:pointer' onclick='XemChiTietDonHang(\"" + table.Rows[i]["idDonHang"].ToString() + "\")'>Xem chi tiết</a></td>";
            html += "       </tr>";

        }
        html += "  </table><table >   <tr>";
        html += "       <td colspan='21' class='footertable'>";
        string url = "ThongKeDonHang.aspx?";
        if (sMaDonHang != "")
            url += "MaDonHang=" + sMaDonHang + "&";
        if (sIdKho != "" && sIdKho != "0")
            url += "idKho=" + sIdKho + "&";


        if (sIdNguoiDung != "" && sIdNguoiDung != "0")
            url += "idNguoiDung=" + sIdNguoiDung + "&";


        if (sMaTinhTrang != "" && sMaTinhTrang != "0")
            url += "MaTinhTrang=" + sMaTinhTrang + "&";
        if (sTuNgay != "")
            url += "TuNgay=" + sTuNgay + "&";
        if (sDenNgay != "")
            url += "DenNgay=" + sDenNgay + "&";
        if (sIdKhachHang != "" && sIdKhachHang != "0")
            url += "idKhachHang=" + sIdKhachHang;
        url += "Page=";
        html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
        html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
        html += "     </table></center>";
        html += "<div style='text-align:right;font-size: 20px;'><b>Tổng số hóa đơn:</b> " + SoHoaDon.ToString() + "</div>";
        html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền hàng:</b> " + TongTienHang.ToString("#,##").Replace(",", ".") + "</div>";
        html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền cước:</b> " + TongTienCuoc.ToString("#,##").Replace(",", ".") + "</div>";
        dvDanhSachDonHang.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string MaDonHang = txtMaDonHang.Value.Trim();
        string idKho = slKho.Value.Trim();
        string MaTinhTrang = slTinhTrang.Value.Trim();
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        string idKhachHang = hdIdKhachHang.Value.Trim();
        string idNguoiDung = slNhanVienGiao.Value.Trim();

        string url = "ThongKeDonHang.aspx?";
        if (MaDonHang != "")
            url += "MaDonHang=" + MaDonHang + "&";
        if (idKho != "" && idKho != "0")
            url += "idKho=" + idKho + "&";
        if (MaTinhTrang != "" && MaTinhTrang != "0")
            url += "MaTinhTrang=" + MaTinhTrang + "&";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "&";
        if (idKhachHang != "" && idKhachHang != "0")
            url += "idKhachHang=" + idKhachHang + "&";

        if (idNguoiDung != "" && idNguoiDung != "0")
            url += "idNguoiDung=" + idNguoiDung + "&";

        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "ThongKeDonHang.aspx";
        Response.Redirect(url);
    }
}