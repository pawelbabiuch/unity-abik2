using UnityEngine;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{
    public Transform content;
    public ColorBlock doneColor, normalColor;

    private int levelsCount = 0;

    private void Start()
    {

        levelsCount = Repository.ins.GetUserResults(Repository.ins.loggedUserID).Count;

        for (int i = 0; i < content.childCount; i++)
        {
            Button btn = content.GetChild(i).GetComponent<Button>();
            if(i < levelsCount)
            {
                btn.interactable = true;
                btn.colors = doneColor;
            }
            else if(i == levelsCount)
            {
                btn.interactable = true;
                btn.colors = normalColor;
            }
            else
            {
                btn.interactable = false;
            }
        }
    }
}
