using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Olá!");

            Console.WriteLine("___________________________________________");
            Console.WriteLine();
            Console.WriteLine("Aplicação de Exemplo MSDN - Ler Arquivo CSV");
            Console.WriteLine();
            Console.WriteLine("___________________________________________");
            try
            {
                //Declaro o StreamReader para o caminho onde se encontra o arquivo 
                StreamReader rd = new StreamReader(@"C:\Users\flora\Desktop\asd.csv");
                //Declaro uma string que será utilizada para receber a linha completa do arquivo 
                string linha = null;
                //Declaro um array do tipo string que será utilizado para adicionar o conteudo da linha separado 
                string[] linhaseparada = null;
                //realizo o while para ler o conteudo da linha 
                while ((linha = rd.ReadLine()) != null)
                {
                    //com o split adiciono a string 'quebrada' dentro do array 
                    linhaseparada = linha.Split(';');
                    //aqui incluo o método necessário para continuar o trabalho 
                    Console.WriteLine(linhaseparada.First().ToString());

                }
                rd.Close();
            }
            catch
            {
                Console.WriteLine("Erro ao executar Leitura do Arquivo");
            }


            string connectionString = @"Data Source=convenio.database.windows.net;Initial Catalog=Convenio;Integrated Security=False;User ID=convenio;Password=fl0r@143;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            SqlConnection conexao = new SqlConnection(connectionString); /* conexao irá conectar o C# ao banco de dados */
            SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Table]", conexao); /*cmd possui mais de um parâmetro, neste caso coloquei o comando SQL "SELECT * FROM tabela" que irá selecionar tudo(*) de tabela, o segundo parâmetro indica onde o banco está conectado,ou seja se estamos selecionando informações do banco precisamos dizer onde ele está localizado */
            //Tenta executar o que estiver abaixo
            try
            {
                conexao.Open(); // abre a conexão com o banco   
                cmd.ExecuteNonQuery();  
                SqlDataAdapter da = new SqlDataAdapter(); /* Aadapta o banco de dados ao nosso projeto */
                DataSet ds = new DataSet();
                da.SelectCommand = cmd; // adapta cmd ao projeto
                da.Fill(ds); // preenche todas as informações dentro do DataSet                                          
                //dataGridView1.DataSource = ds; //Datagridview recebe ds já preenchido
                //dataGridView1.DataMember = ds.Tables[0].TableName;
                Console.WriteLine(ds.Tables[0].TableName);
                Console.WriteLine(ds.Tables[0].Columns[0].ColumnName);
                Console.WriteLine(ds.Tables[0].Columns[1].ColumnName);

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            Console.WriteLine(row[column]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "erro");
                Console.ReadKey();
            }
            finally
            {
                Console.WriteLine("Fechando conexão");
                conexao.Close();
                Console.WriteLine("Conexão fechada");
            }


            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}

