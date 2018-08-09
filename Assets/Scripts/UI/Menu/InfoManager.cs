using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public Transform redPanel, greenPanel;
    public readonly float visableTime = 3.0f;

    public static InfoManager ins;

    private WaitForSeconds wfS;
    private List<IEnumerator> infoList = new List<IEnumerator>();
    private bool isWorking = false;
    private LastPanel lastPanel = new LastPanel();

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        wfS = new WaitForSeconds(visableTime);
        InvokeRepeating("CheckInfo", 2, 0.2f);
    }

    private void CheckInfo()
    {
        if (infoList.Count > 0 && !isWorking)
            StartCoroutine(infoList[0]);
    }

    public void SetUpPanel(PanelType panelType, string header, string info)
    {
        if (lastPanel.Equals(panelType, header, info)) return;
        else lastPanel = new LastPanel(panelType, header, info);

        Transform panel = null;

        switch(panelType)
        {
            case PanelType.Green:
                panel = greenPanel;
                break;
            case PanelType.Red:
                panel = redPanel;
                break;
        }

        infoList.Add(PanelActive(panel, header, info));     
    }

    private IEnumerator PanelActive(Transform panel, string header, string info)
    {
        isWorking = true;
        GetHeaderText(panel).text = header;
        GetInfoText(panel).text = info;

        panel.gameObject.SetActive(true);
        yield return wfS;
        infoList.RemoveAt(0);

        if (infoList.Count == 0) lastPanel = new LastPanel();

        panel.gameObject.SetActive(false);
        isWorking = false;
    }

    private Text GetHeaderText(Transform panel)
    {
        return panel.Find("Header").Find("Text_Header").GetComponent<Text>();
    }

    private Text GetInfoText(Transform panel)
    {
        return panel.Find("Text_Body").GetComponent<Text>();
    }
}

public enum PanelType
{
    Red, Green
}

public struct LastPanel
{
    PanelType panelType;
    string header;
    string info;

    public LastPanel(PanelType panelType, string header, string info):this()
    {
        this.panelType = panelType;
        this.header = header;
        this.info = info;
    }

    public bool Equals(PanelType panelType, string header, string info)
    {
        if (this.panelType == panelType && this.header == header && this.info == info)
            return true;
        else
            return false;
    }
}
