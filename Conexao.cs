using System;
using MySql.Data.MySqlClient;
using System.Data;

class Conexao
{
    private string connectionString;
    private MySqlConnection connection;

    public Conexao(string server, string userid, string password, string database)
    {

        connectionString = $"server={server};userid={userid};password={password};database={database}";
        connection = new MySqlConnection(connectionString);

    }
    public MySqlConnection abrirConexao()
    {
        try
        {
            connection.Open();
            return connection;
        }
        catch (MySqlException ex)
        {

            Console.WriteLine($"Erro ao abrir conexão. {ex.Message}");
            return null;
        }
    }
    public void fecharConexao()
    {
        if (connection != null && connection.State != ConnectionState.Closed)
        {
            connection.Close();
            Console.WriteLine("Conexão fechada com sucesso!");
        }
    }
}