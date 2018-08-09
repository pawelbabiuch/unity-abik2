using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public abstract class MoveableCode : MonoBehaviour
                                , IPointerClickHandler
                                , IBeginDragHandler
                                , IDragHandler
                                , IEndDragHandler
                                , IPointerEnterHandler
                                , IPointerExitHandler
{
    [HideInInspector]
    public CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public abstract int codeNumber { get; set; }

    public static void RenumberCodes(int number = -1)
    {
        Code[] codeList = CodeManager.ins.GetComponentsInChildren<Code>().Where(x => x.codeNumber != number).ToArray();

        int increase = 1;
        for (int i = 0; i < codeList.Length; i++)
        {
            codeList[i].codeNumber = i + increase;

            if (codeList[i] is CodeFor) increase++;

            if (codeList[i] is CodeFor)
                ((CodeFor)codeList[i]).codeEnd.GetComponent<CodeForEnd>().SetUpArrow();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && eventData.clickCount == 2)
        {
            Debug.Log("Usuwanie kodu");

            if (this is CodeFor)
            {
                GameObject endCode = ((CodeFor)this).codeEnd.gameObject;
                Destroy(endCode);

            }
            else if (this is CodeForEnd) { return; }


            Destroy(gameObject);
            RenumberCodes(this.codeNumber);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        int siblingIndex = transform.GetSiblingIndex();
        CodeManager.ins.emptyCode.SetSiblingIndex(siblingIndex);

        CodeManager.ins.lastCode.GetComponent<Image>().raycastTarget = false;
        CodeManager.ins.emptyCode.GetComponent<Image>().raycastTarget = false;

        if (this is CodeForEnd)
            ((CodeForEnd)this).arrow.gameObject.SetActive(false);
        else if (this is CodeFor)
            ((CodeFor)this).codeEnd.GetComponent<CodeForEnd>().arrow.gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvasGroup)
            canvasGroup.blocksRaycasts = false;

        transform.SetParent(CodeManager.ins.transform);

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (CodeManager.ins.selectedCode == null)
        {
            int siblingIndex = CodeManager.ins.emptyCode.GetSiblingIndex();
            CodeManager.ins.emptyCode.SetSiblingIndex(CodeManager.ins.content.childCount - 2);
            transform.SetParent(CodeManager.ins.content);
            transform.SetSiblingIndex(siblingIndex);

            if (this is CodeForEnd)
                ((CodeForEnd)this).SetUpArrow();
        }
        else
        {
            int siblingIndex = CodeManager.ins.selectedCode.GetSiblingIndex();
            CodeManager.ins.emptyCode.SetSiblingIndex(CodeManager.ins.content.childCount - 2);
            transform.SetParent(CodeManager.ins.content);
            transform.SetSiblingIndex(siblingIndex);
            RenumberCodes();
        }

        CodeManager.ins.lastCode.GetComponent<Image>().raycastTarget = true;
        CodeManager.ins.emptyCode.GetComponent<Image>().raycastTarget = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CodeManager.ins.selectedCode = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CodeManager.ins.selectedCode = null;
    }
}
