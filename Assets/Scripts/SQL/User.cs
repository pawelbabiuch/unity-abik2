using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class User
{
    public int id { get; private set; }
    public string login { get; private set; }
    public string password { get; private set; }

    private User(string login, string password)
    {
        this.id = Repository.ins.users.Count;
        this.login = login;
        this.password = password;

        Repository.ins.users.Add(this);
    }

    public static void AddUser(string login, string password)
    {
        User findUser = Repository.ins.users.FirstOrDefault(x=>x.login == login);

        if (findUser != null)
            Debug.LogWarning("User o podanym loginie już istnieje. Konto nie zostało założone");
        else
            new User(login, password);
    }

    public List<Result> userResults
    {
        get
        {
            return Repository.ins.results.Where(x=>x.userID == this.id).ToList();
        }
    }

    /// <summary>
    /// Porównuje czy użytkowicy mają taki sam login i hasło
    /// </summary>
    /// <returns>true jeżeli login, password jest takie samo</returns>
    public override bool Equals(object obj)
    {
        bool equals = false;

        if (obj is User)
        {
            User user = (User)obj;

            if (this.login == user.login && this.password == user.password)
                equals = true;
        }

        return equals;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


}
