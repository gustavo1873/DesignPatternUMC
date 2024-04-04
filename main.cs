using System;
using MySql.Data.MySqlClient;

class Program
{
    public static void Main(string[] args)
    {
        Singleton s1 = Singleton.getInstance();
        Biblioteca biblioteca = new Biblioteca();
        Secretaria secretaria = new Secretaria();

        Conexao conexao = new Conexao("sql.freedb.tech", "freedb_werek", "s&hsGGk%p%V#XE5", "freedb_freedb_umc_5b_24");
        using (MySqlConnection connection = conexao.abrirConexao())
        {
            if (connection != null)
            {
                Console.WriteLine("Conexão aberta com sucesso!\n\n");
            }

            s1.showMenu("MAIN_MENU");
            string key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    key = s1.showMenu("MENU_BIBLIOTECA");
                    biblioteca.showMenu(connection, key);
                    break;
                case "2":
                    key = s1.showMenu("MENU_SECRETARIA");
                    secretaria.showMenu(connection, key);
                    break;

            }

        }
        conexao.fecharConexao();
    }

}