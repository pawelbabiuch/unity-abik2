using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        // ładuje scenę z 1 poziomem
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
