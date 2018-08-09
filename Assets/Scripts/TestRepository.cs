using System;
using UnityEngine;

public class TestRepository : MonoBehaviour
{
    public string UID;
    private System.Random random;

    private void Awake()
    {
        if (string.IsNullOrEmpty(UID))
            UID = DateTime.Now.ToString();

        random = new System.Random(UID.GetHashCode());

        AddUsers();
        AddResults();
    }

    private void AddUsers()
    {
        User.AddUser("Adam", "assdg");
        User.AddUser("Marek", "a1sdg");
        User.AddUser("Anna", "asdg");
        User.AddUser("Marta", "asdgs");
        User.AddUser("Szymon", "asdg");
        User.AddUser("Dawid", "asdg");
        User.AddUser("Nikolas", "asdgt");
        User.AddUser("Sylwia", "sdfdsf");
        User.AddUser("Iza", "asdes");
        User.AddUser("Marcin", "terdf");
        User.AddUser("Adam1", "s3sdf");
        User.AddUser("Login", "haslo");
        User.AddUser("B", "haslo");
        User.AddUser("C", "haslo");
        User.AddUser("D", "haslo");
        User.AddUser("E", "haslo");
        User.AddUser("A", "1");
    }

    private void AddResults()
    {
        for (int i = 0; i < 50; i++)
        {
            int levelID = random.Next(4);
            int userID = random.Next(Repository.ins.users.Count);
            int lines = random.Next(3, 15);
            int time = random.Next(15, 180);

            User user = Repository.ins.GetUser(userID);

            Result.AddResult(levelID, user, lines, time);
        }
    }
}
