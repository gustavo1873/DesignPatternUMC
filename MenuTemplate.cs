using System;
using System.Data;
using MySql.Data.MySqlClient;

public abstract class MenuTemplate
{
    public void Run(MySqlConnection connection, string key)
    {
       LimparTela();
       ExecutarAcoes(connection, key);
    }

    protected void LimparTela()
    {
        Console.Clear();
    }
  
  public abstract string ExecutarAcoes(MySqlConnection connection, string key);

}