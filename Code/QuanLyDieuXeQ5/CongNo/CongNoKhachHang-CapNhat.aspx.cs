using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuanLyDonHang_QuanLyDonHang_CapNhat : System.Web.UI.Page
{
    string sIdDonHang = "";
    string Page = "";
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdNguoiDung = "";
    string mIdKhachHang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
        {
            mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
            mIdNguoiDung = StaticData.getField("tb_NguoiDung", "idNguoiDung", "TenDangNhap", mTenDangNhap);
            mIdKhachHang = StaticData.getField("tb_KhachHang", "idKhachHang", "TenDangNhap", mTenDangNhap);
            mQuyen = MyStaticData.GetMaQuyen(mTenDangNhap);
            //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVGN" && mQuyen.ToUpper() != "NVVP" && mQuyen.ToUpper() != "KH" )
            //{
            //    Response.Redirect("../Home/DangNhap.aspx");
            //}
        }
        try
        {
            sIdDonHang = StaticData.ValidParameter(Request.QueryString["idKhachHang"].Trim());
        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
            txtNgayLap.Value = DateTime.Now.ToString("dd/MM/yyyy");
            //txtNgayLap.Disabled = true;
            //if (mQuyen.ToUpper() == "KH")
            //{
            //    hdIdKhachHang.Value = mIdKhachHang;
            //    txtTenKhachHang.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", mIdKhachHang);
            //    txtTenKhachHang.Disabled = true;
            //    txtMaDonHang.Value = mIdKhachHang + MyStaticData.TaoMaDonHang();
            //}
            //txtMaDonHang.Value = MyStaticData.TaoMaDonHang();
            LoadMaPhieuThuNo();
            LoadKhachHang();
            LoadNoKhachHang();
            LoadThongTinDonHang();
            //txtMaPhieuTra.Value = "PTKH-00002";
            //txtTenKhachHang.Value = "Nguyễn Văn A";
            //txtSoTienNo.Value = "40.000";
            //txtConLai.Value = "40.000";
        }
    }

    private void LoadMaPhieuThuNo()
    {
        string MaPhieuThuNo = "";
        string sql = "select isnull(max(IDTraNoKhachHang),0)+1 as 'MaPhieuThuNo' from tb_TraNoKhachHang";
        DataTable table = Connect.GetTable(sql);
        MaPhieuThuNo = table.Rows[0]["MaPhieuThuNo"].ToString();

        // txtMaDonHang.DataSource = Connect.GetTable(sql);
        txtMaPhieuTra.Value = "MaPhieuTN" + MaPhieuThuNo + "";
    }
    private void LoadKhachHang()
    {

        string strsql = " select * from tb_KhachHang where IDKhachHang='" + sIdDonHang + "'";
        DataTable tbkh = Connect.GetTable(strsql);
        if (tbkh.Rows.Count > 0)
        {
            txtTenKhachHang.Value = tbkh.Rows[0]["TenKhachHang"].ToString();
        }
        double TongNo = 0;
        double ConLai = 0;
        string sqltongtien = @"select sum(TongCuoc) as NoKhachHang from tb_DonHang where  idKhachHang ='" + sIdDonHang + "'";
        DataTable tabletongtien = Connect.GetTable(sqltongtien);
        double TongTien = double.Parse(tabletongtien.Rows[0]["NoKhachHang"].ToString());
       
               
       
       


        string sqltientra = @"select * from tb_TraNoKhachHang where IDKhachHang='" + sIdDonHang + "'";
        DataTable tabletientra = Connect.GetTable(sqltientra);
        double Tientra = 0;
        if (tabletientra.Rows.Count > 0)
        {
            for (int j = 0; j < tabletientra.Rows.Count; j++)
            {
                double Tien = (tabletientra.Rows[j]["SoTien"].ToString() == "" ? 0 : double.Parse(tabletientra.Rows[j]["SoTien"].ToString())); 

                Tientra += Tien;
            }
        }
     
        ConLai = TongTien - Tientra;
        txtSoTienNo.Value = ConLai.ToString("N0").Replace(",", ".");

    }
    ////private void LoadTinh()
    ////{
    ////    string strSql = "select * from tb_Tinh";
    ////    slTinh.DataSource = Connect.GetTable(strSql);
    ////    slTinh.DataTextField = "TenTinh";
    ////    slTinh.DataValueField = "idTinh";
    ////    slTinh.DataBind();
    ////    slTinh.Items.Add(new ListItem("Chọn Tỉnh/TP", "0"));
    ////    slTinh.Items.FindByText("Chọn Tỉnh/TP").Selected = true;
    ////}

    //private void LoadKho()
    //{
    //    string strSql = "select * from tb_Kho";
    //    slKho.DataSource = Connect.GetTable(strSql);
    //    slKho.DataTextField = "TenKho";
    //    slKho.DataValueField = "idKho";
    //    slKho.DataBind();
    //    slKho.Items.Add(new ListItem("-- Chọn --", "0"));
    //    slKho.Items.FindByText("-- Chọn --").Selected = true;
    //}
    private void LoadThongTinDonHang()
    {
        //if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
        //{
        //    //ckbDaNhanTien.Disabled = true;
        //    //ckbDaNhanTien.Visible = false;
        //}
        //if (sIdDonHang != "")
        //{
        //    string sql = "select * from tb_DonHang where idDonHang='" + sIdDonHang + "'";
        //    DataTable table = Connect.GetTable(sql);
        //    if (table.Rows.Count > 0)
        //    {
        //        if (mQuyen.ToUpper() == "KH" && mIdKhachHang != table.Rows[0]["idKhachHang"].ToString())
        //        {
        //            Response.Redirect("../Home/DangNhap.aspx");
        //        }
        //        //dvTitle.InnerHtml = "SỬA THÔNG TIN ĐƠN HÀNG";
        //        //btLuu.Text = "SỬA";
        //        //txtMaDonHang.Value = table.Rows[0]["MaDonHang"].ToString();
        //        //txtNgayLap.Value = DateTime.Parse(table.Rows[0]["NgayLap"].ToString()).ToString("dd/MM/yyyy");
        //        //string TenKH = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
        //        //////txtNguoiGui.Value = TenKH;
        //        //txtTenKhachHang.Value = StaticData.getField("tb_KhachHang", "SoDienThoai", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
        //        //hdIdKhachHang.Value = table.Rows[0]["idKhachHang"].ToString();
        //        //btHuy.Text = "DSĐH CỦA " + TenKH.ToUpper();
        //        //slKho.Value = table.Rows[0]["idKho"].ToString();
        //        if (mQuyen.ToUpper() == "NVGN")
        //        {
        //            if (mIdNguoiDung != table.Rows[0]["idNguoiDung"].ToString())
        //            {
        //                Response.Redirect("../Home/DangNhap.aspx");
        //            }
        //        }
        //        else
        //        {

        //        }
        //        if (table.Rows[0]["ThoiDiemDuKienGiao"].ToString() != "")
        //        {
        //            ////txtNgayDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).ToString("dd/MM/yyyy");
        //            ////txtGioDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).Hour.ToString();
        //            ////txtPhutDuKienGiao.Value = DateTime.Parse(table.Rows[0]["ThoiDiemDuKienGiao"].ToString()).Minute.ToString();
        //        }
        //        ////slGoiDichVu.Value = table.Rows[0]["GoiDichVu"].ToString();
        //        ////txtThongTinBuuGui.Value = table.Rows[0]["ThongTinBuuGui"].ToString().Trim();
        //        ////if (table.Rows[0]["isDaNhanTien"].ToString() == "True")
        //        ////    ckbDaNhanTien.Checked = true;
        //        //dvButtonChiTietDonHang.InnerHtml = "<input id='btCapNhatChiTietDonHang' type='button' value='Thêm' onclick='CapNhatChiTietDonHang(\"THÊM\", \"" + table.Rows[0]["idDonHang"].ToString() + "\",\"\")' class='btn btn-primary btn-flat' />";
        //        //dvChiTietDonHang.Style.Add("display", "block");
        //        //txtTenKhachHang.Disabled = true;
        //        ////if (table.Rows[0]["NguoiNhanTra"].ToString() == "True")
        //        ////    radiNguoiNhanTra.Checked = true;
        //        //if (mQuyen.ToUpper() == "NVGN")
        //        //{
        //        //    txtMaDonHang.Disabled = true;
        //        //    txtGhiChu.Disabled = true;
        //        //}
                
        //    }
        //}
        //else
        //{
        //    if (mQuyen.ToUpper() == "NVGN")
        //    {
        //        //slNhanVienGiao.Value = mIdNguoiDung;
        //        //slNhanVienGiao.Disabled = true;
        //        Response.Redirect("../Home/DangNhap.aspx");
        //    }
        //}
    }
    protected void LoadNoKhachHang()
    {
        string sql2 = "select * from tb_TraNoKhachHang where IDTraNoKhachHang='" + sIdDonHang + "'";

        DataTable data = Connect.GetTable(sql2);
       
        
            string html = @"<center><table class='table table-bordered table-hover dataTable'>
                            <tr style='white-space: nowrap;'>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    STT
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Mã phiếu Xuất
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    Ngày Xuất Bán
                                </th>

             
                               

             
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Khách Hàng
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Loại Thanh Toán
                                </th>
                                 <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   Tổng Tiền
                                 </th>
                                    <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                   ThanhToan
                                 </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    In phiếu
                                </th>
                                <th class='th' style='background-color:#1c9c13;font-size:15px;color:white;'>
                                    
                                </th>
                                
                            </tr>";
            for (int i = 0; i < data.Rows.Count; i++)
            {

                html += "       <tr>";
               //// html += "       <td style='text-align:center;vertical-align: inherit;'>" + (((Page - 1) * PageSize) + i + 1).ToString() + "</td>";
               // html += "       <td style='text-align:center;vertical-align: inherit;'>" + table.Rows[i]["MaXuatBan"].ToString() + "</td>";
               // DateTime NgayThanhToan = DateTime.Parse(table.Rows[i]["NgayXuatBan"].ToString());
               // html += "       <td style='text-align:center;vertical-align: inherit;'>" + NgayThanhToan.ToString("dd/MM/yyyy") + "</td>";
               // //html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_Kho", "TenKho", "IDKho", table.Rows[i]["IDKho"].ToString()) + "</td>";

               // html += "       <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_KhachHang", "TenKhachHang", "IDKhachHang", table.Rows[i]["IDKhachHang"].ToString()) + "</td>";
            }
        
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaPhieuTra = "";
        string TenKhachHang = "";
        string NgayTra = "";
        string SoTien = "";
        string IDKhachHang = "";
        int temp = 0;

      if (txtMaPhieuTra.Value.Trim() != "")
      {
          if (sIdDonHang != "")
          {
              string sqlCheckDH = "select top 1 IDTraNoKhachHang from tb_TraNoKhachHang where MaPhieuTra='" + txtMaPhieuTra.Value.Trim() + "'";
              DataTable tbCheckDH = Connect.GetTable(sqlCheckDH);
              if (tbCheckDH.Rows.Count > 0)
              {
                  Response.Write("<script>alert('Mã đơn hàng đã tồn tại!')</script>");
                  return;
              }
              else
              {
                  MaPhieuTra = StaticData.ValidParameter(txtMaPhieuTra.Value.Trim());
                  temp = 0;
              }
          }
          else
          {
              Response.Write("<script>alert('Bạn không được phép trả nợ!')</script>");
              temp = 1;
          }
         
         
      }
       
      TenKhachHang = StaticData.ValidParameter(txtTenKhachHang.Value.Trim());
      string sqlCheckID = "select top 1 IDKhachHang from tb_KhachHang where TenKhachHang=N'" + TenKhachHang + "'";
      DataTable tbCheckID = Connect.GetTable(sqlCheckID);
      if (tbCheckID.Rows.Count > 0)
          IDKhachHang = tbCheckID.Rows[0]["IDKhachHang"].ToString();
      NgayTra = StaticData.ConvertDDMMtoMMDD(txtNgayLap.Value.Trim());
      SoTien = StaticData.ValidParameter(txtSoTienTra.Value.Trim().Replace(",", "").Replace(".", ""));
    if(temp == 0)
    {
        // đang làm tới đây.
        string sqlMaHoaDonLasest = "SELECT TOP 1 IDTraNoKhachHang FROM tb_TraNoKhachHang ORDER BY IDTraNoKhachHang DESC";
        DataTable tbl = Connect.GetTable(sqlMaHoaDonLasest);
        string sql = @"INSERT INTO tb_TraNoKhachHang(IDKhachHang,MaPhieuTra,SoTien,NgayTra,idNguoiDung)
                                     VALUES (N'" + IDKhachHang
                                           + "',N'" + MaPhieuTra.Trim()
                                            + "',N'" + SoTien.Trim()

                                          
                                           + "',N'" + NgayTra
                                                   + "',N'" + mIdNguoiDung
                                           + "')";
        bool chksql = Connect.Exec(sql);
    }
    Response.Redirect("CongNoKhachHang.aspx?");
       
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
            Response.Redirect("CongNoKhachHang.aspx");
    }


    [WebMethod]
    public static string LoadThongTinNguoiNhan(string SoDT)
    {
        string kq = "|~~|";
        string sql = @"SELECT TOP 1 * FROM [tb_DonHang] WHERE SoDienThoaiNguoiNhan = '" +SoDT + "'";
        DataTable tb = Connect.GetTable(sql);
        if(tb.Rows.Count > 0)
        {
            kq = tb.Rows[0]["NguoiNhan"].ToString() + "|~~|" + tb.Rows[0]["DiaChiNguoiNhan"].ToString();
        }

        return kq;
       
    }


}