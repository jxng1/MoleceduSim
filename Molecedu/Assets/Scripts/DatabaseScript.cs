using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class DatabaseScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<User> ReadInAccounts(List<User> users)
    {
        string connectionString = "URI=file:" + Application.dataPath + "/Databases/UsersInfo.db"; //Connection to db.
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            dbConnection.Open();
            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
                string sqlQuery = "SELECT * " + "FROM Users";
                dbCommand.CommandText = sqlQuery;
                IDataReader reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    string username = (string)reader["username"].ToString().Replace(" ", string.Empty);
                    string hashedPassword = (string)reader["hashedPassword"].ToString().Replace(" ", string.Empty);
                    string passwordSalt = (string)reader["passwordSalt"].ToString().Replace(" ", string.Empty);
                    string fullName = (string)reader["fullName"];
                    User user = new User(username, fullName, hashedPassword, passwordSalt);
                    users.Add(user);
                }
            }
            dbConnection.Close();
            return users;
        } //READS ACCOUNTS FROM THE DATABASE
    }

    public void SaveNewAccount(User newAccount)
    {
        string connectionString = "URI=file:" + Application.dataPath + "/Databases/UsersInfo.db"; //Connection to db.
        using (IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
            {
                dbConnection.Open();
                using (IDbCommand dbCommand = dbConnection.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO Users VALUES ('" + newAccount.username + "','" + newAccount.hashedPassword + "','" + newAccount.passwordSalt + "','" + newAccount.fullName + "')";
                    dbCommand.CommandText = sqlQuery;
                    dbCommand.ExecuteNonQuery();
                }
            }
            dbConnection.Close();
        }
    }




}

