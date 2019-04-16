<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeFile="DangNhap.aspx.cs" Inherits="Home_DangNhap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Phần Mềm Điều Xe Quận 5</title>
    <link rel="icon" href="https://xep.vn/images/mypics/icon100.png" type="image/x-icon" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/flat-ui.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>

    <script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="../bootstrap/js/bootstrap.min.js"></script>
    <!-- AdminLTE App -->
    <script src="../dist/js/app.min.js"></script>

     <link href="../plugins/iCheck/all.css" rel="stylesheet" />
    <script src="../plugins/iCheck/icheck.min.js"></script>


    <script type="text/javascript">



        $(document).ready(function () {
            //alert('dfdsf');
            //$("#dvHeader").load("../header.html");
            //$("#dvMenu").load("../menu.html");
            $("#dvFooter").load("../footer.html");



        });
        function loadPage(href) {
            var xmlhttp = new XMLHttpRequest();
            xmlhttp.open("GET", href, false);
            xmlhttp.send();
            return xmlhttp.responseText;
        }


    </script>

</head>
<%--<body class="hold-transition skin-blue sidebar-mini">--%>
<body style="background-image: url('../images/BG_LIGHT.jpg');background-size:auto; background-repeat:no-repeat" >

<div style="margin-top:5px">
            <div style="font-family:Arial; font-size:10pt; color: #EDEFF1; padding-left: 20px;">
                © Được phát triển bởi <i><a style="color: #EDEFF1" href="https://xep.vn/"><b>Công ty TNHH Phần Mềm XEP</b></a></i>
            </div>
    </div>
<%--<body style="background-color: #1ABC9C">--%>
    <form runat="server">
        <div>
            <!-- Khung đăng nhập -->
            <div class="login-box">
                <%--Thông báo lỗi--%>
                <div id="ThongBao" runat="server"></div>
                <!-- Khung đăng nhập -->
                <div class="login-form">
                    <%--Tiêu đề phần mềm--%>
                    <div style="text-align: center; padding-bottom: 20px; color: #1ABC9C; font-size:15pt"> PHẦN MỀM ĐIỀU XE QUẬN 5</div>
                    <%--Tên đăng nhập--%>


                    <%--<div id="QuyenDangNhap" runat="server" style="text-align: center">
                        <label style="padding-right: 10px;">
                            <input id="rbtCuaHang" type="radio" name="r3" class="flat-red" runat="server" checked="" />
                            Cửa hàng
                        </label>
                        <label>
                            <input id="rbtNguoiDung" type="radio" name="r3" runat="server" class="flat-red" />
                             Người dùng
                        </label>
                    </div>--%>

                    <div id="dvTenDangNhap" runat="server" class="form-group">
                        <input type="text" style="padding-right: 30px" class="form-control login-field" id="txtTenDangNhap" runat="server" placeholder="Tên đăng nhập" />
                        <span class="ion-person form-control-feedback"></span>
                    </div>
                    <%--Mật khẩu--%>
                    <div id="dvMatKhau" runat="server" class="form-group">
                        <input type="password" style="padding-right: 30px" class="form-control login-field" id="txtMatKhau" runat="server" placeholder="Mật khẩu" />
                        <span class="ion-locked form-control-feedback"></span>
                    </div>
                  

                    <%--Nút đăng nhập--%>
                    <div class="form-group" style="padding-bottom: 20px">
                        <div style="padding-top: 10px; text-align: right">
                            <asp:LinkButton ID="btDangNhap" runat="server" class="btn btn-primary pull-right" OnClientClick="Test()"  OnClick="btDangNhap_Click"><i class="fa fa-sign-in" aria-hidden="true"></i> Đăng nhập </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    
    <script>

        $('#rbtNguoiDung').on('ifChanged', function (event) {
            document.getElementById("rbtNguoiDung").checked = true;
        });

        $('#rbtTaiXe').on('ifChanged', function (event) {
            document.getElementById("rbtTaiXe").checked = true;
        });

        $('#rbtPhuXe').on('ifChanged', function (event) {
            document.getElementById("rbtPhuXe").checked = true;
        });


        $('#rbtChuHang').on('ifChanged', function (event) {
            document.getElementById("rbtChuHang").checked = true;
        });


        $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });
        //Red color scheme for iCheck
        $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
            checkboxClass: 'icheckbox_minimal-red',
            radioClass: 'iradio_minimal-red'
        });
        //Flat red color scheme for iCheck
        $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
            checkboxClass: 'icheckbox_flat-green',
            radioClass: 'iradio_flat-green'
        });
    </script>

</body>
</html>

