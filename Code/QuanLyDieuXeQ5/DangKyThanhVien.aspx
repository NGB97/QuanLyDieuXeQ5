<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DangKyThanhVien.aspx.cs" Inherits="DangKyThanhVien" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Phần mềm quản lý hoa hồng Vitamia</title>
  <link rel="shortcut icon" type="../image/x-icon" href="../Images/Logo.png" />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="../css/style.css">
  <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="../dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect.
  -->
  <link rel="stylesheet" href="../dist/css/skins/skin-blue.min.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
  <!-- REQUIRED JS SCRIPTS -->

<!-- jQuery 2.2.0 -->
<script src="../plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="../bootstrap/js/bootstrap.min.js"></script>
<!-- AdminLTE App -->
<script src="../dist/js/app.min.js"></script>

<!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. Slimscroll is required when using the
     fixed layout. -->
  <script type="text/javascript">
      $(document).ready(function () {
          //alert('dfdsf');
          //$("#dvHeader").load("../header.html");
          //$("#dvMenu").load("../menu.html");
          $("#dvFooter").load("../footer.html");
          ChonLoaiDaiLy();
      });
      function loadPage(href) {
          var xmlhttp = new XMLHttpRequest();
          xmlhttp.open("GET", href, false);
          xmlhttp.send();
          return xmlhttp.responseText;
      }
      function ChonLoaiDaiLy()
      {
          var LoaiDaiLy = document.getElementById("slLoaiDaiLy").value;
          //alert(LoaiDaiLy);
          if(LoaiDaiLy == "Cá nhân")
          {
              document.getElementById("dvCaNhan").style.display = "block";
              document.getElementById("dvDoanhNghiep").style.display = "none";
          }
          if (LoaiDaiLy == "Doanh nghiệp") {
              document.getElementById("dvCaNhan").style.display = "none";
              document.getElementById("dvDoanhNghiep").style.display = "block";
          }
          if (LoaiDaiLy == "") {
              document.getElementById("dvCaNhan").style.display = "none";
              document.getElementById("dvDoanhNghiep").style.display = "none";
          }
      }
    </script> 
</head>
<body class="hold-transition skin-blue sidebar-mini" style="background-image: url('../images/bg3.jpg');background-size: 100%;background-attachment: fixed;background-position: center top;">
<form runat="server">
<div>
  <!-- Content Wrapper. Contains page content -->
  <div style="padding: 5% 15%;">
    
    <!-- Horizontal Form -->
          <div class="box box-info" style="background-color: #f1f1f1;">
            <div class="box-header with-border">
              <div style="COLOR: #00c0ef;FONT-SIZE: 25PX;FONT-WEIGHT: BOLD;TEXT-ALIGN: CENTER;PADDING-BOTTOM: 15PX;">ĐĂNG KÝ THÀNH VIÊN VITAMIA</div>
              <!--<h3 class="box-title" style="text-align:center">Đăng nhập hệ thống</h3>-->
            </div>
            <!-- /.box-header -->
            <!-- form start -->
              <div class="box-body">
                <div class="form-group">
                    <div class="row">
                        <div class="dvnull">&nbsp;</div>
                        <div class="coninput1">
                          <div class="titleinput"><b>Loại đại lý:</b></div>
                          <div class="txtinput">
                              <select class="form-control" id="slLoaiDaiLy" onclick="ChonLoaiDaiLy()" runat="server">
                                  <option value="">-- Chọn --</option>
                                  <option value="Cá nhân">Cá nhân</option>
                                  <option value="Doanh nghiệp">Doanh nghiệp</option>
                              </select>
                          </div>
                        </div>
                        <%--<div class="coninput2">
                          <div class="titleinput"><b>Đến ngày:</b></div>
                          <div class="txtinput">
                              <input class="form-control" data-val="true" data-val-required="" id="Text2" runat="server" name="Content.ContentName" type="text" value="" />
                          </div>
                        </div>--%>
                      </div>
                </div>
                  <div id="dvCaNhan" style="display:none">
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Họ tên:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text1" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Giới tính:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select1" runat="server">
                                      <option>-- Chọn--</option>
                                      <option value="Nam">Nam</option>
                                      <option value="Nữ">Nữ</option>
                                  </select>
                              </div>
                            </div>
                          </div>
                      </div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>CMND:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text2" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Số điện thoại:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text3" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>
                          </div>
                      </div>
                      
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Huyện/ Quận:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select3" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Tỉnh/ TP:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select2" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                          </div>
                      </div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Email:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text4" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>
                            <%--<div class="coninput2">
                              <div class="titleinput"><b>Số điện thoại:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text5" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>--%>
                          </div>
                      </div>
                  </div>
                  <div id="dvDoanhNghiep" style="display:none">
                      <div style="font-weight: bold;color: #2196F3;padding-left: 3%;padding-bottom: 10px;">Thông tin doanh nghiệp</div>
                        <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Tên DN:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text5" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <div class="coninput2">
                                  <div class="titleinput"><b>Mã số thuế:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text6" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                              </div>
                          </div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Huyện/ Quận:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select4" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Tỉnh/ TP:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select5" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                          </div>
                      </div>
                      <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Chủ DN:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text7" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <div class="coninput2">
                                  <div class="titleinput"><b>Số điện thoại:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text8" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                              </div>
                          </div>
                          <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Email:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text9" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <div class="coninput2">
                                  <div class="titleinput"><b>Website:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text10" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                              </div>
                          </div>
                         <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Tên ngân hàng:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text11" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <div class="coninput2">
                                  <div class="titleinput"><b>Số tài khoản:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text12" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                              </div>
                          </div>
                          <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Chi nhánh:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text13" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <%--<div class="coninput2">
                                  <div class="titleinput"><b>Số tài khoản:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text14" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>--%>
                              </div>
                          </div>
                      <div style="font-weight: bold;color: #2196F3;padding-left: 3%;padding-bottom: 10px;">Thông tin liên hệ</div>
                      <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Họ tên:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text14" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <div class="coninput2">
                                  <div class="titleinput"><b>Email:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text15" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                              </div>
                          </div>
                      <div class="form-group">
                            <div class="row">
                                <div class="dvnull">&nbsp;</div>
                                <div class="coninput1">
                                  <div class="titleinput"><b>Số điện thoại:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text16" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>
                                <%--<div class="coninput2">
                                  <div class="titleinput"><b>Email:</b></div>
                                  <div class="txtinput">
                                      <input class="form-control" data-val="true" data-val-required="" id="Text17" runat="server" name="Content.ContentName" type="text" value="" />
                                  </div>
                                </div>--%>
                              </div>
                          </div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Huyện/ Quận:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select6" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Tỉnh/ TP:</b></div>
                              <div class="txtinput">
                                  <select class="form-control" id="Select7" runat="server">
                                      <option>-- Chọn--</option>
                                  </select>
                              </div>
                            </div>
                          </div>
                      </div>
                  </div>
                  <div id="dvThongTinDangNhap">
                      <div style="font-weight: bold;color: #2196F3;padding-left: 3%;padding-bottom: 10px;">Thông tin đăng nhập</div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Tên đăng nhập:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text17" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>
                            <%--<div class="coninput2">
                              <div class="titleinput"><b>Số điện thoại:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text5" runat="server" name="Content.ContentName" type="text" value="" />
                              </div>
                            </div>--%>
                          </div>
                      </div>
                      <div class="form-group">
                        <div class="row">
                            <div class="dvnull">&nbsp;</div>
                            <div class="coninput1">
                              <div class="titleinput"><b>Mật khẩu:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text18" runat="server" name="Content.ContentName" type="password" value="" />
                              </div>
                            </div>
                            <div class="coninput2">
                              <div class="titleinput"><b>Nhập lại MK:</b></div>
                              <div class="txtinput">
                                  <input class="form-control" data-val="true" data-val-required="" id="Text56" runat="server" name="Content.ContentName" type="password" value="" />
                              </div>
                            </div>
                          </div>
                      </div>
                  </div>
              </div>
              <!-- /.box-body -->
              <div class="box-footer" style="text-align:center;padding-right: 20px;background-color: #f1f1f1;">
                <button type="submit" class="btn btn-info">Đăng ký</button>
                <%--<button type="submit" class="btn btn-info pull-right">Đăng nhập</button>--%>
              </div>
              <!-- /.box-footer -->
          </div>
  </div>
  <!-- /.content-wrapper -->

  <!-- Main Footer -->
  <P style="color: #00c0ef;
    /* padding-left: 20PX; */
    FONT-SIZE: 17PX;
    text-align: center;">
    @ Được phát triển bởi <i><b><a href="https://xep.vn/">Công ty TNHH Phần Mềm XEP</a></b></i>
  </P>

  <!-- Control Sidebar -->
  <aside class="control-sidebar control-sidebar-dark">
    <!-- Create the tabs -->
    <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
      <li class="active"><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
      <li><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
      <!-- Home tab content -->
      <div class="tab-pane active" id="control-sidebar-home-tab">
        <h3 class="control-sidebar-heading">Recent Activity</h3>
        <ul class="control-sidebar-menu">
          <li>
            <a href="javascript::;">
              <i class="menu-icon fa fa-birthday-cake bg-red"></i>

              <div class="menu-info">
                <h4 class="control-sidebar-subheading">Langdon's Birthday</h4>

                <p>Will be 23 on April 24th</p>
              </div>
            </a>
          </li>
        </ul>
        <!-- /.control-sidebar-menu -->

        <h3 class="control-sidebar-heading">Tasks Progress</h3>
        <ul class="control-sidebar-menu">
          <li>
            <a href="javascript::;">
              <h4 class="control-sidebar-subheading">
                Custom Template Design
                <span class="label label-danger pull-right">70%</span>
              </h4>

              <div class="progress progress-xxs">
                <div class="progress-bar progress-bar-danger" style="width: 70%"></div>
              </div>
            </a>
          </li>
        </ul>
        <!-- /.control-sidebar-menu -->

      </div>
      <!-- /.tab-pane -->
      <!-- Stats tab content -->
      <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
      <!-- /.tab-pane -->
      <!-- Settings tab content -->
      <div class="tab-pane" id="control-sidebar-settings-tab">
        <form method="post">
          <h3 class="control-sidebar-heading">General Settings</h3>

          <div class="form-group">
            <label class="control-sidebar-subheading">
              Report panel usage
              <input type="checkbox" class="pull-right" checked>
            </label>

            <p>
              Some information about this general settings option
            </p>
          </div>
          <!-- /.form-group -->
        </form>
      </div>
      <!-- /.tab-pane -->
    </div>
  </aside>
  <!-- /.control-sidebar -->
  <!-- Add the sidebar's background. This div must be placed
       immediately after the control sidebar -->
  <div class="control-sidebar-bg"></div>
</div>
<!-- ./wrapper -->
</form>
</body>
</html>
