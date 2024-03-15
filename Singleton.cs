using System;

public sealed class Singleton
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
  //return string menu
    public string menu()
    {
        Console.WriteLine("Escolha uma das opções abaixo: ");

        Console.WriteLine("1- Listar livros ");
        Console.WriteLine("2- Cadastrar livro ");
        Console.WriteLine("3- Atualizar livro ");
        Console.WriteLine("4- Excluir livro");
        Console.WriteLine("5- Listar aluno");
        Console.WriteLine("6- Cadastrar aluno");
        Console.WriteLine("7- Atualizar aluno");
        Console.WriteLine("8- Excluir aluno");
        Console.WriteLine("9- SAIR");
      return Console.ReadLine();
    }
}