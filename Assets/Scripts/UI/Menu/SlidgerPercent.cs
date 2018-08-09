using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SlidgerPercent : MonoBehaviour
{
    public Text textPercent;
    private Slider slidger;

    private void Start()
    {
        slidger = this.GetComponent<Slider>();
        OnValueChange();
    }

    public void OnValueChange()
    {
        float percent = slidger.value / slidger.maxValue * 100;
        textPercent.text = string.Format("{0}%", Mathf.Round(percent));
    }
}
