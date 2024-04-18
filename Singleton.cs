
using System;
using System.Data;
using MySql.Data.MySqlClient;

public sealed class Singleton : MenuTemplate
{
    private Singleton() { }
    private static Singleton _instance;
    public static Singleton getInstance()
    {
        if (_instance == null)
        {
            _instance = new Singleton();
        }
        return _instance;
    }

  public override string ExecutarAcoes(MySqlConnection connection, string key)
  {
      switch (key)
      {
          case "MAIN_MENU":
              Console.WriteLine("1- Biblioteca ");
              Console.WriteLine("2- Secretaria ");
              Console.WriteLine("0- SAIR");
              Console.Write("Escolha uma das opções acima: ");
              break;
          case "MENU_BIBLIOTECA":
              Console.WriteLine("1- Listar Livros");
              Console.WriteLine("2- Cadastrar Livro");
              Console.WriteLine("3- Atualizar Livro");
              Console.WriteLine("4- Remover Livro\n\n");
              //Console.WriteLine("5- Emprestar Livro\n\n");
             //Console.WriteLine("6- Venda de Livro\n\n");
              Console.WriteLine("0- SAIR");
              break;
          case "MENU_SECRETARIA":
              Console.WriteLine("1- Listar Alunos");
              Console.WriteLine("2- Cadastrar Aluno");
              Console.WriteLine("3- Atualizar Aluno");
              Console.WriteLine("4- Remover Aluno\n\n");
              Console.WriteLine("0- SAIR");
              break;
          case "0":
              Console.WriteLine("Você saiu do sistema");
              break;
          default:
              Console.WriteLine("Opção inválida!");
              break;
      }
        return Console.ReadLine();
  }

}
