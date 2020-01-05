using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Security.Cryptography;
using System.Linq;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    private DatabaseScript databaseAccess;
    private Initialisation variableAccess;
    private InterfaceScript interfaceScriptAccess;
    public CanvasGroup usernameExists;
    public CanvasGroup passwordInvalid;
    public CanvasGroup blanks;
    public CanvasGroup adminCodeError;
    public CanvasGroup login;
    public CanvasGroup register;
    public CanvasGroup adminCodePanel;
    public InputField usernameInput;
    public InputField passwordInput;
    public InputField fullNameInput;
    public InputField adminCodeInput;
    public Button adminCodeButton;
    List<User> users = new List<User>();
    User newAccount = new User();

    void Start()
    {
        databaseAccess = FindObjectOfType<DatabaseScript>();
        variableAccess = FindObjectOfType<Initialisation>();
        interfaceScriptAccess = FindObjectOfType<InterfaceScript>();
    }
    public void Authentication()
    {     
        users = databaseAccess.ReadInAccounts(users);
        string username = usernameInput.text;
        string password = passwordInput.text;
        string fullName = fullNameInput.text;
        if (CheckUsername(username) == false) { interfaceScriptAccess.CloseOpenInterface(usernameExists); }
        else { CheckPassword(password); newAccount.username = username; }
        if (CheckPassword(password) == false) { interfaceScriptAccess.CloseOpenInterface(passwordInvalid); }
        else { CreatePassword(password); }
        if (CheckFullname(fullName) == false) { interfaceScriptAccess.CloseOpenInterface(blanks); }
        else { newAccount.fullName = fullName; }
        adminCodeButton.onClick.AddListener(CheckAdminCode);

    }
    public bool CheckUsername(string username)
    {
        bool valid = true;
        if (!string.IsNullOrWhiteSpace(username))
        {
            foreach (User user in users)
            {
                if (user.username.Equals(username)) { valid = false; }
            }
        }
        return valid;
    }

    public void CreatePassword(string password)
    {
        newAccount.passwordSalt = CreateSalt();
        string unhashedPassword = password + newAccount.passwordSalt;
        var crypt = new SHA256Managed();
        var hash = new StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(unhashedPassword));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        newAccount.hashedPassword = hash.ToString();
    }

    public bool CheckPassword(string password)
    {
        string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,20}$";
        Regex rg = new Regex(pattern);
        if (!string.IsNullOrWhiteSpace(pattern)) { return Regex.IsMatch(password, pattern); }
        else { return false; }
    }

    private string CreateSalt()
    {
        string validCharacters = "./abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string s = "";
        using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
        {
            while (s.Length != 128)
            {
                byte[] oneByte = new byte[1];
                provider.GetBytes(oneByte);
                char c = (char)oneByte[0];
                if (validCharacters.Contains(c))
                {
                    s += c;
                }
            }
        }
        return s;
    }

    public bool CheckFullname(string fullName)
    {
        if (!string.IsNullOrEmpty(fullName)) { return true; }
        else { return false; }
    }

    public void CheckAdminCode()
    {
        bool valid = false;
        while (valid == false)
        {
            if (!adminCodeInput.text.Equals("PGSCSTAFF")) { interfaceScriptAccess.CloseOpenInterface(adminCodeError); adminCodeInput.text = ""; }
            else { valid = true; }
        }
        if (valid == true) { users.Add(newAccount); databaseAccess.SaveNewAccount(newAccount); interfaceScriptAccess.CloseOpenInterface(adminCodePanel); interfaceScriptAccess.CloseOpenInterface(login); }
        usernameInput.text = "";
        passwordInput.text = "";
        adminCodeInput.text = "";
        fullNameInput.text = "";
    }

}
