using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager ins;

    private Text tooltipText;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        tooltipText = transform.GetComponentInChildren<Text>();
    }

    public void SetUpTooltip(string info)
    {
        canvasGroup.alpha = 1;
        tooltipText.text = info;
    }

    public void SetDownTooltip()
    {
        canvasGroup.alpha = 0;
    }
}
