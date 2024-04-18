using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

class Secretaria : MenuTemplate
{
    public override string ExecutarAcoes(MySqlConnection connection, string key)
    {
        bool running = true;
            Console.Write("Opção: ");
        while (running)
        {
            string option = Console.ReadLine();

            switch (option)
            {
            case "1": ListarAlunos(connection); break;
            case "2":
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

                    CadastrarAluno(connection, nome, rgm, dataNascimento, curso, rg, genero, bolsista);
              
                    break;
            case "3": AtualizarAluno(connection); break;
            case "4": RemoverAluno(connection); break;
            case "0": running = false; return "MAIN_MENU";
                default: Console.WriteLine("Opção inválida!"); break;
              
            }
          
        }
      return null;
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