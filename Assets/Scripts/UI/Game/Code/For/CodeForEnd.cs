using UnityEngine;

public class CodeForEnd : MoveableCode
{
    [HideInInspector]
    public Transform codeFor;
    private int CodeNumber = 0;

    public RectTransform arrow;

    private int distance
    {
        get
        {
            int codeForY = codeFor.GetSiblingIndex();
            int myLastY = transform.GetSiblingIndex();
            return  myLastY - codeForY - 1;
        }
    }

    public void SetUpArrow()
    {
        Debug.Log(distance);

        if(distance > 0)
        {
            arrow.gameObject.SetActive(true);
            arrow.sizeDelta = new Vector2(arrow.sizeDelta.x, distance * 60 + 20);
        }
        else if(arrow.gameObject.activeInHierarchy)
        {
            arrow.gameObject.SetActive(false);
        }
    }

    public override int codeNumber
    {
        get
        {
            return CodeNumber;
        }

        set
        {
            CodeNumber = value;
        }
    }
}
