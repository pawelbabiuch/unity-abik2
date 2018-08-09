using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CodeBlock : MonoBehaviour
                                    ,IPointerClickHandler
                                    ,IPointerEnterHandler
                                    ,IPointerExitHandler
                                    ,IBeginDragHandler
                                    ,IDragHandler
                                    ,IEndDragHandler
{
    public CodeType codeType = CodeType.Unknown;
    public GameObject codePrefab;
    public string codeInfo = "";

    private void Start()
    {
        if (codeType == CodeType.Unknown) throw new System.FormatException(string.Format("codeType dla '{0}' nie może być ustawiony jako: '{1}'", codeText, codeType));
        if (string.IsNullOrEmpty(codeInfo)) throw new System.FormatException(string.Format("codeInfo dla '{0}' nie może być puste", codeText));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (eventData.clickCount != 2) return;

        int siblingIndex = CodeManager.ins.content.childCount - 2;

        GameObject newCode = Instantiate(codePrefab, CodeManager.ins.content);
        newCode.GetComponent<Code>().codeText = codeText;
        newCode.GetComponent<Code>().codeNumber = siblingIndex +1;
        newCode.GetComponent<Code>().codeType = codeType;
        newCode.GetComponent<CanvasGroup>().blocksRaycasts = true;
        newCode.transform.SetSiblingIndex(siblingIndex);

        if (this is CodeForBlock) ((CodeForBlock)this).SetUpCodeForEnd(newCode.transform);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.ins.SetUpTooltip(codeInfo);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.ins.SetDownTooltip();
    }


    Code newCode = null;
    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject newCode = Instantiate(codePrefab, CodeManager.ins.transform);
        this.newCode = newCode.GetComponent<Code>();
        this.newCode.codeType = codeType;
        this.newCode.codeText = codeText;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (newCode == null) return;
        newCode.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (newCode == null) return;

        if (CodeManager.ins.selectedCode == null)
        {
            Destroy(newCode.gameObject);
        }
        else if (CodeManager.ins.selectedCode != null)
        {
            newCode.GetComponent<CanvasGroup>().blocksRaycasts = true;

            int siblingIndex = CodeManager.ins.selectedCode.GetSiblingIndex();

            if (CodeManager.ins.selectedCode == CodeManager.ins.lastCode)
                siblingIndex -= 1;
            else
                CodeManager.ins.emptyCode.SetSiblingIndex(CodeManager.ins.content.childCount - 2);

            newCode.transform.SetParent(CodeManager.ins.content);
            newCode.transform.SetSiblingIndex(siblingIndex);

           if(this is CodeForBlock) ((CodeForBlock)this).SetUpCodeForEnd(newCode.transform);

            Code.RenumberCodes();
        }
        else
            Debug.LogError("Wystąpił błąd przy wstawianiu bloku kodu do programu!");

        newCode = null;
    }

    public string codeText
    {
        get { return transform.GetComponentInChildren<Text>().text; }
    }
}

public enum CodeType
{
    Unknown, PickUpBlock, PutDownBlock, While_n_more_0
}
