using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SQLite;
using System.Web.Services.Description;
using System.Reflection;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace TeleAtendimentoWeb
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetOptionsGridView();
        }
        private void GetOptionsGridView()
        {
            this.GridView.DataSource = Pessoa.Consultar("all");
            this.GridView.DataBind();
        }
        protected void Btn_Inserir_Click(object sender, EventArgs e)
        {
            Response.Redirect("InsertNew.aspx");
        }

        protected void Btn_modificar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imageButton.NamingContainer;

            if (row.RowIndex >= 0)
            {
                string selectedCPF = GridView.Rows[row.RowIndex].Cells[2].Text;

                Response.Redirect("Update.aspx?cpf=" + selectedCPF);
            }
            //Debug.WriteLine(imageButton.Attributes["RowIndex"]);
        }

        protected void Btn_deletar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imageButton.NamingContainer;

            if (row.RowIndex >= 0)
            {
                string selectedCPF = GridView.Rows[row.RowIndex].Cells[2].Text;

                Pessoa[] newPerson = Pessoa.Consultar(selectedCPF);
                Pessoa.Excluir(newPerson[0]);
                GetOptionsGridView();
            }
        }
    }
}