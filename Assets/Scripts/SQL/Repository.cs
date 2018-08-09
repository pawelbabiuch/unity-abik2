using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Repository : MonoBehaviour
{
    public static Repository ins;

    [HideInInspector]
    public List<User> users = new List<User>();
    [HideInInspector]
    public List<Result> results = new List<Result>();

    public int loggedUserID = -1;

    private void Awake()
    {
        ins = this;
        SceneManager.LoadScene("Menu_Unknow user", LoadSceneMode.Additive);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("UserID") && PlayerPrefs.GetInt("UserID") != -1)
            Debug.Log("Ustawić automatyczne logowanie w repozytorium");
    }

    public User GetUser(int userID)
    {
        return users.FirstOrDefault(x => x.id == userID);
    }

    public User GetUser(string userLogin)
    {
        return users.FirstOrDefault(x => x.login == userLogin);
    }

    public List<Result> GetUserResults(int userID)
    {
        return results.Where(x=>x.userID == userID).ToList();
    }
}
