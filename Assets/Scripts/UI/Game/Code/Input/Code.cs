using UnityEngine;
using UnityEngine.UI;

public class Code : MoveableCode 
{
    [HideInInspector]
    public CodeType codeType;

    public string codeText
    {
        get { return transform.Find("Task").GetComponentInChildren<Text>().text; }
        set { transform.Find("Task").GetComponentInChildren<Text>().text = value; }
    }

    public override int codeNumber
    {
        get
        {
            string text = transform.Find("Text_Number").GetComponent<Text>().text;
            return int.Parse(text);
        }

        set
        {
            transform.Find("Text_Number").GetComponent<Text>().text = value.ToString();
        }
    }
}