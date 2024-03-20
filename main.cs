using System;
using MySql.Data.MySqlClient;

class Program
{
    public static void Main(string[] args)
    {
        Singleton s1 = Singleton.getInstance();
        Biblioteca biblioteca = new Biblioteca();
        Secretaria secretaria = new Secretaria();

        Conexao conexao = new Conexao("sql.freedb.tech", "freedb_aluno", "&ta2Vz@C5?EnXbD", "freedb_db_aula01");
        using (MySqlConnection connection = conexao.abrirConexao())
        {
            if (connection != null)
            {
                Console.WriteLine("Conexão aberta com sucesso!");
            }

            s1.showMenu("key");
            biblioteca.showMenu(connection, "key");
            secretaria.showMenu(connection, "key");
        }
        conexao.fecharConexao();

    }

}

