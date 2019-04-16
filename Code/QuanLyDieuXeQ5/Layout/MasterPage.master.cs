using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Layout_MasterPage : System.Web.UI.MasterPage
{
    string mTenDangNhap = "";
    string mQuyen = "";
    string mIdKhachHang = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["QuanLyCongNoAnhKiet_Login"] != null && Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim() != "")
        {
            mTenDangNhap = Request.Cookies["QuanLyCongNoAnhKiet_Login"].Value.Trim();
            LoadThongTinNguoiDung();
        }
        else
        {
            Response.Redirect("../Home/DangNhap.aspx");
        }
        if (!IsPostBack)
        {
            LoadMenu();
        }
    }
    private void LoadThongTinNguoiDung()
    {
        string html = "";
         string sqlTTND = "";
         if (mTenDangNhap == "admin" || mTenDangNhap == "minhtam.cohai")
             sqlTTND = "select * from tb_NguoiDung where TenDangNhap='" + mTenDangNhap + "'";
        else
            sqlTTND = "select nd.*, TenChiNhanh from tb_NguoiDung nd, tb_ChiNhanh cn where  nd.ChiNhanh = cn.idChiNhanh and TenDangNhap = '" + mTenDangNhap + "'";
        DataTable tbTTND = Connect.GetTable(sqlTTND);
        if (tbTTND.Rows.Count > 0)
        {
            mQuyen = tbTTND.Rows[0]["MaQuyen"].ToString();

            if (mQuyen != "Admin")
                lbChiNhanh.InnerHtml = tbTTND.Rows[0]["TenChiNhanh"].ToString();
            else
                lbChiNhanh.InnerHtml = "QUẢN LÝ";
            lbTenDangNhap.InnerHtml = tbTTND.Rows[0]["HoTen"].ToString();
            html += "<img src='../dist/img/user2-160x160.jpg' class='img-circle' alt='User Image'>";
            html += "<p>" + tbTTND.Rows[0]["HoTen"].ToString() + "<small>";
            html += "Quyền: " + mQuyen + "</small></p>";
        }
        else
        {
            string sqlTTKH = "select * from tb_KhachHang where TenDangNhap='" + mTenDangNhap + "'";
            DataTable tbTTKH = Connect.GetTable(sqlTTKH);
            if (tbTTKH.Rows.Count > 0)
            {
                mQuyen = "KH";
                mIdKhachHang = tbTTKH.Rows[0]["idKhachHang"].ToString();
                lbTenDangNhap.InnerHtml = tbTTKH.Rows[0]["TenKhachHang"].ToString();
                html += "<img src='../dist/img/user2-160x160.jpg' class='img-circle' alt='User Image'>";
                html += "<p>" + tbTTKH.Rows[0]["TenKhachHang"].ToString() + "<small>";
                html += "Quyền: Khách hàng</small></p>";
            }
        }
       // dvTTND.InnerHtml = html;
    }
    private void LoadMenu()
    {
        string URL = HttpContext.Current.Request.Url.AbsoluteUri.ToUpper();
        string html = "";
        html += @"<a href='#' class='visible-phone'><i class='icon icon-align-justify' style='margin-top: -1px;'></i> <span style='font-size: 14px;'>DANH MỤC QUẢN LÝ</span></a><ul>

     ";
        /////////Danh mục
        if (mQuyen.ToUpper() == "ADMIN")
        {
            if (URL.Contains("/DANHMUC/"))
                html += "  <li id='dvDanhMuc' class='submenu activeMenu'>";
            else
                html += "  <li id='dvDanhMuc' class='submenu'>";
            html += @"<a href='#'><i class='fa fa-list FixIcon'></i> <span>Danh mục</span> </a>
              <ul class='treeview-menu'>";

            html += "<li ";
            if(URL.Contains(("/DanhMuc/DanhMucNguoiDung.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../DanhMuc/DanhMucNguoiDung.aspx'><i class='fa fa-users FixIcon'  style='margin-top: 7px; margin-right:7px' ></i>Danh mục người dùng</a></li>";
          
            html += "<li ";
            if (URL.Contains(("/DanhMuc/DanhMucChiNhanh.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../DanhMuc/DanhMucChiNhanh.aspx'><i class='fa fa-list FixIcon'  style='margin-top: 7px; margin-right:8px' ></i>Danh mục chi nhánh</a></li>";

            //html += "<li ";
            //if (URL.Contains(("/DanhMuc/DanhMucLoaiSanPham.aspx").ToUpper()))
            //{
            //    html += @" class='activeSubMenu'";
            //}
            //html += @" ><a href='../DanhMuc/DanhMucLoaiSanPham.aspx'><i class='fa fa-th-large FixIcon'  style='margin-top: 7px; margin-right:9px' ></i>D. mục loại sản phẩm</a></li>";

            //html += "<li ";
            //if (URL.Contains(("/DanhMuc/DanhMucKho.aspx").ToUpper()))
            //{
            //    html += @" class='activeSubMenu'";
            //}
            //html += @" ><a href='../DanhMuc/DanhMucKho.aspx'><i class='fa fa-cubes FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Danh mục kho</a></li>";
            
            html += "<li ";
            if (URL.Contains(("/DanhMuc/DanhMucHangHoa.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" ><a href='../DanhMuc/DanhMucHangHoa.aspx'><i class='fa fa-money FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Danh mục Hàng Hóa</a></li>";
            
            //
            html += "<li ";
            if (URL.Contains(("/DanhMuc/DanhMucKhachHang.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../DanhMuc/DanhMucKhachHang.aspx'><i class='fa fa-list FixIcon'  style='margin-top: 7px; margin-right:8px' ></i>Danh mục khách hàng</a></li>";

            ////
            //html += "<li ";
            //if (URL.Contains(("/DanhMuc/DanhMucXe.aspx").ToUpper()))
            //{
            //    html += @" class='activeSubMenu'";
            //}
            //html += @" ><a href='../DanhMuc/DanhMucXe.aspx'><i class='fa fa-money FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Danh mục Xe</a></li>";
            
            //
            html += "<li ";
            //if (URL.Contains(("/DanhMuc/DanhMucTinh.aspx").ToUpper()))
            //{
            //    html += @" class='activeSubMenu'";
            //}
            html += @" 
                </li>
              </ul>
           </li>";

//            html += "<li ";
//            if (URL.Contains(("/DanhMuc/DanhMucXe.aspx").ToUpper()))
//            {
//                html += @" class='activeSubMenu'";
//            }
//            html += @" ><a href='../DanhMuc/DanhMucXe.aspx'><i class='fa fa-server FixIcon'  style='margin-top: 7px; margin-right:8px' ></i></i>Danh mục Xe</a></li>
//              </ul>
//           </li>";
        }
        /////////
        /////////// Quản lý đơn hàng
        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/QUANLYDONHANG/"))
        //        html += "  <li id='dvQuanLyDonHang' class='activeMenu'>";
        //    else
        //        html += "  <li id='dvQuanLyDonHang' class=''>";
        //    if(mQuyen == "KH")
        //        html += "  <a href='../QuanLyDonHang/QuanLyDonHang.aspx?idKhachHang="+ mIdKhachHang +"'>";
        //    else
        //        html += "  <a href='../QuanLyDonHang/QuanLyDonHang.aspx'>";
        //    html += "    <i class='fa fa-file-text-o FixIcon'></i> <span>Quản lý đơn hàng</span>";
        //    html += "  </a>";
        //    html += "</li>";
        //}
        ///////// QUản lý dơn hàng
        if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        {
            if (URL.Contains("/QUANLYDONHANG/"))
                html += "  <li id='dvQuanLyDonHang' class='submenu activeMenu'>";
            else
                html += "  <li id='dvQuanLyDonHang' class='submenu'>";
            html += @"<a href='#'><i class='fa fa-file-text-o FixIcon'></i> <span>Quản Lý Đơn Hàng</span></a>
              <ul class='treeview-menu'>";

            html += "<li ";
            if (URL.Contains(("/QuanLyDonHang/QuanLyDonHang.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../QuanLyDonHang/QuanLyDonHang.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Nhận hàng</a></li>";

            html += "<li ";
            if (URL.Contains(("/QuanLyDonHang/QuanLyTraHang.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../QuanLyDonHang/QuanLyTraHang.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Trả Hàng</a></li>";

            html += "</ul>";
        }

        ///////// Vé Xe
        if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        {
            if (URL.Contains("/VeXe/"))
                html += "  <li id='dvVeXe' class='submenu activeMenu'>";
            else
                html += "  <li id='dvVeXe' class='submenu'>";
            html += @"<a href='#'><i class='fa fa-file-text-o FixIcon'></i> <span>Quản Lý Vé Xe</span></a>
              <ul class='treeview-menu'>";

            html += "<li ";
            if (URL.Contains(("/QuanLyVeXe/QuanLyNhanVe.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../QuanLyVeXe/QuanLyNhanVe.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Nhận Vé</a></li>";

            html += "<li ";
            if (URL.Contains(("/QuanLyVeXe/QuanLyVeXe.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../QuanLyVeXe/QuanLyVeXe.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Bán Vé</a></li>";

            html += "<li ";
            if (URL.Contains(("#").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='#'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>TK Doanh Thu Xe</a></li>";

            html += "</ul>";
        }


        /////Phân hàng xuống văn phòng

        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "TAIXE" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/QUANLYHANGTAIVANPHONG/"))
        //        html += "  <li id='dvQuanLyHangTaiVanPhong' class='activeMenu'>";
        //    else
        //        html += "  <li id='dvQuanLyHangTaiVanPhong' class=''>";
        //    if (mQuyen == "KH")
        //        html += "  <a href='../QuanLyHangTaiVanPhong/QuanLyHangTaiVanPhong.aspx?idKhachHang=" + mIdKhachHang + "'>";
        //    else
        //        html += "  <a href='../QuanLyHangTaiVanPhong/QuanLyHangTaiVanPhong.aspx'>";
        //    html += "    <i class='fa fa-file-text-o FixIcon'></i> <span>Phân hàng xuống văn phòng</span>";
        //    html += "  </a>";
        //    html += "</li>";
        //}


        /////////
        ///////// Quản lý chi tiêu
        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/QUANLYHANGTRENXE/"))
        //        html += "  <li id='dvQuanLyHangTrenXe' class='activeMenu'>";
        //    else
        //        html += "  <li id='dvQuanLyHangTrenXe' class=''>";
        //    html += "  <a href='../QuanLyHangTrenXe/QuanLyHangTrenXe.aspx'>";
        //    html += "    <i class='fa fa-credit-card-alt FixIcon'></i> <span>Quản lý hàng trên xe</span>";
        //    html += "  </a>";
        //    html += "</li>";
        //}

        // thanh toán
        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/THANHTOAN/"))
        //        html += "  <li id='dvTHANHTOAN' class='activeMenu'>";
        //    else
        //        html += "  <li id='dvQuanLyChiTieu' class=''>";
        //    html += "  <a href='../ThanhToan/ThanhToan.aspx'>";
        //    html += "    <i class='fa fa-credit-card-alt FixIcon'></i> <span>Thanh Toán</span>";
        //    html += "  </a>";
        //    html += "</li>";
        //}

        ///////////công nợ
        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP")
        // {
        //     if (URL.Contains("/CONGNO/"))
        //         html += "  <li id='dvCongNo' class='submenu activeMenu'>";
        //     else
        //         html += "  <li id='dvCongNo' class='submenu'>";
        //     html += @"<a href='#'><i class='fa fa-file-text-o FixIcon'></i> <span>Công nợ</span></a>
        //      <ul class='treeview-menu'>";

        //     html += "<li ";
        //     if (URL.Contains(("/CongNo/CongNoKhachHang.aspx").ToUpper()))
        //     {
        //         html += @" class='activeSubMenu'";
        //     }
        //     html += @" > <a href='../CongNo/CongNoKhachHang.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Khách hàng nợ</a></li>";
        //     html += "<li ";
        //     if (URL.Contains(("/CongNo/CongNoCOD.aspx").ToUpper()))
        //     {
        //         html += @" class='activeSubMenu'";
        //     }
        //     html += @" > <a href='../CongNo/CongNoCOD.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>COD</a></li>";
            
        //     html += "</ul>";
        // }
        //if (mQuyen.ToUpper() == "TAIXE" || mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/Xe/"))
        //        html += "  <li id='dvPhanHangLenXe' class='activeMenu'>";
        //    else
        //        html += "  <li id='dvPhanHangLenXe' class=''>";

        //    if (mQuyen == "KH")
        //        html += "  <a href='../Xe/PhanHangLenXe.aspx?idKhachHang=" + mIdKhachHang + "'>";

        //    else
        //        html += "  <a href='../Xe/PhanHangLenXe.aspx'>";
        //    html += "    <i class='fa fa-file-text-o FixIcon'></i> <span>Phân hàng lên xe</span>";
        //    html += "  </a>";
        //    html += "</li>";
        //}


        ///////// Xe
        if (mQuyen.ToUpper() == "TAIXE" || mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
        {
            if (URL.Contains("/XE/"))
                html += "  <li id='dvXe' class='submenu activeMenu'>";
            else
                html += "  <li id='dvXe' class='submenu'>";
            html += @"<a href='#'><i class='fa fa-file-text-o FixIcon'></i> <span>Danh Mục Điều Xe</span></a>
              <ul class='treeview-menu'>";

            html += "<li ";
            if (URL.Contains(("/Xe/PhanHangLenXe.aspx").ToUpper()))
            {
                html += @" class='activeSubMenu'";
            }
            html += @" > <a href='../Xe/PhanHangLenXe.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Phân Hàng Lên Xe</a></li>";

            if (mQuyen.ToUpper() == "ADMIN")
            {
                html += "<li ";
                if (URL.Contains(("/Xe/DanhMucXe.aspx").ToUpper()))
                {
                    html += @" class='activeSubMenu'";
                }
                html += @" > <a href='../Xe/DanhMucXe.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Danh Mục Xe</a></li>";

            }
            html += "</ul>";
        }
        /////////// Thống kê
        //if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "QTWS")
        //{
        //    if (URL.Contains("/THONGKE/"))
        //        html += "  <li id='dvThongKe' class='submenu activeMenu'>";
        //    else
        //        html += "  <li id='dvThongKe' class='submenu'>";
        //    html += @"<a href='#'><i class='fa fa-bar-chart FixIcon'></i> <span>Thống Kê</span></a>
        //      <ul class='treeview-menu'>";

        //    //html += "<li ";
        //    //if (URL.Contains(("/ThongKe/ThongKeDonHang.aspx").ToUpper()))
        //    //{
        //    //    html += @" class='activeSubMenu'";
        //    //}
        //    //html += @" > <a href='../ThongKe/ThongKeDonHang.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Thống kê đơn hàng</a></li>";
        //    if (mQuyen.ToUpper() != "KH")
        //    {
        //        //    html += "<li ";
        //        //    if (URL.Contains(("/ThongKe/ThongKeDonHangNhanTrongNgay.aspx").ToUpper()))
        //        //{
        //        //    html += @" class='activeSubMenu'";
        //        //}
        //        //    html += @" ><a href='../ThongKe/ThongKeDonHangNhanTrongNgay.aspx'><i class='fa fa-bar-chart FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Hàng Hóa Nhận</a></li>";


        //        html += "<li ";
        //        if (URL.Contains(("/ThongKe/HangHoaCho.aspx").ToUpper()))
        //        {
        //            html += @" class='activeSubMenu'";
        //        }
        //        html += @" ><a href='../ThongKe/HangHoaCho.aspx'><i class='fa fa-bar-chart FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Hàng Hóa Chở</a></li>";

        //        html += "<li ";
        //        if (URL.Contains(("/ThongKe/ThongKeDoanhThu.aspx").ToUpper()))
        //        {
        //            html += @" class='activeSubMenu'";
        //        }
        //        html += @" ><a href='../ThongKe/ThongKeDoanhThu.aspx'><i class='fa fa-bar-chart FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Doanh Thu Trong Ngày</a></li>";
        //        //html += "<li ";
        //        //if (URL.Contains(("/ThongKe/ThongKeChuyenPhatNhanh.aspx").ToUpper()))
        //        //{
        //        //    html += @" class='activeSubMenu'";
        //        //}
        //        //html += @" ><a href='../ThongKe/ThongKeChuyenPhatNhanh.aspx'><i class='fa fa-bar-chart FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Tiền Chuyển Phát Nhanh</a></li>";

        //    }




        //    html += @"</ul>
        //   </li>";
        //}


        ////Quan Lý Chi
        //if (URL.Contains(("/QuanLyChiTieu/").ToUpper()))
        //    html += "  <li id='dvQuanLyChiTieu' class='activeMenu'>";
        //else
        //    html += "  <li id='dvQuanLyChiTieu'>";
        //html += "  <a href='../QuanLyChiTieu/QuanLyChiTieu.aspx'>";
        //html += "    <i class='fa fa-file-text-o FixIcon'></i> <span>Quản Lý Chi</span>";
        //html += "  </a>";
        //html += "</li>";

            ///////// QUản lý Thu Chi
            if (mQuyen.ToUpper() == "ADMIN" || mQuyen.ToUpper() == "NVGN" || mQuyen.ToUpper() == "NVVP" || mQuyen.ToUpper() == "KH" || mQuyen.ToUpper() == "QTWS")
            {
                if (URL.Contains("/QUANLYTHUCHI/"))
                    html += "  <li id='dvQuanLyThuChi' class='submenu activeMenu'>";
                else
                    html += "  <li id='dvQuanLyThuChi' class='submenu'>";
                html += @"<a href='#'><i class='fa fa-file-text-o FixIcon'></i> <span>Quản Lý Thu Chi</span></a>
                  <ul class='treeview-menu'>";

                html += "<li ";
                if (URL.Contains(("#").ToUpper()))
                {
                    html += @" class='activeSubMenu'";
                }
                html += @" > <a href='#'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Thu</a></li>";

                html += "<li ";
                if (URL.Contains(("/QuanLyChiTieu/QuanLyChiTieu.aspx").ToUpper()))
                {
                    html += @" class='activeSubMenu'";
                }
                html += @" > <a href='../QuanLyChiTieu/QuanLyChiTieu.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Quản Lý Chi</a></li>";

                html += "<li ";
                if (URL.Contains(("#").ToUpper()))
                {
                    html += @" class='activeSubMenu'";
                }
                html += @" > <a href='#'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Doanh Thu Đơn Hàng</a></li>";

                html += "<li ";
                if (URL.Contains(("/ThongKe/ThongKeDoanhThu.aspx").ToUpper()))
                {
                    html += @" class='activeSubMenu'";
                }
                html += @" > <a href='../ThongKe/ThongKeDoanhThu.aspx'><i class='fa fa-file-text FixIcon'  style='margin-top: 7px; margin-right:5px' ></i>Doanh Thu Trong Ngày</a></li>";

                html += "</ul>";
            }



        /////////
        /////////Đổi mật khẩu
        //if (URL.Contains("/DOIMATKHAU/"))
        //    html += "  <li id='dvDoiMatKhau' class='active'>";
        //else
        //    html += "  <li id='dvDoiMatKhau' class='active'>";
        //html += "  <a href='../DoiMatKhau/DoiMatKhau.aspx'>";
        //html += "    <i class='fa fa-retweet FixIcon'></i> <span>Đổi mật khẩu</span>";
        //html += "  </a>";
        //html += "</li>";
        /////////
        html += "</ul>";
        sidebar.InnerHtml = html;
    }
}
