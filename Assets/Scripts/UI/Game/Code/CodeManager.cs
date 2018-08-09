using UnityEngine;

public class CodeManager : MonoBehaviour
{
    public Transform content;
    public Transform emptyCode;
    public Transform lastCode;

    [HideInInspector]
    public Transform selectedCode;


    public static CodeManager ins;

    private void Awake()
    {
        ins = this;
    }
}
