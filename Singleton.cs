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
    public string showMenu(string key)
    {
        Console.WriteLine("Escolha uma das opções abaixo: ");
        Console.WriteLine("1- Biblioteca ");
        Console.WriteLine("2- Secretaria ");
        Console.WriteLine("0- SAIR");
        key = Console.ReadLine();
        switch (key)
        {
            case "1":
                Console.WriteLine("1- Listar Livros");
                Console.WriteLine("2- Cadastrar Livro");
                Console.WriteLine("3- Atualizar Livro");
                Console.WriteLine("4- Remover Livro");

                break;

            case "2":
                Console.WriteLine("1- Listar Alunos");
                Console.WriteLine("2- Cadastrar Aluno");
                Console.WriteLine("3- Atualizar Aluno");
                Console.WriteLine("4- Remover Aluno");
                break;

            case "0":
                break;
            default: break;
        }
        return key;
    }

}
