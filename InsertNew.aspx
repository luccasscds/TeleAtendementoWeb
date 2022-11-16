<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertNew.aspx.cs" Inherits="TeleAtendimentoWeb.InsertNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href=".\libs\css\bootstrap.min.css" rel="stylesheet" crossorigin="anonymous" />
    <link href=".\assets\css\index.css" rel="stylesheet"/>
    <script src=".\libs\js\bootstrap.bundle.min.js"></script>
    <title><%= TeleAtendimentoWeb.Global.APP_NAME  %> | Novo usuário</title>
</head>
<body>
    <h3>Dados Pessoais</h3>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Lb_Nome" CssClass="form-label"  runat="server" Text="Nome"></asp:Label>
            <asp:TextBox ID="Tb_Nome" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label ID="Lb_CPF" CssClass="form-label" runat="server" Text="CPF"></asp:Label>
            <asp:TextBox ID="Tb_CPF" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
            <br />

            <h3>Endereço</h3>
            <asp:Label ID="Lb_CEP" CssClass="form-label" runat="server" Text="CEP"></asp:Label>
            <asp:TextBox ID="Tb_CEP" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>

            <asp:Label ID="Lb_Logradouro" CssClass="form-label" runat="server" Text="Logradouro"></asp:Label>
            <asp:TextBox ID="Tb_Logradouro" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label ID="Lb_Numero" CssClass="form-label" runat="server" Text="Número"></asp:Label>
            <asp:TextBox ID="Tb_Numero" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>

            <asp:Label ID="Lb_Bairro" CssClass="form-label" runat="server" Text="Bairro"></asp:Label>
            <asp:TextBox ID="Tb_Bairro" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label ID="Lb_Cidade" CssClass="form-label" runat="server" Text="Cidade"></asp:Label>
            <asp:TextBox ID="Tb_Cidade" CssClass="form-control" runat="server"></asp:TextBox>

            <asp:Label ID="Lb_Estado" CssClass="form-label" runat="server" Text="Estado"></asp:Label>
            <asp:DropDownList ID="DropDownList_estado" CssClass="form-control" runat="server"></asp:DropDownList>
            <br />

            <h3>Telefones</h3>
            <asp:ImageButton runat="server" ID="Btn_Add_Telefone"
                CssClass="btn btn-info mb-3" ImageUrl=".\assets\images\icon-phone-plus.svg" 
                OnClick="Btn_Add_Telefone_Click" ToolTip="Adicionar outro telefone"
            />
            <asp:ImageButton runat="server" ID="Btn_Remove_Telefone"
                CssClass="btn btn-danger mb-3" ImageUrl=".\assets\images\icon-phone-x.svg" 
                OnClick="Btn_Remove_Telefone_Click" ToolTip="Remover telefone"
            />
            <div style="display: flex; flex-wrap: wrap; gap: 1rem;">
                <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
            </div>
            <br />
            <br />

            
            <asp:Button ID="Btn_ok" CssClass="btn btn-success" runat="server" Text="OK" OnClick="Btn_ok_Click" />
            <asp:Button ID="Btn_cancel" CssClass="btn btn-danger" runat="server" OnClick="Btn_cancel_Click" Text="Cancelar" />
            <br />
        </div>
    </form>
</body>
</html>
