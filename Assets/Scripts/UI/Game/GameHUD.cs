using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject canvasCode;
    public GameObject canvasToolbar;

    [Header("Panels")]
    public GameObject panelMenu;
    public GameObject panelInfo;
    public GameObject panelParts;
    public GameObject successPanel;
    public GameObject faillPanel;

    [SerializeField]
    [Header("Toolbar")]
    private Slider slidgerSpeed;

    [SerializeField]
    [Header("Panel Program - Panels")]
    private CanvasGroup Panels;
    //private CanvasGroup panelsCanvasGroup;

    public static GameHUD ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        timeScale = 1;
    }

    private float timeScale
    {
        get { return Time.timeScale; }
        set { Time.timeScale = value; }
    }

    public void OpenMenu()
    {
        timeScale = 0;
        panelMenu.SetActive(true);
    }

    /// <summary>
    /// Zamykanie menu podczas rozgrywki.
    /// </summary>
    public void CloseMenu()
    {
        ChangeGameSpeed();
        panelMenu.SetActive(false);
    }

    /// <summary>
    /// Restartowanie aktualnego poziomu
    /// Funkcja działa tylko w przypadku, gdy zaczynamy od sceny: Repository
    /// </summary>
    public void ResetLevel()
    {
        try
        {
            if (SceneManager.sceneCount != 2)
                throw new System.Exception("Nie można wrócić do menu głównego w przypadku, gdy nie zaczynasz ze sceny REPOSITORY!");

            AudioManager.ins.PlaySound();
            string sceneName = SceneManager.GetSceneAt(1).name;
            SceneManager.UnloadSceneAsync(sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        }
        catch(System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    /// <summary>
    /// Wykonuje powród do głównego menu.
    /// Funkcja działa tylko w przypadku, gdy zaczynamy od sceny: Repository
    /// </summary>
    public void BackToMainMenu()
    {
        try
        {
            if (SceneManager.sceneCount != 2)
                throw new System.Exception("Nie można wrócić do menu głównego w przypadku, gdy nie zaczynasz ze sceny REPOSITORY!");

            //timeScale = 1;
            AudioManager.ins.PlaySound(AudioManager.ins.defaultSound);
            string sceneName = SceneManager.GetSceneAt(1).name;
            SceneManager.UnloadSceneAsync(sceneName);
            SceneManager.LoadScene("Menu_Logged", LoadSceneMode.Additive);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    /// <summary>
    /// Po zrobieniu zadania wynik zostaje zapisany oraz gracz przenoszony jest do głównego menu.
    /// </summary>
    public void SaveResult()
    {
        int level = int.Parse(ToolbarManager.ins.txtLevelNumber.text) -1;
        int lines = CodeManager.ins.content.childCount - 2;
        int time = int.Parse(ToolbarManager.ins.timer.ToSeconds());

        User user = Repository.ins.GetUser(Repository.ins.loggedUserID);
        Result.AddResult(level, user, lines, time);

        BackToMainMenu();
    }

    /// <summary>
    /// Ustawianie sceny po zaakceptowaniu zadania z początku poziomu
    /// </summary>
    public void AcceptTask()
    {
        panelInfo.SetActive(false);
        canvasCode.SetActive(true);
        canvasToolbar.SetActive(true);
    }

    /// <summary>
    /// Zmiana prędkości rozgrywki - na przesuwanie paska SPEED w trakcie gry
    /// </summary>
    public void ChangeGameSpeed()
    {
        timeScale = slidgerSpeed.value;
    }

    /// <summary>
    /// Metody stworzone na potrzeby przycisku Play i Stop
    /// Dzięki dwóm metodom poniżej jest możliwość wyłączenia jak i włączenia
    /// okna odpowiedzialnego za tworzenie kodu w grze
    /// </summary>

    public void DeactivePanelParts()
    {
        panelParts.SetActive(false);
        PartialDeactivePanelProgram();
    }

    public void ActivePanelParts()
    {
        panelParts.SetActive(true);
        PartialActivePanelProgram();
    }

    private void PartialDeactivePanelProgram()
    {
        Panels.blocksRaycasts = false;
    }

    private void PartialActivePanelProgram()
    {
        Panels.blocksRaycasts = true;
    }

    public void SetUp_FaillPanel(string faillMsg)
    {
        faillPanel.SetActive(true);
        faillPanel.transform.Find("Panel").Find("Body").GetComponentInChildren<Text>().text = faillMsg;
    }

    public void SetUp_SuccessPanel()
    {
        successPanel.SetActive(true);
        PanelSuccess.ins.levelText.text = ToolbarManager.ins.txtLevelNumber.text;
        PanelSuccess.ins.linesText.text = (CodeManager.ins.content.childCount - 2).ToString();
        PanelSuccess.ins.timeText.text = ToolbarManager.ins.txtTimer.text;
    }
}
