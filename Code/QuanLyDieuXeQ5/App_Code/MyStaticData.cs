using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;

/// <summary>
/// Summary description for MyStaticData
/// </summary>
public class MyStaticData
{
    public static string TaoMaDonHang()
    {
        string[] ngay = DateTime.Now.ToString("dd/MM/y").Split('/');
        string MaDonHang = ngay[2].Substring(1, 1) + ngay[1] + ngay[0];
        // Mã phiếu nhập , Ngày nhập
        //string sqlMaDonHang = "select top 1 idDonHang from tb_DonHang order by idDonHang desc";
        string sqlMaDonHang = "select top 1 MaDonHang from tb_DonHang where convert(varchar(10),NgayLap,103)='" + DateTime.Now.ToString("dd/MM/yyyy") + @"' and Len(MaDonHang) = 8
and SUBSTRING(MaDonHang,0,6) = '" + ngay[2].Substring(1, 1) + ngay[1] + ngay[0] + "' order by idDonHang desc";
        DataTable tableMaDonHang = Connect.GetTable(sqlMaDonHang);

        if (tableMaDonHang.Rows.Count > 0)
        {
            string sSoDH = tableMaDonHang.Rows[0]["MaDonHang"].ToString().Substring(5, tableMaDonHang.Rows[0]["MaDonHang"].ToString().Length - 5);
            if (sSoDH != "")
            {
                string sDuoi = (long.Parse(sSoDH) + 1).ToString();

                //string sDuoi = (long.Parse(tableMaDonHang.Rows[0]["idDonHang"].ToString()) + 1).ToString();

                if (sDuoi.Length == 1)
                    MaDonHang += "00" + sDuoi;
                if (sDuoi.Length == 2)
                    MaDonHang += "0" + sDuoi;
                if (sDuoi.Length > 2)
                    MaDonHang += sDuoi;
            }
            else
            {
                MaDonHang += "001";
            }
        }
        else
        {
            MaDonHang += "001";
        }
        return MaDonHang;
    }
    public static string GetTieuDeIn(string barCode, string idKhachHang)
    {
        string html = @"<div style='width: 100%; border-bottom: 1px solid #4a4a4a; padding-bottom: 0;'>
                            <table width='100%' border='0' style='font-size: 11px'>
                                <tr>
                                    <td style='text-align: center'>
                                        <img src='/images/logo1.png' height='25px'>
                                        <br />
                                        <img id='Imgcode' src='data:image/png;base64," + barCode + "' style='height:20px;width:180px;' /> </br> ";
        html += @"                  </td>
                                    <td>
                                        <table width='100%' border='0' style='margin-top: 0; font-size: 11px'>
                                            <tr>
                                                <td align='right'>
                                                    <strong>Của Hàng xe cô hai</strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align='right'>Địa chỉ: TP.HCM</td>
                                            </tr>
                                            <tr>
                                                <td align='right'>Điện thoại: 0975 626 292 - 0913 672 172</td>
                                            </tr>

                                            <tr>
                                                <td align='right'>Website: giaonhan.com</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>";
        return html;
    }
    public static string GetMaQuyen(string TenDangNhap)
    {
        string MaQuyen = "";
        MaQuyen = StaticData.getField("tb_NguoiDung", "MaQuyen", "TenDangNhap", TenDangNhap).Trim();
        if(MaQuyen == "")
        {
            string TenKhachHang = StaticData.getField("tb_KhachHang", "TenKhachHang", "TenDangNhap", TenDangNhap).Trim();
            if (TenDangNhap != "")
                MaQuyen = "KH";
        }
        return MaQuyen;
    }
}