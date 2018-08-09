using UnityEngine;

public class CodeForBlock : CodeBlock
{
    public Transform codeForEnd;

    public void SetUpCodeForEnd(Transform code)
    {
        int index = code.GetSiblingIndex() +1;
        CodeFor cF = code.GetComponent<CodeFor>();

        cF.codeEnd = Instantiate(codeForEnd, code.parent);
        cF.codeEnd.SetSiblingIndex(index);

        CodeForEnd cFE = cF.codeEnd.GetComponent<CodeForEnd>();
        cFE.arrow = cFE.transform.Find("Arrow").GetComponent<RectTransform>();
        cFE.codeFor = code;
        cFE.canvasGroup = cFE.GetComponent<CanvasGroup>();
        cFE.SetUpArrow();
    }
}
