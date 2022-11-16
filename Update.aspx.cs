using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TeleAtendimentoWeb
{
    public partial class Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList_estado.Items.Clear();
                DropDownList_estado.Items.AddRange(InsertNew.GetOptionsDropDownListEstado());
            }
            Pessoa currentPerson = Pessoa.Consultar(Request.Params["cpf"])[0];
            SetValuesTextBox(currentPerson);
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //CreateTextBox("DDD", "test", 80);
            //CreateTextBox("Número de telefone", "test", Unit.Empty);
        }
        private void SetValuesTextBox(Pessoa person)
        {
            Tb_Nome.Text = person.NOME;
            Tb_CPF.Text = person.CPF.ToString();
            Tb_Logradouro.Text = person.ENDERECO?.LOGRADOURO;
            Tb_Numero.Text = person.ENDERECO?.NUMERO.ToString();
            Tb_CEP.Text = person.ENDERECO?.CEP.ToString();
            Tb_Bairro.Text = person.ENDERECO?.BAIRRO;
            Tb_Cidade.Text = person.ENDERECO?.CIDADE;
            SelectedValueDropDow(DropDownList_estado, person.ENDERECO?.ESTADO);

            bool isPhone = person.TELEFONES.Count > 0;
            int count = 0;
            foreach (Telefone telefone in person.TELEFONES)
            {
                CreateTextBox(
                    count.ToString(), 
                    isPhone ? telefone?.DDD.ToString() : "",
                    isPhone ? telefone?.NUMERO.ToString() : "",
                    isPhone ? telefone?.TIPO?.TIPO : " ",
                    PlaceHolder
                );
                count++;
            }
        }
        public static void SelectedValueDropDow(DropDownList dropDownList, string value)
        {
            for (int i = 0; i < dropDownList.Items.Count; i++)
            {
                if (dropDownList.Items[i].Value == value)
                {
                    dropDownList.SelectedIndex = i;
                }
            }
        }
        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
        protected void Btn_ok_Click(object sender, EventArgs e)
        {
            Pessoa person = Pessoa.Consultar(Request.Params["cpf"])[0];
            person.NOME = Tb_Nome.Text;
            person.CPF = long.Parse(Tb_CPF.Text);
            person.ENDERECO.LOGRADOURO = Tb_Logradouro.Text;
            person.ENDERECO.NUMERO = long.Parse(Tb_Numero.Text);
            person.ENDERECO.CEP = long.Parse(Tb_CEP.Text);
            person.ENDERECO.BAIRRO = Tb_Bairro.Text;
            person.ENDERECO.CIDADE = Tb_Cidade.Text;
            person.ENDERECO.ESTADO = DropDownList_estado.SelectedValue;

            int count = 0;
            foreach (Telefone telefone in person.TELEFONES)
            {
                TextBox ddd = (TextBox)PlaceHolder.FindControl("Tb_DDD_"+count);
                TextBox numero = (TextBox)PlaceHolder.FindControl("Tb_Numero_telefone_"+count);
                DropDownList tipo = (DropDownList)PlaceHolder.FindControl("DropDownList_Tipo_"+count);

                person.TELEFONES[count].DDD = long.Parse(ddd?.Text == "" ? "0" : ddd?.Text);
                person.TELEFONES[count].NUMERO = long.Parse(numero?.Text == "" ? "0" : numero?.Text);
                person.TELEFONES[count].TIPO.TIPO = tipo.SelectedValue.ToLower();
                count++;
            }
            Pessoa.Alterar(person);
        }
        public static void CreateTextBox(
            string IdCount, 
            string LabelValue, 
            string TextBoxValue, 
            string DropDowValue , 
            PlaceHolder placeHolder
        )
        {
            placeHolder.Controls.Add(new LiteralControl("<div class='container_"+IdCount+"'>"));

            placeHolder.Controls.Add(new Label()
            {
                CssClass = "form-label",
                Text = "DDD"
            });
            placeHolder.Controls.Add(new TextBox()
            {
                ID = "Tb_DDD_"+ IdCount,
                Text = LabelValue,
                CssClass = "form-control",
                TextMode = TextBoxMode.Number,
                Width = 80
            });

            placeHolder.Controls.Add(new Label()
            {
                CssClass = "form-label",
                Text = "Número de telefone"
            });
            placeHolder.Controls.Add(new TextBox()
            {
                ID = "Tb_Numero_telefone_"+ IdCount,
                Text = TextBoxValue,
                CssClass = "form-control",
                TextMode = TextBoxMode.Number
            });

            placeHolder.Controls.Add(new Label()
            {
                CssClass = "form-label",
                Text = "Tipo de telefone"
            });
            DropDownList newDropDownList = new DropDownList()
            {
                ID = "DropDownList_Tipo_" + IdCount,
                Text = TextBoxValue,
                CssClass = "form-control"
            };
            newDropDownList.Items.Clear();
            newDropDownList.Items.AddRange(InsertNew.GetOptionsDropDownListTipoTelefone());
            SelectedValueDropDow(newDropDownList, DropDowValue);
            placeHolder.Controls.Add(newDropDownList);

            placeHolder.Controls.Add(new LiteralControl("</div>"));
        }
    }
}