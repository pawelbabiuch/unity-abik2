using UnityEngine;
using UnityEngine.UI;

public class SummaryManager : MonoBehaviour {

    public Text linesCounter;

	// Use this for initialization
	void Start ()
    {
        if (!linesCounter)
            Debug.LogError("Missing line counter text reference");

        
	}
	
	// Update is called once per frame
	//void Update ()
    //{
		
	//}

    private void DisplayLinesCount()
    {
        linesCounter.text = (CodeManager.ins.content.childCount - 2).ToString(); // odejmujemy 2 elementy ponieważ już istnieją na scenie i do tego są puste jak i niewidoczne dla użytkownika
    }
}
