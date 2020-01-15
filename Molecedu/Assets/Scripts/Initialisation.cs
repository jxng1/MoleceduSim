using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Initialisation : MonoBehaviour
{
    private DatabaseScript databaseAccess;
    public List<User> users = new List<User>(); //CREATES LIST OF USERS
    public List<Model> models = new List<Model>(); //CREATES LIST OF MODELS
    public List<Question> questions = new List<Question>(); // CREATE LIST OF QUESTIONS

    private void Start()
    {
        CheckDatabaseFilesExist();
        databaseAccess = FindObjectOfType<DatabaseScript>();
        GameObject[] interfaces = GameObject.FindGameObjectsWithTag("Interfaces");
        CanvasGroup[] canvasGroups = new CanvasGroup[interfaces.Length];
        for (int i = 0; i < interfaces.Length; i++)
        {
            canvasGroups[i] = interfaces[i].GetComponent<CanvasGroup>();
        }
        HideAll(canvasGroups);

        //foreach (User user in databaseAccess.ReadInAccounts(users))//prints each user out from list
        //{
        //    Debug.Log("Printing...");
        //    Debug.Log(user.username + user.fullName + user.hashedPassword + user.passwordSalt);
        //}

    }

    private void HideAll(CanvasGroup[] canvasGroups)
    {
        foreach (CanvasGroup _ in canvasGroups)
        {
            _.alpha = 0f;
            _.interactable = false;
            _.blocksRaycasts = false;
        }
    }

    private void CheckDatabaseFilesExist()
    {
        string tmpConnectionString = "URI=file:" + Application.dataPath + "/Databases/Database.db";
        if (!File.Exists("Database.db"))
        {
            SqliteConnection.CreateFile("Database.db"); //COULD BE OPTIMISED LATER   
            using (IDbConnection dbConnection = new SqliteConnection(tmpConnectionString))
            {
                {
                    dbConnection.Open();
                    using (IDbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        string sqlQuery = "CREATE TABLE Models (modelID INTEGER PRIMARY KEY AUTOINCREMENT, molecularFormula TEXT, IUPACName TEXT, synonyms TEXT, molecularWeight REAL, physicalDesc TEXT, boilingPoint REAL, meltingPoint REAL, solubility TEXT)";
                        dbCommand.CommandText = sqlQuery;
                        dbCommand.ExecuteNonQuery();
                    }
                }
                dbConnection.Close();
            }
            using (IDbConnection dbConnection = new SqliteConnection(tmpConnectionString))
            {
                {
                    dbConnection.Open();
                    using (IDbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        string sqlQuery = "CREATE TABLE Questions (questionID INTEGER PRIMARY KEY AUTOINCREMENT, difficulty TEXT, question TEXT, correctAnswer TEXT, incorrectAnswer1 TEXT, incorrectAnswer2 TEXT, incorrectAnswer3 TEXT)";
                        dbCommand.CommandText = sqlQuery;
                        dbCommand.ExecuteNonQuery();
                    }
                }
                dbConnection.Close();
            }
            using (IDbConnection dbConnection = new SqliteConnection(tmpConnectionString))
            {
                {
                    dbConnection.Open();
                    using (IDbCommand dbCommand = dbConnection.CreateCommand())
                    {
                        string sqlQuery = "CREATE TABLE Users( username TEXT, hashedPassword TEXT, passwordSalt TEXT, fullName TEXT, PRIMARY KEY(username))";
                        dbCommand.CommandText = sqlQuery;
                        dbCommand.ExecuteNonQuery();
                    }
                }
                dbConnection.Close();
            }
            Debug.Log("Success!");
        }
    }

}

public class User
{
    public string username { get; set; }
    public string fullName { get; set; }
    public string hashedPassword { get; set; }
    public string passwordSalt { get; set; }

    public User()
    {
        this.username = "";
        this.fullName = "";
        this.hashedPassword = "";
        this.passwordSalt = "";
    }
    public User(string username, string fullName, string hashedPassword, string passwordSalt)
    {
        this.username = username;
        this.fullName = fullName;
        this.hashedPassword = hashedPassword;
        this.passwordSalt = passwordSalt;
    }
    public void SetNewDetails(string newUsername, string newFullName, string newPassword)
    {
        this.username = newUsername;
    }
    public User ReturnDetails()
    {
        return new User(this.username, this.fullName, this.hashedPassword, this.passwordSalt);
    }

}
public class Model
{
    public int modelID { get; set; }
    public string molecularFormula { get; set; }
    public string iupacName { get; set; }
    public string synonyms { get; set; }
    public float molecularWeight { get; set; }
    public string physicalDescription { get; set; }
    public float boilingPoint { get; set; }
    public float meltingPoint { get; set; }
    public string solubility { get; set; }
    public Model(int modelID, string molecularFormula, string iupacName, string synonyms, float molecularWeight, string physicalDescription, float boilingPoint, float meltingPoint, string solubility)
    {
        this.modelID = modelID;
        this.molecularFormula = molecularFormula;
        this.iupacName = iupacName;
        this.synonyms = synonyms;
        this.molecularWeight = molecularWeight;
        this.physicalDescription = physicalDescription;
        this.boilingPoint = boilingPoint;
        this.meltingPoint = meltingPoint;
        this.solubility = solubility;
    }
    public Model ReturnDetails()
    {
        return new Model(this.modelID, this.molecularFormula, this.iupacName, this.synonyms, this.molecularWeight, this.physicalDescription, this.boilingPoint, this.meltingPoint, this.solubility);
    }
}
public class Question
{
    public int questionID;
    public string difficulty;
    public string question;
    public string correctAnswer;
    public string incorrectAnswer1;
    public string incorrectAnswer2;
    public string incorrectAnswer3;

    public Question(int questionID, string difficulty, string question, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
    {
        this.questionID = questionID;
        this.difficulty = difficulty;
        this.question = question;
        this.correctAnswer = correctAnswer;
        this.incorrectAnswer1 = incorrectAnswer1;
        this.incorrectAnswer2 = incorrectAnswer2;
        this.incorrectAnswer3 = incorrectAnswer3;
    }

    public Question ReturnDetails()
    {
        return new Question(questionID, difficulty, question, correctAnswer, incorrectAnswer1, incorrectAnswer2, incorrectAnswer3);
    }


}



