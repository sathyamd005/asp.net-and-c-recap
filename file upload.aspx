<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="file upload.aspx.cs" Inherits="WebApplication1.file_upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">

    <style>
        .uploadbtn{
            white-space:normal;
            width:150px;
              display:inline;
        }
        .fileupload{
            display:inline;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <section  class="d-flex justify-content-center  align-items-center" style="width:100vw; height:100vh">
       <div class="container">
             <div class="row mx-5 py-5  justify-content-center align-items-center border border-dark border border-3">
                 <div class="col-3">
                     <p class="m-0"><b>Please Select Excel File: </b></p>
                     <asp:FileUpload  runat="server" ID="UploadFileAsCSV" CssClass="fileupload"/>
                 </div>
                  <div class="col-3">
                 <asp:Button ID="UploadButton" runat="server" Text="Upload(ExcelFile) " class="uploadbtn btn btn-outline-dark" OnClick="UploadButton_Click"/>
                       <asp:Button ID="viewbtn" runat="server" Text="view" CssClass=" btn btn-outline-primary" OnClick="viewbtn_Click"/>
                 <asp:Label ID="label" runat="server" Visible="false" Font-Bold="true" ForeColor="#00ff00"></asp:Label><br />
                 
                  </div>
             </div>
        </section>

    </div>
    </form>
</body>
</html>
