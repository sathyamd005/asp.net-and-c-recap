<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepeaterTable.aspx.cs" Inherits="WebApplication1.RepeaterTable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Additional CSS (Bulma and FontAwesome) -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bulma/0.4.2/css/bulma.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" />

    <!-- FooTable CSS -->
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/2.0.3/css/footable.core.min.css" integrity="sha512-bAGeVDVMoP6dDZ4gMuxUcZDHUHKxb1R/2GZFqBlacok0d4tSA7cGof6rHosLRGqrQ3Pz2S0iyGOO1VtITvPdDg==" crossorigin="anonymous" referrerpolicy="no-referrer" />

 
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
             <div class="row my-5">
            <asp:Repeater ID="repeaterview" runat="server">
                <HeaderTemplate>
                    <table   data-paging="true" data-pagining-size="10" data-sorting="true" border="1" class="table table-dark table-striped text-white">
                        <thead>
                            <tr>
                                <th>SNO</th>
                                <th >firstname</th>
                                <th >lastname</th>
                                <th data-hide="tablet">date</th>
                                <th  data-hide="tablet">address</th>
                                <th  data-hide="tablet">phonenumber</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Container.ItemIndex + 1 %></td>
                        <td><%# Eval("firstname") %></td>
                        <td><%# Eval("lastname") %></td>
                        <td><%# Eval (("date"),"{0:dd/MM/yyyy}") %></td>
                        <td><%# Eval("address") %></td>
                        <td><%# Eval("phonenumber") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                       </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="row my-5">
            <h1 id="NoRecords" class="text-center text-danger fs-1" runat="server">NO RECORDS FOUND 🙅‍</h1>
        </div>
        </div>
       
    </form>
       <!-- jQuery -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- FooTable JavaScript -->
   <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/2.0.3/js/footable.min.js" integrity="sha512-ivFYZ2+UaqhOH3d82bpebXvJY5ZcH2LfrI5y3lSZQvcYH2oF9TzoK82lrpshppIZX++twg9nMaNIMK4IgkKgkw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script>
        $(function () {
            $('.table').footable({
                filtering: {
                    enabled: true
                }

            });
        });
    </script>
</body>
</html>
