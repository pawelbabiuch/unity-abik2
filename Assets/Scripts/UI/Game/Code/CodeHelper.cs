using UnityEngine;
using UnityEngine.EventSystems;

public class CodeHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CodeManager.ins.selectedCode = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CodeManager.ins.selectedCode = null;
    }
}