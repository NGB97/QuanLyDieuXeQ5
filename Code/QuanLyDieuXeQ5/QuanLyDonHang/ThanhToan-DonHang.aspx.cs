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
            sIdDonHang = StaticData.ValidParameter(Request.QueryString["idDonHang"].Trim());

        }
        catch { }
        try
        {
            Page = StaticData.ValidParameter(Request.QueryString["Page"].Trim());
        }
        catch { }
        if (!IsPostBack)
        {
          
          //  LoadTinh();
            
            txtNgayThanhToan.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtNgayThanhToan.Disabled = true;
            LoadLoaiCuoc();
            LoadLoaiSanPham1();
            //LoadKho();
            LoadNhanVienGiao();
          
            LoadTinhTrang();
            if (mQuyen.ToUpper() == "KH")
            {
                //hdIdKhachHang.Value = mIdKhachHang;
                //txtNguoiNhan.Value = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", mIdKhachHang);
                //txtNguoiNhan.Disabled = true;
             //   txtMaDonHang.Value = mIdKhachHang + MyStaticData.TaoMaDonHang();

                //slNhanVienGiao.Disabled = true;
                //txtNgayDuKienGiao.Disabled = true;
                //txtGioDuKienGiao.Disabled = true;
                //txtPhutDuKienGiao.Disabled = true;
               
                //slKho.Disabled = true;
                //slLoaiCuoc.Disabled = true;
                //slTinhTrangDonHang.Disabled = true;
                //slTinhTrangDonHang.Value = "CXL";
            }
        //    LoadMaDonHang();
           // txtMaDonHang.Value = MyStaticData.TaoMaDonHang();
            LoadThongTinDonHang();

        }
    }

   private void LoadTenHangHoa()
    {

    }
    //private void LoadMaDonHang()
    //{
    //    string MaKhachHang = "";
    //    // string _Ngay = "";
    //    string sql = "select isnull(max(IDDonHang),0)+1 as 'MaKhachHang' from tb_DonHang";
    //    DataTable table = Connect.GetTable(sql);
    //    MaKhachHang = table.Rows[0]["MaKhachHang"].ToString();
    //    //_Ngay = DateTime.Now.ToString("yyyy-MM-dd");
    //    string[] ngay = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
    //    string MaDonHang = ngay[0] + ngay[1] + ngay[2];
    //    // txtMaDonHang.DataSource = Connect.GetTable(sql);
    //    txtMaDonHang.Value = MaDonHang + "00" + MaKhachHang + "";
    //}

    //private void LoadTinh()
    //{
    //    string strSql = "select * from tb_Tinh";
    //    slTinh.DataSource = Connect.GetTable(strSql);
    //    slTinh.DataTextField = "TenTinh";
    //    slTinh.DataValueField = "idTinh";
    //    slTinh.DataBind();
    //    slTinh.Items.Add(new ListItem("Chọn Tỉnh/TP", "0"));
    //    slTinh.Items.FindByText("Chọn Tỉnh/TP").Selected = true;
    //}
    //private void LoadHuyen(string idTinh)
    //{
    //    string strSql = "select * from tb_Huyen where idTinh='" + idTinh + "'";
    //    slHuyen.DataSource = Connect.GetTable(strSql);
    //    slHuyen.DataTextField = "TenHuyen";
    //    slHuyen.DataValueField = "idHuyen";
    //    slHuyen.DataBind();
    //    slHuyen.Items.Add(new ListItem("Chọn Quận/Huyện", "0"));
    //    slHuyen.Items.FindByText("Chọn Quận/Huyện").Selected = true;
    //}
    private void LoadLoaiSanPham1()
    {
        string strSql = "select * from tb_LoaiSanPham";
        slLoaiSanPham1.DataSource = Connect.GetTable(strSql);
        slLoaiSanPham1.DataTextField = "TenLoaiSanPham";
        slLoaiSanPham1.DataValueField = "idLoaiSanPham";
        slLoaiSanPham1.DataBind();
        slLoaiSanPham1.Items.Add(new ListItem("-- Chọn --", "0"));
        slLoaiSanPham1.Items.FindByText("-- Chọn --").Selected = true;
    }
    private void LoadLoaiCuoc()
    {
        //string strSql = "select * from tb_LoaiCuoc";
        //slLoaiCuoc.DataSource = Connect.GetTable(strSql);
        //slLoaiCuoc.DataTextField = "TenLoaiCuoc";
        //slLoaiCuoc.DataValueField = "MaLoaiCuoc";
        //slLoaiCuoc.DataBind();
        //slLoaiCuoc.Items.Add(new ListItem("Loại cước", "0"));
        //slLoaiCuoc.Items.FindByText("Loại cước").Selected = true;
    }
    private void LoadTinhTrang()
    {
        //string strSql = "select * from tb_TinhTrang";
        //slTinhTrangDonHang.DataSource = Connect.GetTable(strSql);
        //slTinhTrangDonHang.DataTextField = "TenTinhTrang";
        //slTinhTrangDonHang.DataValueField = "MaTinhTrang";
        //slTinhTrangDonHang.DataBind();
        //slTinhTrangDonHang.Items.Add(new ListItem("-- Chọn --", "0"));
        //slTinhTrangDonHang.Items.FindByText("-- Chọn --").Selected = true;
    }

    private void LoadNhanVienGiao()
    {
        //string strSql = "select * from tb_NguoiDung where MaQuyen='NVGN'";
        //slNhanVienGiao.DataSource = Connect.GetTable(strSql);
        //slNhanVienGiao.DataTextField = "HoTen";
        //slNhanVienGiao.DataValueField = "idNguoiDung";
        //slNhanVienGiao.DataBind();
        //slNhanVienGiao.Items.Add(new ListItem("-- Chọn --", "0"));
        //slNhanVienGiao.Items.FindByText("-- Chọn --").Selected = true;
    }

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
        if (mQuyen.ToUpper() != "ADMIN" && mQuyen.ToUpper() != "NVVP")
        {
         //   ckbDaNhanTien.Disabled = true;
          //  ckbDaNhanTien.Visible = false;
        }
        if (sIdDonHang != "")
        {
            LoadMaPhieuThuNo();
            string sql = "select * from tb_DonHang dh,tb_ChiTietDonHang ct where dh.idDonHang=ct.idDonHang and  dh.idDonHang='" + sIdDonHang + "'";
            DataTable table = Connect.GetTable(sql);
            if (table.Rows.Count > 0)
            {
                if (mQuyen.ToUpper() == "KH" && mIdKhachHang != table.Rows[0]["idKhachHang"].ToString())
                {
                    Response.Redirect("../Home/DangNhap.aspx");
                }
                dvTitle.InnerHtml = "CẬP NHẬT GIAO HÀNG CHO KHÁCH";
                btLuu.Text = "LƯU";

                //if (table.Rows[0]["MaTinhTrang"].ToString() == "DHT")
                //{
                //    Response.Write("<script>alert('Đơn hàng đã hoàn thành! Bạn KHÔNG THỂ TRẢ HÀNG')</script>");
                //    {
                //        Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
                //    }
                //}
                txtMaDonHang1.Value = table.Rows[0]["MaDonHang"].ToString();
          //      txtNgayLap.Value = DateTime.Parse(table.Rows[0]["NgayLap"].ToString()).ToString("dd/MM/yyyy");

                string TenKH = StaticData.getField("tb_KhachHang", "TenKhachHang", "idKhachHang", table.Rows[0]["idKhachHang"].ToString());
                txtTenKhachHang.Value = TenKH.ToString();
                hdIdKhachHang.Value = table.Rows[0]["idKhachHang"].ToString();
                txtNgayThanhToan.Value = DateTime.Now.ToString("dd/MM/yyyy");
                txtNgayThanhToan.Disabled = true;
                //btHuy.Text = "DSĐH CỦA " + TenKH.ToUpper();

                string NguoiNhanTra = table.Rows[0]["NguoiNhanTra"].ToString();
                double TienCuoc = 0;
                if (NguoiNhanTra == "True")
                {
                   TienCuoc = table.Rows[0]["TongCuoc"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["TongCuoc"].ToString());
                    
                }
                else
                {
                    TienCuoc = 0;
                    txtSoTienTra.Disabled = true;


                }
                txtTongCuoc.Value = TienCuoc.ToString("N0").Replace(",", ".");
                double CPN = table.Rows[0]["ChuyenPhatNhanh"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["ChuyenPhatNhanh"].ToString());

                double COD = table.Rows[0]["PhiCOD"].ToString() == "" ? 0 : double.Parse(table.Rows[0]["PhiCOD"].ToString());
                if(COD == 0)
                {
                    TienCOD.Visible = false;
                    txt_TienCOD.Value = COD.ToString("N0");

                }
                else
                {
                    txt_TienCOD.Value = COD.ToString("N0").Replace(",", ".");
                }
               // txtTongTien.Value = TienCuoc.ToString("N0").Replace(",", ".");
                txt_ChuyenPhatNhanh.Value = CPN.ToString("N0").Replace(",", ".");
                string sql1 = "select * from tb_ChiTietDonHang where IDDonHang ='" + sIdDonHang + "'";

                DataTable data = Connect.GetTable(sql1);
                //SetPage();
                loadSP(data);
                
            }
        }
        else
        {
            if (mQuyen.ToUpper() == "NVGN")
            {
                //slNhanVienGiao.Value = mIdNguoiDung;
                //slNhanVienGiao.Disabled = true;
                Response.Redirect("../Home/DangNhap.aspx");
            }
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

    protected void loadSP(DataTable data)
    {
        if (data.Rows.Count > 0)
        {
            string slistSP = "";

            string html = "";
            double TongTien = 0;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string DonGia = "";
                string TenHH = "";

                string IDHangHoa = data.Rows[i]["IDHangHoa"].ToString();
              
                string sql = "select * from tb_HangHoa where IDHangHoa = '" + IDHangHoa + "'";
                DataTable tb = Connect.GetTable(sql);
                if (tb.Rows.Count > 0)
                {
                    TenHH = tb.Rows[0]["TenHangHoa"].ToString();
                    DonGia = tb.Rows[0]["GiaCuoc"].ToString();
                }

                slistSP += IDHangHoa + "-" + data.Rows[i]["SoLuong"] + "-" + data.Rows[i]["MaTinhTrang"] + "-" + DonGia;
                html += "<tr id='tr_" + IDHangHoa + "'>";
                html += "<td>" + (i + 1) + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + TenHH + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + data.Rows[i]["SoLuong"] + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + DonGia + "</td>";
                html += "     <td style='text-align:center;vertical-align: inherit;'>" + StaticData.getField("tb_TinhTrang", "TenTinhTrang", "MaTinhTrang", data.Rows[i]["MaTinhTrang"].ToString()) + "</td>"; 
                //html += "       <td style='text-align:center'>" + data.Rows[i]["DonViTinh"] + "</td>";
              
                //html += "<td style='text-align:center'><a onclick=''><img class='imgedit' id='DeleteSP_" + data.Rows[i]["Id"] + "' src='../Images/delete.png'/></a></td>";
                html += "   <td style='text-align: center'><a style='cursor:pointer' onclick='XoaSanPham(\"" + IDHangHoa + "\")'><i class='fa fa-trash'></i></a></td>";
                //html += "       <td style='text-align:center'>" + data.Rows[i]["HangGoi"] + "</td>";
                if (i < data.Rows.Count - 1)
                    slistSP += ",";


                html += "</tr>";
                double ThanhTien = double.Parse(tb.Rows[0]["GiaCuoc"].ToString());
                TongTien += ThanhTien;

            }
            // txtTongTien.Value = TongTien.ToString();
           // txtTongTien.Value = TongTien.ToString("##,0").Replace(",", ".");

            //txtCKTien.Value = ((TongTien * ChietKhau) / 100).ToString();
       //     listSanPham.Value = slistSP;
         //   danhSachSPChon.InnerHtml = html;
        }
    }
    protected void btLuu_Click(object sender, EventArgs e)
    {
        string MaDonHang = "";
        string NgayLap = "";
        string MaPhieuTra = "";
        string ThanhToan = "";
        string SDTNguoiGui = "";
        string Nguoigui = "";
        //string idKho = "";
        string idChiNhanhGui = "";

       // string NguoiNhan = "";
       // string DiaChiNguoiNhan = "";
       // string SoDienThoaiNguoiNhan = "";
        string idChiNhanhNhan = "";
        string idKhachHang = "";
        string TongCuoc = "";
        string PhiCOD = "";

        string ChuyenPhatNhanh = "";
        string MaTinhTrang = "";
        string GhiChu = "";

    
 
            MaDonHang = StaticData.ValidParameter(txtMaDonHang1.Value.Trim());
            NgayLap = StaticData.ConvertDDMMtoMMDD(txtNgayThanhToan.Value.Trim());
            idKhachHang = hdIdKhachHang.Value.Trim();
            MaPhieuTra = txtMaPhieuTra.Value.Trim();
            ThanhToan = txtSoTienTra.Value.Trim().Replace(".", "");
            string CPN = txt_ChuyenPhatNhanh.Value.Trim().Replace(".", "");
     
     //   idChiNhanhGui = hdIDChiNhanhGui.Value.Trim();
///
     //   SDTNguoiGui = txtDienThoaiNguoiGui.Value.Trim();
     //   Nguoigui = txtNguoiGui.Value.Trim();
     //   PhiCOD = txtPhiCOD.Value.Trim().Replace(".", "");
        
       // DiaChiNguoiNhan = txtDiaChiNguoiNhan.Value.Trim();
 //       idChiNhanhNhan = hdIDChiNhanhNhan.Value.Trim();


        //Khách hàng

            string ThanhToanCOD = txt_TienCOD.Value.Trim().Replace(".", "");
        TongCuoc = txtSoTienTra.Value.Trim().Replace(".", "");

      
        
      
        //////////
        if (sIdDonHang != "")
        {
           // string MaDonHang1 = MyStaticData.TaoMaDonHang();
            string sqlInsertDH = "insert into tb_TraNoKhachHang(idKhachHang,MaPhieuTra,TienTraHang,NgayTra,idNguoiDung,ChuyenPhatNhanh,ThanhToanCOD)";
            sqlInsertDH += " values('" + idKhachHang + "','" + MaPhieuTra + "','" + ThanhToan + "',N'" + NgayLap + "',N'" + mIdNguoiDung + "',N'" + CPN + "',N'" + ThanhToanCOD + "'";
         
            
            sqlInsertDH += ")";
            bool ktInsertKH = Connect.Exec(sqlInsertDH);
            if (ktInsertKH)
            {
               if(CPN != "0")
               {
                   string sqlUpdateDH = "";
                   sqlUpdateDH += "update tb_DonHang set";
                   sqlUpdateDH += " MaTinhTrangCPN = 'DHT'";
                   sqlUpdateDH += " where idDonHang ='" + sIdDonHang + "'";
                   bool ktUpdateKH = Connect.Exec(sqlUpdateDH);
               }
                else
               {

                    string sqlupdate = @"select * from tb_ChiTietDonHang where idDonHang = '" + sIdDonHang + "'";
                    DataTable tb = Connect.GetTable(sqlupdate);
                    for (int i = 0; i < tb.Rows.Count; i++ )
                    {
                        string idCTDonHang = tb.Rows[i]["idChiTietDonHang"].ToString();
                        string sqlUpdateDH = "";
                              sqlUpdateDH += "update tb_ChiTietDonHang set";
                              sqlUpdateDH += " MaTinhTrang = 'DHT'";
                              sqlUpdateDH += " where idChiTietDonHang ='" + idCTDonHang + "'";
                         bool ktUpdateKH = Connect.Exec(sqlUpdateDH);
                       //  if (ktUpdateKH)
                    }
               }
                Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
               // string idDonHang = StaticData.getField("tb_DonHang", "idDonHang", "MaDonHang", MaDonHang);
               // //Response.Redirect("QuanLyDonHang-CapNhat.aspx?idDonHang=" + idDonHang);
               //// Response.Redirect("QuanLyDonHang.aspx" + (Page == "" ? "" : "?Page=" + Page));
               // if (ChuyenPhatNhanh == "")
               // {
               //   //  SaveOrderDetail(arraySPList, idDonHang);
               // }
              
            }
            else
            {
                Response.Write("<script>alert('Lỗi thêm đơn hàng, bạn vui lòng kiểm tra lại dữ liệu nhập!')</script>");
            }

        }
        else
        {
            
//            string sqlUpdateDH = "";
//            sqlUpdateDH += "update tb_DonHang set";
//            sqlUpdateDH += " NgayLap = '" + NgayLap + "'";
//            sqlUpdateDH += " ,MaDonHang = '" + MaDonHang + "'";
         
//            sqlUpdateDH += " ,SDTNguoiGui = '" + SDTNguoiGui + "'";
//            sqlUpdateDH += " ,NguoiGui = N'" + Nguoigui + "'";
//            sqlUpdateDH += " ,idChiNhanhGui = '" + idChiNhanhGui + "'";
       
           
       
           
//            //sqlUpdateDH += " ,DiaChiNguoiNhan = N'" + DiaChiNguoiNhan + "'";
//            //
//            sqlUpdateDH += " ,idKhachHang = '" + idKhachHang + "'";
//            sqlUpdateDH += " ,idChiNhanhNhan = '" + idChiNhanhNhan + "'";
//            sqlUpdateDH += " ,PhiCOD = '" + PhiCOD + "'";
   

        
           
//            sqlUpdateDH += " where idDonHang ='" + sIdDonHang + "'";
//            bool ktUpdateKH = Connect.Exec(sqlUpdateDH);
//            if (ktUpdateKH)
//            {
//                //if (Page != "")
//                //    Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
//                //else
//                //    Response.Redirect("QuanLyDonHang.aspx");
//                //SaveOrderDetail(arraySPList, sIdDonHang);
//                //Response.Write("<script>alert('Sửa thành công!')</script>");

//                string sqlDeleteCTSupervisor = @"DELETE FROM tb_ChiTietDonHang
//                                            WHERE idDonHang = '" + sIdDonHang + "'";

//                bool ExsqlDelete = Connect.Exec(sqlDeleteCTSupervisor);
//                if (ExsqlDelete)
//                {
//                  //  SaveOrderDetail(arraySPList, sIdDonHang);
//                }


//            }
//            else
//            {
//                Response.Write("<script>alert('Lỗi !')</script>");
//            }
        }
      
    }

    protected void SaveOrderDetail(string[] arraySPList, string idDonHang)
    {

        for (int i = 0; i < arraySPList.Length; i++)
        {
            string MaHangHoa = "";
            string[] arr = arraySPList[i].Split('-');
            string idHangHoa = arr[0];
            string Mahh = "";
            string sql = "select isnull(max(idChiTietDonHang),0)+1 as 'MaKhachHang' from tb_ChiTietDonHang";
            DataTable table = Connect.GetTable(sql);
            Mahh = table.Rows[0]["MaKhachHang"].ToString();
            string[] ngay = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
            string MaDonHang = ngay[0] + ngay[1] + ngay[2];
            // txtMaDonHang.DataSource = Connect.GetTable(sql);
            MaHangHoa = Mahh + "00" + MaDonHang + i +"";
            string sqlorderdetail = @"INSERT INTO tb_ChiTietDonHang(idDonHang,idHangHoa,SoLuong,MaTinhTrang,MaHangHoa,DonGia)
                                              VALUES ('" + idDonHang + "','" + idHangHoa + "','" + arr[1] + "',N'" + arr[2] + "',N'" + MaHangHoa + "',N'" + arr[3] + "')";
            bool Exsql = Connect.Exec(sqlorderdetail);


        }
        Response.Redirect("QuanLyDonHang.aspx?Page=" + Page);
    }
    protected void btHuy_Click(object sender, EventArgs e)
    {
        string idKhachHang = "";
        if (hdIdKhachHang.Value.Trim() != "")
            idKhachHang = hdIdKhachHang.Value.Trim();
        if(idKhachHang != "")
            Response.Redirect("QuanLyDonHang.aspx?");
        else
            Response.Redirect("QuanLyDonHang.aspx");
    }
    protected void slTinh_SelectedIndexChanged(object sender, EventArgs e)
    {
    //    LoadHuyen(slTinh.SelectedValue.Trim());
    }
    protected void slHuyen_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string sqlHuyen = "select * from tb_Huyen where idHuyen='" + slHuyen.SelectedValue.Trim() + "'";
        //DataTable tbHuyen = Connect.GetTable(sqlHuyen);
        //if(tbHuyen.Rows.Count>0)
        //{
        //    slNhanVienGiao.Value = tbHuyen.Rows[0]["idNguoiDung"].ToString();
        //    if (tbHuyen.Rows[0]["MaLoaiCuoc"].ToString() != "")
        //    {
        //        slLoaiCuoc.Value = tbHuyen.Rows[0]["MaLoaiCuoc"].ToString();
        //        string GiaCuoc = StaticData.getField("tb_LoaiCuoc","GiaCuoc","MaLoaiCuoc",tbHuyen.Rows[0]["MaLoaiCuoc"].ToString());
        //        txtTienCuoc.Value = (GiaCuoc == "" ? "" : double.Parse(GiaCuoc).ToString("N0").Replace(",", "."));
        //    }
        //    //else
        //    //    slLoaiCuoc.Value = "DiTinh";
        //    double TienHang = (txtTienHang.Value == "" ? 0 : double.Parse(txtTienHang.Value.Replace(".", "")));
        //    double TienCuoc = (txtTienCuoc.Value == "" ? 0 : double.Parse(txtTienCuoc.Value.Replace(".", "")));
        //    double PhuPhi = (txtPhuPhi.Value == "" ? 0 : double.Parse(txtPhuPhi.Value.Replace(".", "")));
        //    txtTongTien.Value = (TienHang + TienCuoc + PhuPhi).ToString("N0").Replace(",", ".");
        //}
    }


    //[WebMethod]
    //public static string LoadThongTinNguoiNhan(string SoDT)
    //{
    //    string kq = "|~~|";
    //    string sql = @"SELECT TOP 1 * FROM [tb_DonHang] WHERE SoDienThoaiNguoiNhan = '" +SoDT + "'";
    //    DataTable tb = Connect.GetTable(sql);
    //    if(tb.Rows.Count > 0)
    //    {
    //        kq = tb.Rows[0]["NguoiNhan"].ToString() + "|~~|" + tb.Rows[0]["DiaChiNguoiNhan"].ToString();
    //    }

    //    return kq;
       
    //}


}