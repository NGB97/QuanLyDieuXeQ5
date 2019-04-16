using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;

public partial class ThongKe_ThongKeNhanVienThuTien : System.Web.UI.Page
{
    int SoHoaDon = 0;
    double TongSoLuong = 0;
    double TongTienHang = 0;
    double TongTienCuoc = 0;
    double TongPhuPhi = 0;
    string sTuNgay = "";
    string sDenNgay = "";
    string sNguoiGui = "";

    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";

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
                mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
                if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
            }
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
                if (Request.QueryString["NguoiGui"].Trim() != "")
                {
                    sNguoiGui = Request.QueryString["NguoiGui"].Trim();
                    txtNguoiGui.Value = sNguoiGui;
                }
            }
            catch { }
            LoadDonHang();
        }
    }


    
    #region paging
    private void SetPage()
    {
        string sql = @" select Count(NgayLap) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber) from 
(
select *, 
'TT' = Sum(Tong) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
 from ( select *
, 'Tong' = CuocHangGui + CuocHangNhan + ThuNo - CPNTra + CPNNhan + CODNhan - CODTra - ChiKhac
      ,'Count' = Count(NgayLap) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
	   from ( SELECT DENSE_RANK() OVER (ORDER BY CONVERT(date, NgayLap, 111) DESC) AS RowNumber
                    ,* FROM (
				      select *,
    'CuocHangGui' = (select ISNULL(sum(ThanhToan),0) from tb_DonHang dh where dh.idNguoiDung = nd.idNguoiDung and NgayLap = tb1.NgayLap),
   'CuocHangNhan' = (select ISNULL(sum(TienTraHang),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   'ThuNo' = (select ISNULL(sum(SoTien),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   
   'CPNTra' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
   'CPNNhan' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_DonHang where idNguoiDung = nd.idNguoiDung and NgayLap  = tb1.NgayLap),
   'CODNhan' = (select ISNULL(sum(ThanhToanCOD),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
     'CODTra' = (select ISNULL(sum(SoTien),0) from tb_TraNoCOD where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
	      'ChiKhac' = (select ISNULL(sum(SoTien),0) from tb_ChiKhac where idNguoiDung = nd.idNguoiDung and NgayChi  = tb1.NgayLap)
   from tb_NguoiDung nd , (select NgayLap from tb_DonHang union select NgayTra from tb_TraNoKhachHang union select NgayTra from tb_TraNoCOD) as tb1 
                ) AS TB WHERE 1 = 1 and MaQuyen = 'NVVP'  ";
        if (mQuyen.ToUpper() != "ADMIN")
        {
            sql += "and TB.idNguoiDung = ' " + mIdNguoiDung + "'";
        }
         if (sTuNgay != "")
             sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sNguoiGui != "")
        //    sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

        sql += ") as tb2 ) as tb3 ) as tb4";
       // sql += ") as tb2 ";
        //if (sNguoiGui != "")
        //    sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

        DataTable tbTotalRows = Connect.GetTable(sql);
       // int TotalRows = tbTotalRows.Rows.Count;
        int TotalRows = int.Parse(tbTotalRows.Rows[0][0].ToString());
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
        sql += @"select *, 
'TT' = Sum(Tong) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
 from ( select *
, 'Tong' = CuocHangGui + CuocHangNhan + ThuNo - CPNTra + CPNNhan + CODNhan - CODTra - ChiKhac
      ,'Count' = Count(NgayLap) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
	   from ( SELECT DENSE_RANK() OVER (ORDER BY CONVERT(date, NgayLap, 111) DESC) AS RowNumber
                    ,* FROM (
				      select *,
    'CuocHangGui' = (select ISNULL(sum(ThanhToan),0) from tb_DonHang dh where dh.idNguoiDung = nd.idNguoiDung and NgayLap = tb1.NgayLap),
   'CuocHangNhan' = (select ISNULL(sum(TienTraHang),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   'ThuNo' = (select ISNULL(sum(SoTien),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   
   'CPNTra' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
   'CPNNhan' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_DonHang where idNguoiDung = nd.idNguoiDung and NgayLap  = tb1.NgayLap),
   'CODNhan' = (select ISNULL(sum(ThanhToanCOD),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
     'CODTra' = (select ISNULL(sum(SoTien),0) from tb_TraNoCOD where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
	      'ChiKhac' = (select ISNULL(sum(SoTien),0) from tb_ChiKhac where idNguoiDung = nd.idNguoiDung and NgayChi  = tb1.NgayLap)
   from tb_NguoiDung nd , (select NgayLap from tb_DonHang union select NgayTra from tb_TraNoKhachHang union select NgayTra from tb_TraNoCOD) as tb1 
                ) AS TB WHERE 1 = 1 and MaQuyen = 'NVVP'  ";
        if (mQuyen.ToUpper() != "ADMIN")
        {
            sql += "and TB.idNguoiDung = ' " + mIdNguoiDung + "'";
        }
         if (sTuNgay != "")
             sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sNguoiGui != "")
        //    sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

        sql += ") as tb2 ) as tb3 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1  order by RowNumber ";


        DataTable table = Connect.GetTable(sql);
        //txtNoiDung.InnerHtml = table.Rows[0]["NoiDung"].ToString();
        SetPage();
        string html = "";
        if (mQuyen.ToUpper() != "ADMIN")
        {
                 html += @"<center><table class='table table-bordered table-hover dataTable'>
                                <tr>
                                   <th class='th'>
                                    STT
                                </th>
                                <th class='th'>
                                    Ngày
                                </th>
                            
                                <th class='th'>
                                    Cước Hàng Gửi
                                </th>
                                <th class='th'>
                                    Cước Hàng Nhận
                                </th>
                                <th class='th'>
                                    Thu Nợ
                                </th>
                                <th class='th'>
                                    Chuyển Phát Nhanh Trả
                                </th>
                                <th class='th'>
                                    Chuyển Phát Nhanh Nhận
                                </th>
                                <th class='th'>
                                    COD Nhận
                                </th>
                                <th class='th'>
                                    COD Trả
                                </th>
                 
                                    <th class='th'>
                                    Chi Khác
                                </th>        
                                    <th class='th'>
                                    Tổng Theo Ngày
                                </th>                      
                                </tr>";

                html += "           <tr style='background: #ff7423;'>";
                html += "               <td colspan='2' style='text-align:center;vertical-align: inherit;'><b>Tổng cộng</b></td>";

                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CuocHangGui)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CuocHangNhan)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(ThuNo)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CPNTra)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CPNNhan)", string.Empty).ToString()) + "</b></td>";

                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CODNhan)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(CODTra)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(ChiKhac)", string.Empty).ToString()) + "</b></td>";
                html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + StaticData.ConVertstringtodouble(table.Compute("SUM(Tong)", string.Empty).ToString()) + "</b></td>";
           // html += "               <td colspan='1' style='text-align:center;vertical-align: inherit;'><b></b></td>";
                //   html += "               <td style='text-align:center;vertical-align: inherit;'><b>" + (TongTienHang + TongTienCuoc + TongPhuPhi).ToString("N0").Replace(",", ".") + "</b></td>";
                html += "           </tr>";
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    html += "       <tr>";
                    html += "       <td style='text-align:center;vertical-align: inherit;' >" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
                    DateTime Ngay = DateTime.Parse(table.Rows[i]["NgayLap"].ToString());
                    html += "       <td style='text-align:center;vertical-align: inherit;font-size:20px;white-space: nowrap;'>" + Ngay.ToString("dd/MM/yyyy") + "</td>";
                    //    html += "       <td style='text-align:center;vertical-align: inherit;' > <a href='../DanhMuc/DanhMucKhachHang-CapNhat.aspx?idKhachHang=" + table.Rows[i]["idKhachHang"].ToString() + "' >  " + StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[i]["idKhachHang"].ToString()) + " </a></td>";

                    double CuocHangGui = (table.Rows[i]["CuocHangGui"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangGui"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "</td>";

                    double CuocHangNhan = (table.Rows[i]["CuocHangNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangNhan"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangNhan.ToString("N0").Replace(",", ".") + "</td>";

                    double ThuNo = (table.Rows[i]["ThuNo"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ThuNo"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + ThuNo.ToString("N0").Replace(",", ".") + "</td>";

                    double CPNTra = (table.Rows[i]["CPNTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNTra"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNTra.ToString("N0").Replace(",", ".") + "</td>";

                    double CPNNhan = (table.Rows[i]["CPNNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNNhan"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNNhan.ToString("N0").Replace(",", ".") + "</td>";

                    double CODNhan = (table.Rows[i]["CODNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODNhan"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODNhan.ToString("N0").Replace(",", ".") + "</td>";

                    double CODTra = (table.Rows[i]["CODTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODTra"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODTra.ToString("N0").Replace(",", ".") + "</td>";
                    double ChiKhac = (table.Rows[i]["ChiKhac"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChiKhac"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;'>" + ChiKhac.ToString("N0").Replace(",", ".") + "</td>";

                    double Tong = (table.Rows[i]["Tong"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["Tong"].ToString()));
                    html += "       <td style='text-align:center;vertical-align: inherit;background-color:rgba(37, 170, 226, 0.1)'>" + Tong.ToString("N0").Replace(",", ".") + "</td>";
                    html += "       </tr>";
                }
                html += "  </table><table >   <tr>";
                html += "       <td colspan='21' class='footertable'>";
                string url = "ThongKeDoanhThu.aspx?";

                //if (sNguoiGui != "")
                //    url += "NguoiGui=" + sNguoiGui + "&";

                url += "Page=";
                html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
                html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
                html += "     </table></center>";

        }
        else
        {
            html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr>
                                <th class='th'>
                                    STT
                                </th>
                                <th class='th'>
                                    Ngày
                                </th>
                                <th class='th'>
                                    Họ Tên NV
                                </th>
                                <th class='th'>
                                    Số Điện Thoại
                                </th>
                                <th class='th'>
                                    Cước Hàng Gửi
                                </th>
                                <th class='th'>
                                    Cước Hàng Nhận
                                </th>
                                <th class='th'>
                                    Thu Nợ
                                </th>
                                <th class='th'>
                                    Chuyển Phát Nhanh Chi
                                </th>
                                <th class='th'>
                                    Chuyển Phát Nhanh Nhận
                                </th>
                                <th class='th'>
                                    COD Nhận
                                </th>
                                <th class='th'>
                                    COD Chi
                                </th>
                                <th class='th'>
                                    Chi Khác
                                </th>
                                <th class='th'>
                                    Tổng Từng Người
                                </th>

    
                                <th class='th'>
                                    Số NV 
                                </th>
                                                                <th class='th'>
                                    Tổng Tất Cả
                                </th>
                                
                            </tr>";
            //html += "<tr style='background: #f78625d9;font-weight: bold;'>";
            //html += "<td style='text-align:center;vertical-align: inherit;' colspan='5'> TỔNG</td>";
            //html += "<td style='text-align:center;vertical-align: inherit;'>" + MyStaticData.TongSoLuongNhapHang("Sum", false, sTuNgay, sDenNgay, sMaDonHang).ToString("N0").Replace(",", ".") + "</td>";
            //html += "<td style='text-align:center;vertical-align: inherit;'>" + MyStaticData.TongSoLuongNhapHang("count", false, sTuNgay, sDenNgay, sMaDonHang).ToString("N0").Replace(",", ".") + "</td>";
            //html += "<td style='text-align:center;vertical-align: inherit;'>" + MyStaticData.TongSoLuongNhapHang("Sum", true, sTuNgay, sDenNgay, sMaDonHang).ToString("N0").Replace(",", ".") + "</td>";
            //html += "</tr>";
            if (table.Rows.Count > 0)
            {
                string flag = StaticData.ConvertMMDDYYtoDDMMYY(table.Rows[0]["NgayLap"].ToString());
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string idNguoiDung = table.Rows[i]["idNguoiDung"].ToString();
                    //string idDonHang = table.Rows[i]["idPhieuNhap"].ToString();
                    if (StaticData.ConvertMMDDYYtoDDMMYY(table.Rows[i]["NgayLap"].ToString()) != flag || i == 0)
                    {
                        DateTime Ngay = DateTime.Parse(table.Rows[i]["NgayLap"].ToString());
                        if (int.Parse(table.Rows[i]["RowNumber"].ToString()) % 2 == 0)
                            html += "       <tr style='background-color:white'>";
                        else
                            html += "       <tr style='background-color:rgba(37, 170, 226, 0.1)'>";


                        html += "       <td rowspan=" + table.Rows[i]["Count"].ToString() + " style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["RowNumber"].ToString() + "</td>";
                        html += "       <td rowspan=" + table.Rows[i]["Count"].ToString() + " style='text-align:center;vertical-align: inherit;'>" + Ngay.ToString("dd/MM/yyyy") + "</td>";
                       
                        
                        html += "       <td  style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["HoTen"].ToString() + "</td>";
                        html += "       <td  style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";

                        //double CuocHangGui = (table.Rows[i]["CuocHangGui"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangGui"].ToString()));
                        //html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "<br>";
                        //html += "           <a href='#' onclick='LoadPopupHangGuiXem(" + idNguoiDung + ");'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        //html += "    </td>";
                        double CuocHangGui = (table.Rows[i]["CuocHangGui"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangGui"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupHangGuiXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CuocHangNhan = (table.Rows[i]["CuocHangNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupHangNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double ThuNo = (table.Rows[i]["ThuNo"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ThuNo"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + ThuNo.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupThuNoXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CPNTra = (table.Rows[i]["CPNTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNTra"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNTra.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCPNTraXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CPNNhan = (table.Rows[i]["CPNNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCPNNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CODNhan = (table.Rows[i]["CODNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCODNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CODTra = (table.Rows[i]["CODTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODTra"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODTra.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCODTraGuiXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";
                        double ChiKhac = (table.Rows[i]["ChiKhac"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChiKhac"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + ChiKhac.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupChiKhacXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double Tong = (table.Rows[i]["Tong"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["Tong"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tong.ToString("N0").Replace(",", ".") + "</td>";
                        html += "       <td rowspan=" + table.Rows[i]["Count"].ToString() + " style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["Count"].ToString() + "</td>";
                        double TT = (table.Rows[i]["TT"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TT"].ToString()));
                        html += "       <td rowspan=" + table.Rows[i]["Count"].ToString() + " style='text-align:center;vertical-align: inherit;'>" + TT.ToString("N0").Replace(",", ".") + "</td>";
                        html += "       </tr>";
                        flag = StaticData.ConvertMMDDYYtoDDMMYY(table.Rows[i]["NgayLap"].ToString());
                    }
                    else
                    {
                        if (int.Parse(table.Rows[i]["RowNumber"].ToString()) % 2 == 0)
                            html += "       <tr style='background-color:white'>";
                        else
                            html += "       <tr style='background-color:rgba(37, 170, 226, 0.1)'>";

                        html += "       <td  style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["HoTen"].ToString() + "</td>";
                        html += "       <td  style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["SoDienThoai"].ToString() + "</td>";


                        double CuocHangGui = (table.Rows[i]["CuocHangGui"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangGui"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangGui.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupHangGuiXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";
          
                        html +="    </td>";

                        double CuocHangNhan = (table.Rows[i]["CuocHangNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CuocHangNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CuocHangNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupHangNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double ThuNo = (table.Rows[i]["ThuNo"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ThuNo"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + ThuNo.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupThuNoXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CPNTra = (table.Rows[i]["CPNTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNTra"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNTra.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCPNTraXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CPNNhan = (table.Rows[i]["CPNNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CPNNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CPNNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCPNNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CODNhan = (table.Rows[i]["CODNhan"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODNhan"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODNhan.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCODNhanXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";

                        double CODTra = (table.Rows[i]["CODTra"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["CODTra"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + CODTra.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupCODTraGuiXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";
                        double ChiKhac = (table.Rows[i]["ChiKhac"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["ChiKhac"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + ChiKhac.ToString("N0").Replace(",", ".") + "<br>";
                        html += "           <a href='#' onclick='LoadPopupChiKhacXem(\"" + idNguoiDung + "\",\"" + table.Rows[i]["NgayLap"].ToString() + "\")'><i class='fa fa-eye' title='Xem'></i> Xem</a>";

                        html += "    </td>";
                        double Tong = (table.Rows[i]["Tong"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["Tong"].ToString()));
                        html += "       <td style='text-align:center;vertical-align: inherit;'>" + Tong.ToString("N0").Replace(",", ".") + "</td>";
                        double TT = (table.Rows[i]["TT"].ToString() == "" ? 0 : double.Parse(table.Rows[i]["TT"].ToString()));
                   //     html += "       <td  style='text-align:center;vertical-align: inherit;'>" + TT.ToString("N0").Replace(",", ".") + "</td>";
                        html += "  </tr>";
                    }
                }
            }
            html += "  </table><table >   <tr>";
            html += "       <td colspan='17' class='footertable'>";
            string url = "ThongKeDoanhThu.aspx?";
            //if (sMaDonHang != "")
            //    url += "MaNhapHang=" + sMaDonHang + "&";
            //if (sTuNgay != "")
            //    url += "TuNgay=" + sTuNgay + "&";
            //if (sDenNgay != "")
            //    url += "DenNgay=" + sDenNgay + "&";
            url += "Page=";
            html += StaticData.PhanTrang(url, txtFistPage, txtPage1, txtPage2, txtPage3, txtPage4, txtPage5, txtLastPage, Page);
            html += "    </td></tr><tr><td colspan='17'>&nbsp;</td></tr>";
            html += "     </table></center>";
        }
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng số hóa đơn:</b> " + SoHoaDon.ToString() + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền hàng:</b> " + TongTienHang.ToString("#,##").Replace(",", ".") + "</div>";
        //html += "<div style='text-align:right;font-size: 20px;'><b>Tổng tiền cước:</b> " + TongTienCuoc.ToString("#,##").Replace(",", ".") + "</div>";
        dvThongKeNguoiGui.InnerHtml = html;
    }
    protected void btTimKiem_Click(object sender, EventArgs e)
    {
        string TuNgay = txtTuNgay.Value.Trim();
        string DenNgay = txtDenNgay.Value.Trim();
        string url = "ThongKeDoanhThu.aspx?";
        if (TuNgay != "")
            url += "TuNgay=" + TuNgay + "&";
        if (DenNgay != "")
            url += "DenNgay=" + DenNgay + "";
        Response.Redirect(url);
    }
    protected void btXemTatCa_Click(object sender, EventArgs e)
    {
        string url = "ThongKeDoanhThu.aspx";
        Response.Redirect(url);
    }
    protected void Reload_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ThongKe/ThongKeDoanhThu.aspx");
    }
    private void ExportToExcel(DataTable dt)
    {
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt, "ThongKeDoanhThu");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ThongKeDonHang.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btXuatExcel_Click(object sender, EventArgs e)
    {
//        string sql = @"select tb1.RowNumber 'STT', tb_KhachHang.TenKhachHang 'NGƯỜI GỬI / KHÁCH HÀNG',tb1.SoDonHang 'SỐ ĐƠN HÀNG',PARSENAME(convert(varchar,convert(money,isnull(tb1.TongTien,0)),1),2 ) 'TỔNG TIỀN' from
//            (
//	             SELECT ROW_NUMBER() OVER
//                  (
//                        ORDER BY TongTien desc
//                  )AS RowNumber, *
//				  FROM (
//				  SELECT
//	              	COUNT(idKhachHang) AS SoDonHang,
//				  ISNULL(SUM(TienHang + TienCuoc + PhuPhi),0) AS TongTien,		  
//				  idKhachHang
//                  FROM tb_DonHang
//				  GROUP BY idKhachHang
//					) AS TB WHERE 1 = 1) as tb1,tb_KhachHang where tb1.idKhachHang = tb_KhachHang.idKhachHang";
        string sql = "";
        sql += @"select *, 
'TT' = Sum(Tong) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
 from ( select *
, 'Tong' = CuocHangGui + CuocHangNhan + ThuNo - CPNTra + CPNNhan + CODNhan - CODTra - ChiKhac
      ,'Count' = Count(NgayLap) OVER(PARTITION BY CONVERT(date, NgayLap, 111) ORDER BY RowNumber)
	   from ( SELECT DENSE_RANK() OVER (ORDER BY CONVERT(date, NgayLap, 111) DESC) AS RowNumber
                    ,* FROM (
				      select *,
    'CuocHangGui' = (select ISNULL(sum(ThanhToan),0) from tb_DonHang dh where dh.idNguoiDung = nd.idNguoiDung and NgayLap = tb1.NgayLap),
   'CuocHangNhan' = (select ISNULL(sum(TienTraHang),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   'ThuNo' = (select ISNULL(sum(SoTien),0)  from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra= tb1.NgayLap ),
   
   'CPNTra' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
   'CPNNhan' = (select ISNULL(sum(ChuyenPhatNhanh),0) from tb_DonHang where idNguoiDung = nd.idNguoiDung and NgayLap  = tb1.NgayLap),
   'CODNhan' = (select ISNULL(sum(ThanhToanCOD),0) from tb_TraNoKhachHang where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
     'CODTra' = (select ISNULL(sum(SoTien),0) from tb_TraNoCOD where idNguoiDung = nd.idNguoiDung and NgayTra  = tb1.NgayLap),
	      'ChiKhac' = (select ISNULL(sum(SoTien),0) from tb_ChiKhac where idNguoiDung = nd.idNguoiDung and NgayChi  = tb1.NgayLap)
   from tb_NguoiDung nd , (select NgayLap from tb_DonHang union select NgayTra from tb_TraNoKhachHang union select NgayTra from tb_TraNoCOD) as tb1 
                ) AS TB WHERE 1 = 1 and MaQuyen = 'NVVP'  ";
        if (mQuyen.ToUpper() != "ADMIN")
        {
            sql += "and TB.idNguoiDung = ' " + mIdNguoiDung + "'";
        }
        if (sTuNgay != "")
            sql += " and NgayLap >= '" + StaticData.ConvertDDMMtoMMDD(sTuNgay) + " 00:00:00'";
        if (sDenNgay != "")
            sql += " and NgayLap <= '" + StaticData.ConvertDDMMtoMMDD(sDenNgay) + " 00:00:00'";
        //if (sNguoiGui != "")
        //    sql += " and idKhachHang in (select idKhachHang from tb_KhachHang where TenKhachHang like N'%" + sNguoiGui + "%')";

        sql += ") as tb2 ) as tb3 WHERE RowNumber BETWEEN (" + Page + " - 1) * " + PageSize + " + 1 AND (((" + Page + " - 1) * " + PageSize + " + 1) + " + PageSize + ") - 1  order by RowNumber ";


        DataTable table = Connect.GetTable(sql);
        ExportToExcel(table);

       
    }
}