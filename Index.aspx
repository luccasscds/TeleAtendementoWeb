<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TeleAtendimentoWeb.Index" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href=".\libs\css\bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
    <link href=".\assets\css\index.css" rel="stylesheet"/>
    <script src=".\libs\js\bootstrap.bundle.min.js"></script>
    <title><%= TeleAtendimentoWeb.Global.APP_NAME  %></title>
</head>
<body>
    <h1 style="text-align:center;">
        <%= TeleAtendimentoWeb.Global.APP_NAME  %>
        <small style="font-size:medium;">V<%= TeleAtendimentoWeb.Global.APP_VERSION %></small>
    </h1>

    <form id="form1" runat="server">
        <div>
            <asp:ImageButton runat="server" 
                CssClass="btn btn-info mb-3" ImageUrl=".\assets\images\icon-circle-plus.svg" 
                OnClick="Btn_Inserir_Click" ToolTip="Criar novo"
            />
            <br />
            <asp:GridView ID="GridView" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" 
                ForeColor="#333333" GridLines="None" Height="378px" 
                style="margin-right: 0px; width: 100%;"
            >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" />
                    <asp:BoundField DataField="NOME" HeaderText="Nome" />
                    <asp:BoundField DataField="CPF" HeaderText="CPF" />
                    <asp:TemplateField ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:ImageButton ControlStyle-CssClass="btn btn-danger" 
                                             ID="Btn_deletar" ImageUrl=".\assets\images\icon-trash.svg" 
                                             ToolTip="Deletar linha"
                                             OnClick="Btn_deletar_Click" OnClientClick="return confirm('Deseja apagar esse registro?')"
                                             runat="server" CommandName="Deletar"
                            />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:ImageButton ControlStyle-CssClass="btn btn-warning" 
                                             ID="Btn_modificar" ButtonType="Button"
                                             ImageUrl=".\assets\images\icon-edit.svg" 
                                             ToolTip="Modificar linha"
                                             OnClick="Btn_modificar_Click"
                                             runat="server" CommandName="Modificar"
                            />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <sortedascendingcellstyle backcolor="#F5F7FB" />
                <sortedascendingheaderstyle backcolor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
