using System;
using System.Data.SqlClient;
using System.Linq;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("___________________________________________");
            Console.WriteLine("Aplicação de Exemplo - Ler Arquivo CSV e importando no banco de dados");
            Console.WriteLine("___________________________________________");

            string connectionString = @"Data Source=FLORA-PC;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conexao = new SqlConnection(connectionString);
            conexao.Open();
            var transaction = conexao.BeginTransaction();
            try
            {
                StreamReader rd = new StreamReader(@"C:\Users\flora\Desktop\asd.csv");
                string linha = null;
                string[] linhaseparada = null;
                string linhaConcatenada = null;
                while ((linha = rd.ReadLine()) != null)
                {
                    //linhaConcatenada = linha.Replace(';', ',');
                    linhaseparada = linha.Split(';');
                    int i = 0;
                    foreach (var item in linhaseparada)
                    {
                        if (i == 0)
                            linhaConcatenada = "'" + item + "'" + ",";
                        else if (i < linhaseparada.Length - 1)
                            linhaConcatenada = linhaConcatenada + "'" + item + "'" + ",";
                        else
                            linhaConcatenada = linhaConcatenada + "'" + item + "'";
                        i++;
                    }
                    var sql = "INSERT INTO [clinica].[dbo].[convenios] VALUES(" + linhaConcatenada + ")";
                    SqlCommand cmd = new SqlCommand(sql, conexao, transaction);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine(linhaseparada.First().ToString());
                }
                transaction.Commit();
                rd.Close();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine("Erro ao executar Leitura do Arquivo" + e);
            }
            finally
            {
                Console.WriteLine("Fechando conexão");
                conexao.Close();
                Console.WriteLine("Conexão fechada");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}


