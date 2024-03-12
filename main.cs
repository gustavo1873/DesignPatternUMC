using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

class Program
{
    public static void Main(string[] args)
    {

        //string cs = @"server=sql.freedb.tech;userid=freedb_aluno;password=&ta2Vz@C5?EnXbD;database=freedb_db_aula01";

        using var con = new MySqlConnection();
        try
        {
            con.Open();
            Console.WriteLine($"MySQL version : {con.ServerVersion}");
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message.ToString());
        }

        if (con.State == ConnectionState.Open)
        {
            Console.WriteLine("Connected!");
            //using var cmd = new MySqlCommand(sql, con);
            //using MySqlDataReader rdr = cmd.ExecuteReader();
            //Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10} {rdr.GetName(2),10}");
            //cmd.CommandText = @"CREATE TABLE Teste ( teste                  varchar(100) PRIMARY KEY);";
            //cmd.ExecuteNonQuery();
        }




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

        bool running = true;
        while (running)
        {
            Console.Write("Opção: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1": ListarLivros(con); break;
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

                    CadastrarLivro(con, tituloLivro, autorLivro, generoLivro, isbnLivro, anoLivro, quantidadeLivro, edicao);
                    break;

                case "3": AtualizarLivro(con); break;
                case "4": RemoverLivro(con); break;
                case "5": ListarAlunos(con); break;
                case "6":
                    Console.WriteLine("Digite o nome do aluno:");
                    string nome = Console.ReadLine();

                    Console.WriteLine("Digite o RGM do aluno:");
                    string rgm = Console.ReadLine();

                    Console.WriteLine("Digite a data de nascimento do aluno (dd/mm/yyyy):");
                    DateTime dataNascimento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    Console.WriteLine("Digite o curso do aluno:");
                    string curso = Console.ReadLine();

                    Console.WriteLine("Digite o RG do aluno:");
                    string rg = Console.ReadLine();

                    Console.WriteLine("Digite o gênero do aluno:");
                    string genero = Console.ReadLine();

                    Console.WriteLine("O aluno é bolsista? (sim ou nao):");
                    bool bolsista = Console.ReadLine().Equals("sim", StringComparison.OrdinalIgnoreCase);

                    CadastrarAluno(con, nome, rgm, dataNascimento, curso, rg, genero, bolsista);
                    break;
                case "7": AtualizarAluno(con); break;
                case "8": RemoverAluno(con); break;
                case "9": running = false; break;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        }

        con.Close();
    }

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

    static void ListarAlunos(MySqlConnection connection)
    {
        string sql = "SELECT * FROM Aluno";
        using var cmd = new MySqlCommand(sql, connection);
        using MySqlDataReader rdr = cmd.ExecuteReader();

        Console.WriteLine("Lista de Alunos:");
        while (rdr.Read())
        {
            Console.WriteLine($"Nome: {rdr.GetString("nome")}, RGM: {rdr.GetString("rgm")}, Data de Nascimento: {rdr.GetDateTime("dataNascimento").ToString("dd/MM/yyyy")}, Curso: {rdr.GetString("curso")}, RG: {rdr.GetString("rg")}, Gênero: {rdr.GetString("genero")}, Bolsista: {(rdr.GetBoolean("bolsista") ? "Sim" : "Não")}");
        }
    }

    static void CadastrarAluno(MySqlConnection connection, string nome, string rgm, DateTime dataNascimento, string curso, string rg, string genero, bool bolsista)
    {

        string sql = "INSERT INTO Aluno (Nome, RGM, DataNascimento, Curso, Rg, Genero, Bolsista) VALUES (@nome, @rgm, @dataNascimento, @curso, @rg, @genero, @bolsista)";


        using var cmd = new MySqlCommand(sql, connection);


        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@rgm", rgm);
        cmd.Parameters.AddWithValue("@dataNascimento", dataNascimento);
        cmd.Parameters.AddWithValue("@curso", curso);
        cmd.Parameters.AddWithValue("@rg", rg);
        cmd.Parameters.AddWithValue("@genero", genero);
        cmd.Parameters.AddWithValue("@bolsista", bolsista);


        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Aluno cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao cadastrar aluno.");
        }
    }


    static void AtualizarAluno(MySqlConnection connection)
    {
        Console.WriteLine("Digite o RGM do aluno que deseja atualizar:");
        string rgm = Console.ReadLine();
        Console.WriteLine("Digite o novo curso do aluno: ");
        string novoCurso = Console.ReadLine();

        string sql = "Update Aluno SET curso = @novoCurso WHERE rgm = @rgm";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@novoCurso", novoCurso);
        cmd.Parameters.AddWithValue("@rgm", rgm);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Aluno atualizado com sucesso!");
        }
        else
        {
            Console.WriteLine("Tente novamente!");
        }
    }

    static void RemoverAluno(MySqlConnection connection)
    {
        Console.WriteLine("Digite o RGM do aluno que deseja remover:");
        string rgm = Console.ReadLine();
        string sql = "DELETE FROM Aluno WHERE rgm = @rgm";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@rgm", rgm);
        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Aluno removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Tente novamente!");
        }
    }

}

