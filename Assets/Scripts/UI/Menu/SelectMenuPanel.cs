using UnityEngine;

public class SelectMenuPanel : MonoBehaviour
{
    public static GameObject curOpenPanel;

    public void SwitchPanel()
    {
        curOpenPanel.SetActive(false);
        this.gameObject.SetActive(true);
        curOpenPanel = this.gameObject;

      //  Debug.Log("Changed Panel");
    }
}
