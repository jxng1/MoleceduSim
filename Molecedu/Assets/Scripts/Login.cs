using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;

public class Login : MonoBehaviour
{
    private DatabaseScript databaseAccess;
    private InterfaceScript interfaceScriptAccess;
    public InputField usernameInput;
    public InputField passwordInput;
    public CanvasGroup error;
    public CanvasGroup adminPanel;

    private void Start()
    {
        databaseAccess = FindObjectOfType<DatabaseScript>();
        interfaceScriptAccess = FindObjectOfType<InterfaceScript>();
    }

    public void Authentication()
    {
        List<User> users = new List<User>();
        users = databaseAccess.ReadInAccounts(users);
        string username = usernameInput.text;
        string password = passwordInput.text;
        if (CheckLogin(users, username, password) == true) { interfaceScriptAccess.CloseOpenInterface(adminPanel); }
        else { interfaceScriptAccess.CloseOpenInterface(error); }
    }

    public bool CheckLogin(List<User> users, string username, string password)
    {
        User account = new User();
        bool valid = false;
        if (!string.IsNullOrWhiteSpace(username)) { if (CheckUsername(users, account, username) == true) { valid = true; }; }
        if (valid == true)
        {
            if (!string.IsNullOrWhiteSpace(password)) { if (CheckPassword(account, password) == false) { valid = false; }; }
        }
        return valid;
    }

    public bool CheckUsername(List<User> users, User account, string username)
    {
        bool valid = false;
        foreach (User user in users)
        {
            if (username.Equals(user.username.ToString())) { valid = true; account.username = user.username; account.hashedPassword = user.hashedPassword; account.passwordSalt = user.passwordSalt; account.fullName = user.fullName; }
        }
        return valid;
    }

    public bool CheckPassword(User account, string password)
    {
        bool valid = false;
        if (HashPassword(account, password) == true) { valid = true; }
        return valid;
    }

    public bool HashPassword(User account, string password)
    {
        bool valid = false;
        string test = password + account.passwordSalt;
        var crypt = new SHA256Managed();
        var hash = new StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(test));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        if (hash.ToString().Equals(account.hashedPassword)) { valid = true; }
        return valid;
    }

}