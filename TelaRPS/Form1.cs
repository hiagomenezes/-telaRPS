using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

namespace TelaRPS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ListaNotas.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            StreamWriter MeuCaminho = null;

            SaveFileDialog GerarLote = new SaveFileDialog();
            //Escolher onde salvar o arquivo
            //GerarLote.InitialDirectory = @"C:\Users\hiago\Desktop\teste";
            GerarLote.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // metodo para filtra o tipo do arquivo que vai ser salvo
            GerarLote.Filter = "arquivos txt (*.txt)|*.txt";
            // codigo para retornar para o dioretorio quando salvar o arquivo
            // GerarLote.RestoreDirectory = true;
            // obtém ou define o título da caixa de diálogo do arquivo
            GerarLote.Title = "Lote RPS";


            // contar todas as colunas do datagridview
            int qtdColunas = ListaNotas.Columns.Count;

            if (GerarLote.ShowDialog() == DialogResult.OK)
            {
                //Pega o caminho do arquivo
                string caminho = GerarLote.FileName;
                //Cria um StreamWriter no local
                MeuCaminho = new StreamWriter(caminho);

                foreach (DataGridViewRow item in ListaNotas.Rows)
                {
                    if (Convert.ToBoolean(item.Cells[0].Value) == true)
                    {
                        string linha = null;
                        for (int i = 0; i < qtdColunas; i++)
                        {
                            linha += item.Cells[i].Value.ToString();
                        }
                        MeuCaminho.WriteLine(linha);
                    }
                }
                MeuCaminho.Close();
                /* DialogResult resposta =*/
                MessageBox.Show("Salvo com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                for (int i = ListaNotas.Rows.Count - 1; i >= 0; i--)
                {
                    if ((bool)ListaNotas.Rows[i].Cells[0].FormattedValue)
                    {
                        ListaNotas.Rows.Remove(ListaNotas.curr);
                    }
                }


            }

        }
        // carregar as informações para o data grid view
        private void Form1_Load(object sender, EventArgs e)
        {
            var listPessoas = new List<Pessoas>();
            var json = new Pessoas {
                nome = "Isis Teresinha Ayla da Costa",
                idade = 72,
                cpf = "341.805.323-87",
                rg = "16.590.266-8",
                data_nasc = "18/08/1947",
                signo = "Leão",
                mae = "Maitê Francisca",
                pai = "Iago Yuri da Costa",
                email = "isisteresinhaayladacosta__isisteresinhaayladacosta@unimedsjc.com.br",
                senha = "eTrlHtLeCe",
                cep = "24722-120",
                endereco = "Rua Itacaju",
                numero = 795,
                bairro = "Monjolos",
                cidade = "São Gonçalo",
                estado = "RJ",
                telefone_fixo = "(21) 2883-9258",
                celular = "(21) 98759-5002",
                altura = "1,75",
                peso = 46,
                tipo_sanguineo = "AB-",
                cor = "preto"
            };
            listPessoas.Add(json);

            // Variavel para peguar o caminho da API.
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:58132/");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //chamando a api pela url
            var message = client.GetAsync("weatherforecast").Result.Content.ReadAsStringAsync();
            //
            var response = client.PostAsJsonAsync("weatherforecast/Inova", listPessoas).Result.Content.ReadAsAsync<IEnumerable<Pessoas>>().Result;
            var teste = response;


            ListaNotas.DataSource = response;
            //ListaNotas.DataBind();



            //response.();

            // return URI of the created resource.
            // return response.Headers.Location;

            //List<Pessoas> pessoa = new List<Pessoas>();

            //foreach (var item in pessoa)
            //{
            //    if (true)
            //    {

            //    }
            //}


            //string[,] Linhas = new string[,]{
            // {"false","1","Hiago","2019-10-31","10"},
            // {"false","2","Wellington","2019-09-28","20"},
            // {"false","3","Denis","2019-08-26","30"},
            // {"false","4","Vanessa","2019-07-10","40"},
            // {"false","5","JWill","2019-06-15","50"},
            // {"false","6","Carlor","2019-05-20","60"},
            // {"false","7","Roberto","2019-04-01","70"}
            //};

            //// Linhas.GetLength(0)comprimento de retorno do primeiro D (7)
            //// Linhas.GetLength(1) comprimento de retorno do segundo D
            //for (int i = 0; i < Linhas.GetLength(0); i++)
            //{
            //    string[] Linha = new string[Linhas.GetLength(1)];

            //    for (int j = 0; j < Linhas.GetLength(1); j++)
            //    {
            //        Linha[j] = Linhas[i, j];
            //    }


            //    ListaNotas.Rows.Add(Linha);
            //    // função para não permiter que coloque a ultima linha 
            //    ListaNotas.AllowUserToAddRows = false;


            //    // Não permite que o usuario fazer alteração na tabela
            //    //ListaNotas.ReadOnly = true;

            //}

        }
        // fazer a validação para ativar o botão de gerar o lote
        private void ListaNotas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //var selecionar = (DataGridView)sender;
            //selecionar.EndEdit();

            //foreach (DataGridViewRow row in ListaNotas.Rows)
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        var cbxCell = (DataGridViewCheckBoxCell)selecionar.Rows[e.RowIndex].Cells[0];
            //        if ((bool)cbxCell.Value)
            //       // if (Convert.ToBoolean(cbxCell.Value))
            //        {
            //            button2.Enabled = true;
            //        }
            //        else
            //        {
            //            button2.Enabled = false;
            //        }
            //    }
            //}

            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                ListaNotas.CommitEdit(DataGridViewDataErrorContexts.Commit);
                var i = 0;
                foreach (DataGridViewRow row in ListaNotas.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[0].Value))
                    {
                        i++;
                    }
                }


                if (i > 0)
                {
                    button2.Enabled = true;
                }
                else
                {
                    button2.Enabled = false;
                }
            }


        }
        // botão para contar quantas linhas foram selecionadas
        private void btnlinha_Click(object sender, EventArgs e)
        {

            int Totallinhas = Convert.ToInt32(ListaNotas.Rows.Count.ToString());
            int selecionados = 0;
            for (int x = 0; x < Totallinhas; x++)
            {
                if (ListaNotas.Rows[x].Cells[0].Value.ToString() == "True")
                {
                    selecionados++;
                }
            }
            if (selecionados == 0) MessageBox.Show("Nenhum linha foi selecionada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if (selecionados > 0) MessageBox.Show(selecionados + " linha(s) selecionada(s)", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        // check para selecionar todas as linhas
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow item in ListaNotas.Rows)
            //{
            //    item.Cells[0].Value = (sender as CheckBox).Checked;

            //}

            for (int i = 0; i < 50; i++)
            {
                ListaNotas.Rows[i].Cells[0].Value = (sender as CheckBox).Checked;
            }

            if (checkBox1.Checked == true)
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }


    }

}
