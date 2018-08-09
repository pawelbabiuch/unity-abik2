using UnityEngine;
using UnityEngine.UI;

public class PanelSuccess : MonoBehaviour
{
    public static PanelSuccess ins;

    public Text linesText, levelText, timeText;

    private void Awake()
    {
        ins = this;
    }
}
