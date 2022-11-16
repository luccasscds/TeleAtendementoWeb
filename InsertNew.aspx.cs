using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TeleAtendimentoWeb
{
    public partial class InsertNew : System.Web.UI.Page
    {
        private int Count = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Update.CreateTextBox( Count.ToString(), "", "", " ", PlaceHolder );
            DropDownList_estado.Items.AddRange(GetOptionsDropDownListEstado());

            if (Count < 1)
            {
                Btn_Remove_Telefone.Enabled = false;
            }
        }

        public static ListItem[] GetOptionsDropDownListTipoTelefone()
        {
            ListItem[] newList = {
                new ListItem("vazio", " "),
                new ListItem("celular"),
                new ListItem("residencial")
            };
            return newList;
        }
        
        public static ListItem[] GetOptionsDropDownListEstado()
        {
            ListItem[] newList = {
                new ListItem("Vazio", " "),
                new ListItem("AC", "AC"),
                new ListItem("AL", "AL"),
                new ListItem("AP", "AP"),
                new ListItem("AM", "AM"),
                new ListItem("BA", "BA"),
                new ListItem("CE", "CE"),
                new ListItem("DF", "DF"),
                new ListItem("ES", "ES"),
                new ListItem("GO", "GO"),
                new ListItem("MA", "MA"),
                new ListItem("MT", "MT"),
                new ListItem("MS", "MS"),
                new ListItem("MG", "MG"),
                new ListItem("PA", "PA"),
                new ListItem("PB", "PB"),
                new ListItem("PR", "PR"),
                new ListItem("PE", "PE"),
                new ListItem("PI", "PI"),
                new ListItem("RJ", "RJ"),
                new ListItem("RN", "RN"),
                new ListItem("RS", "RS"),
                new ListItem("RO", "RO"),
                new ListItem("RR", "RR"),
                new ListItem("SC", "SC"),
                new ListItem("SP", "SP"),
                new ListItem("SE", "SE"),
                new ListItem("TO", "TO"),
            };
            return newList;
        }
        
        protected void Btn_cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void Btn_ok_Click(object sender, EventArgs e)
        {
            string nome = Tb_Nome.Text;
            long numero = long.TryParse(Tb_Numero.Text, out long _) ? long.Parse(Tb_Numero.Text) : 0;
            long cep = long.TryParse(Tb_CEP.Text, out long _) ? long.Parse(Tb_CEP.Text) : 0;
            bool isNumberCPF = long.TryParse(Tb_CPF.Text, out long _);

            if (nome.Length > 0 && isNumberCPF)
            {
                List<Telefone> newTelefones = new List<Telefone>();
                for(int i = 0; i < 1; i++)
                {
                    TextBox dddTextBox = (TextBox)PlaceHolder.FindControl("Tb_DDD_" + i);
                    TextBox numeroTelefoneTextBox = (TextBox)PlaceHolder.FindControl("Tb_Numero_telefone_" + i);
                    DropDownList tipoTextBox = (DropDownList)PlaceHolder.FindControl("DropDownList_Tipo_" + i);

                    long numeroTelefone = long.TryParse(numeroTelefoneTextBox.Text, out long _) ? long.Parse(numeroTelefoneTextBox.Text) : 0;
                    long ddd = long.TryParse(dddTextBox.Text, out long _) ? long.Parse(dddTextBox.Text) : 0;

                    TipoTelefone newTipoTelefone = new TipoTelefone(
                        0, tipoTextBox?.SelectedValue.ToLower()
                    );
                    newTelefones.Add(new Telefone(
                            0, 
                            numeroTelefone,
                            ddd,
                            newTipoTelefone
                    ));
                }
                Endereco newEndereco =  new Endereco(
                    0,
                    Tb_Logradouro.Text,
                    numero,
                    cep,
                    Tb_Bairro.Text,
                    Tb_Cidade.Text,
                    DropDownList_estado.SelectedValue
                );
                Pessoa newPerson = new Pessoa(
                    0,
                    nome,
                    long.Parse(Tb_CPF.Text),
                    newEndereco,
                    newTelefones
                );

                Pessoa.Inserir(newPerson);
                Response.Redirect("Index.aspx");
            }
        }

        protected void Btn_Add_Telefone_Click(object sender, EventArgs e)
        {
            Count++;
            Update.CreateTextBox(Count.ToString(), "", "", " ", PlaceHolder);
            Btn_Remove_Telefone.Enabled = true;
        }

        protected void Btn_Remove_Telefone_Click(object sender, ImageClickEventArgs e)
        {
            if (Count > 0)
            {
                PlaceHolder.Controls.Remove(PlaceHolder.FindControl("container_"+Count));
                Count--;
            }
        }
    }
}