using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

class Biblioteca
{
    public void showMenu(MySqlConnection connection, string key)
    {
        bool running = true;
        while (running)
        {
            Console.Write("Opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1": ListarLivros(connection); break;
                case "2":
                    Console.WriteLine("Digite o título do livro:");
                    string tituloLivro = Console.ReadLine();

                    Console.WriteLine("Digite o autor do livro:");
                    string autorLivro = Console.ReadLine();

                    Console.WriteLine("Digite o gênero do livro:");
                    string generoLivro = Console.ReadLine();

                    Console.WriteLine("Digite o ISBN do livro:");
                    string isbnLivro = Console.ReadLine();

                    Console.WriteLine("Digite o ano do livro:");
                    int anoLivro = int.Parse(Console.ReadLine());

                    Console.WriteLine("Digite a quantidade do livro:");
                    int quantidadeLivro = int.Parse(Console.ReadLine());

                    Console.WriteLine("Edição: ");
                    int edicao = int.Parse(Console.ReadLine());

                    CadastrarLivro(connection, tituloLivro, autorLivro, generoLivro, isbnLivro, anoLivro, quantidadeLivro, edicao);
                    break;

                case "3": AtualizarLivro(connection); break;
                case "4": RemoverLivro(connection); break;
                case "0": running = false; break;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        }
    }

    //




    static void ExecutarComando(MySqlConnection connection, string sql, Dictionary<string, object> parameters = null)
    {
        using var cmd = new MySqlCommand(sql, connection);

        if (parameters != null)
        {
            foreach (var parameter in parameters)
            {
                cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
            }
        }

        cmd.ExecuteNonQuery();
    }

    static void ListarLivros(MySqlConnection connection)
    {
        string sql = "SELECT * FROM Livro";
        using var cmd = new MySqlCommand(sql, connection);
        using MySqlDataReader rdr = cmd.ExecuteReader();

        Console.WriteLine("Lista de Livros:");
        while (rdr.Read())
        {
            Console.WriteLine($"Título: {rdr.GetString("titulo")}, Autor: {rdr.GetString("autor")}, Gênero: {rdr.GetString("genero")}, ISBN: {rdr.GetString("isbn")}, Ano: {rdr.GetInt32("ano")}, Quantidade: {rdr.GetInt32("quantidade")}, Edição: {(rdr.GetInt32("edicao"))}");
        }
    }

    static void CadastrarLivro(MySqlConnection connection, string titulo, string autor, string genero, string isbn, int ano, int quantidade, int edicao)
    {
        string sql = "INSERT INTO Livro (Titulo, Autor, Genero, ISBN, Ano, Quantidade, Edicao) VALUES (@titulo, @autor, @genero, @isbn, @ano, @quantidade, @edicao)";
        using var cmd = new MySqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("@titulo", titulo);
        cmd.Parameters.AddWithValue("@autor", autor);
        cmd.Parameters.AddWithValue("@genero", genero);
        cmd.Parameters.AddWithValue("@isbn", isbn);
        cmd.Parameters.AddWithValue("@ano", ano);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.Parameters.AddWithValue("@edicao", edicao);

        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Livro cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao cadastrar livro.");
        }
    }


    static void AtualizarLivro(MySqlConnection connection)
    {
        Console.WriteLine("Digite o ISBN do livro que deseja atualizar:");
        string isbn = Console.ReadLine();
        Console.WriteLine("Digite o nova quantidade do livro: ");
        string novaQuantidade = Console.ReadLine();
        Console.WriteLine("Digite o nova edição do livro: ");
        int novaEdicao = int.Parse(Console.ReadLine());

        string sql = "Update Livro SET edicao = @novaEdicao, quantidade = @novaQuantidade WHERE isbn = @isbn;";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@novaEdicao", novaEdicao);
        cmd.Parameters.AddWithValue("@novaQuantidade", novaQuantidade);
        cmd.Parameters.AddWithValue("@isbn", isbn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Livro atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Tente novamente!");
        }
    }

    static void RemoverLivro(MySqlConnection connection)
    {
        Console.WriteLine("Digite o ISBN do livro que deseja remover:");
        string isbn = Console.ReadLine();
        string sql = "DELETE FROM Livro WHERE isbn = @isbn";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@isbn", isbn);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Livro removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Tente novamente!");
        }
    }


}