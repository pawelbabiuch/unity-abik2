using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTask : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> results = new List<GameObject>();
    public int maxResultsCount = 10;

    public GameObject resultView;
    public Transform contentParent;

    public void AddResult(Result result)
    {
        User user = Repository.ins.GetUser(result.userID);

        GameObject newResult = Instantiate(resultView, contentParent);
        GetNumber(newResult.transform).text = results.Count.ToString();
        GetLogin(newResult.transform).text = user.login;
        GetLines(newResult.transform).text = string.Format("{0} linii kodu", result.codeLines);
        GetTime(newResult.transform).text = TimeOnMinutes(result.codeTime);

        results.Add(newResult);
    }

    private Text GetNumber(Transform result)
    {
        return result.Find("Number").GetComponent<Text>();
    }

    private Text GetLogin(Transform result)
    {
        return result.Find("Login").GetComponent<Text>();
    }

    private Text GetLines(Transform result)
    {
        return result.Find("CodeLines").GetComponent<Text>();
    }

    private Text GetTime(Transform result)
    {
        return result.Find("CodeTime").GetComponent<Text>();
    }

    private string TimeOnMinutes(int sec)
    {
        TimeSpan t = TimeSpan.FromSeconds(sec);

        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
}
