<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="formcreation.aspx.cs" Inherits="WebApplication1.formcreation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <style>
        .bg_color {
            background-color: rgb(255, 240, 245);
        }

        .form_border {
            border: 2px solid rgb(199, 131, 160);
        }

        #gridview1 tr {
            border-bottom: 1px solid black;
        }

        #gridview1 td {
            border-left: 1px solid black;
        }

        #gridview1 th {
            border-left: 1px solid black;
        }
        .wrap { white-space: normal; width: 100px; }
                input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

/* Firefox */
input[type=number] {
  -moz-appearance: textfield;
}
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script>
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section class="bg_color">
            <section>
                <div class="container">
                    <div class="row">
                        <div class="col-6">
                            <h1 class="text-center py-3">Form Submission</h1>
                            <div class="row form_border py-2 px-2">

                                <div class="row py-3">
                                    <div class="col d-flex justify-content-center">
                                        <label class="fw-bold fs-5 " for="firstname ">
                                            FirstName :</label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <input class="w-100 h-80 " type="text" name="" runat="server" autocomplete="off" id="firstname" />
                                    </div>
                                </div>
                                <div class="row py-3">
                                    <div class="col d-flex justify-content-center">
                                        <label class="fw-bold fs-5" for="lastname ">LastName :</label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <input class="w-100 h-80" type="text" name="" runat="server" autocomplete="off" id="lastname" />
                                    </div>
                                </div>
                                <div class="row py-3">
                                    <div class="col d-flex justify-content-center">
                                        <label class="fw-bold fs-5" for="address ">Address :</label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <input class="w-100 h-80" type="text" name="" runat="server" autocomplete="off" id="address" />
                                    </div>

                                </div>
                                <div class="row py-3">
                                    <div class="col d-flex justify-content-center">
                                        <label class="fw-bold fs-5" for="gender" id="gender">Gender :</label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <div class="row  align-items-center">
                                            <div class="col-6 d-flex">
                                                <input type="radio" name="gender" id="male" value="male" />
                                                <label class="fw-bold m-1 fs-6" for="male">Male</label>
                                            </div>
                                            <div class="col-6 d-flex ">
                                                <input type="radio" name="gender" id="female" value="female" />
                                                <label class="fw-bold m-1 fs-6" for="female">Female</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row py-3">
                                    <div class="col d-flex justify-content-evenly">
                                        <label class="fw-bold fs-5" for="country">Country :</label>
                                    </div>
                                    <div class="col">
                                        <select class="w-80" id="country" name="country">
                                            <option value="">Please Select an Option</option>
                                            <option value="india">India</option>
                                            <option value="usa">Usa</option>
                                            <option value="china">China</option>
                                            <option value="japan">Japan</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row py-3">
                                    <div class="col d-flex justify-content-center">
                                        <label class="fw-bold fs-5" for="pin ">Pin :</label>
                                    </div>
                                    <div class="col">
                                        <input class="w-100 h-80 pin" type="number"  name="" id="pin" />
                                   
                                    </div>
                                </div>
                                <div class="row py-3">
                                    <div class="col"></div>
                                    <div class="col d-flex justify-content-end">
                                        <asp:Button runat="server" ID="reset" type="reset" OnClick="reset_Click" value="Reset" class="m-1 btn btn-outline-dark" Text="Cancel"></asp:Button>
                                        <asp:Button ID="btnSumbit" OnClick="btnSumbit_Click" runat="server" value="Submit" Text="Submit" class="m-1 btn btn-outline-success " OnClientClick="javascript: return test()"></asp:Button>

                                        <asp:Button OnClick="view_Click" class="m-1 btn btn-outline-warning" runat="server" Text="View" ID="view"></asp:Button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col py-5  ">
                            <div class="container">
                                <div class="row ">
                                    <asp:GridView ID="gridview1" runat="server" AutoGenerateColumns="false" CellPadding="10" GridLines="Horizontal" BorderColor="Black" BorderStyle="Groove" OnRowDeleting="gridview1_RowDeleting" OnRowEditing="gridview1_RowEditing" OnRowUpdating="gridview1_RowUpdating" OnRowCancelingEdit="gridview1_RowCancelingEdit">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sno">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1  %>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="user_id" SortExpression="user_id">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="editcol10" runat="server" Text='<%#Bind("user_id") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Bind("user_id")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="personname" SortExpression="firstname">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="editcol1" runat="server" Text='<%#Bind("firstname") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>

                                                    <asp:Label ID="col1" runat="server" Text='<%#Bind("firstname")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="personlastname" SortExpression="lastname">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="editcol2" runat="server" Text='<%#Bind("lastname") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="col2" runat="server" Text='<%#Bind("lastname") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="personaddress" SortExpression="address">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="editcol3" runat="server" Text='<%#Bind("address")%>'></asp:TextBox>

                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="col3" runat="server" Text='<%#Bind("address")%>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" SortExpression="date">
                                                <ItemTemplate>
    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("date", "{0:dd-MM-yyyy hh:mm:ss}") %>'></asp:Label>
</ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Button" HeaderText="Remove" ControlStyle-CssClass="btn btn-danger" Text="Delete" runat="server" CommandName="delete" />
                                            <asp:CommandField ShowEditButton="true" ControlStyle-CssClass="btn btn-warning" HeaderText="Action" />

                                        </Columns>

                                    </asp:GridView>

                                </div>
                                <div class="row">
                                    <div class="col-6"></div>
                                    <div class="col-6 d-flex justify-content-end ">
                                        <asp:Button type="button" Visible="false" Text="cleardata" ID="cleardata" runat="server" OnClick="cleardata_Click" OnClientClick="javascript: foo()" class="my-2 mx-2 btn btn-outline-danger"></asp:Button>
                                        <asp:Button runat="server" ID="btnExportToExcel" Text="Download as (Excel)" Visible="false" OnClick="btnExportToExcel_Click"  class="my-3  mx-2 btn btn-outline-info wrap" />
                                        <asp:Button runat="server" ID="btnExportToPdf" Text="Download as (Pdf)" Visible="false" OnClick="btnExportToPdf_Click"
                                             class="my-3  mx-2 btn btn-outline-primary wrap" />
                                    </div>
                                    <h1 id="NoRecords_text" class="text-danger d-flex justify-content-center" runat="server">No Records Found 🙅‍</h1>

                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </section>
        </section>
    </form>
    <script src="Scripts/script.js"></script>

</body>
</html>
