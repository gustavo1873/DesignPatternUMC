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

            s1.ExecutarAcoes(connection, "MAIN_MENU");
            string key = Console.ReadLine();
          do
            {
                switch (key)
                {
                case "1":
                        
                        key = s1.ExecutarAcoes(connection, "MENU_BIBLIOTECA");
                  biblioteca.ExecutarAcoes(connection, key);
                        break;
                case "2":
                        key = s1.ExecutarAcoes(connection, "MENU_SECRETARIA");
                  secretaria.ExecutarAcoes(connection, key);
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        key = "MAIN_MENU";
                        break;
                }
            } while (key != "0");

        }
        conexao.fecharConexao();
    }

}