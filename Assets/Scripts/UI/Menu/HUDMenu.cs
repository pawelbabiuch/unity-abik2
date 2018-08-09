using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class HUDMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject regusterPanel;
    public GameObject reulesPanel;
    public GameObject aboutPanel;
    public GameObject settingsPanel;
    public GameObject scorePanel;
    public GameObject levelsPanel;

    [Header("Register panel Inputs")]
    public InputField registerLogin;
    public InputField registerPassword;
    public InputField registerRePassword;
    public Toggle registerToggleRules;
    public Button registerBtn;

    [Header("Login panel Inputs")]
    public InputField loginLogin;
    public InputField loginPassword;
    public Button loginBtn;

    [Header("Ustawienia")]
    public Toggle soundOnOfToggle;
    public Slider soundVolumeSlidger;
    private float lastVolume;

    [Space]
    public Toggle graphicsMinToggle;
    public Toggle graphicsMidToggle;
    public Toggle graphicsMaxToggle;

    [Header("Wyniki")]
    public PanelTask[] taskPanelArray;
    private SortyBy sortBy = SortyBy.CodeLines;

    [Header("Inne")]
    public Text userLoginText;

    public static HUDMenu ins;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        SelectMenuPanel.curOpenPanel = aboutPanel;  // Ustawia domyślny, statyczny panel startowy. (musi być włączony, a inne wyłączone na scenie)
        LoadResults();
        LoadSettings();

        if (Repository.ins.loggedUserID >= 0 && userLoginText != null)
            userLoginText.text = Repository.ins.GetUser(Repository.ins.loggedUserID).login;
    }

    /// <summary>
    /// Sprawdza czy może aktywować przycisk "Zaloguj" na podstawie
    /// loginu oraz hasła (czy nie są puste)
    /// </summary>
    public void LoginPanelBtnCheck()
    {
        loginBtn.interactable = !string.IsNullOrEmpty(loginLogin.text) && !string.IsNullOrEmpty(loginPassword.text);
    }

    /// <summary>
    /// Sprawdza czy może aktywować przycisk "Zarejestruj" na podstawie
    /// loginu, hasła (czy nie są puste), a także czy drugie hasło jest równe pierwszemu
    /// oraz czy zaznaczono: "akceptuję regulamin"
    /// </summary>
    public void RegisterPanelBtnCheck()
    {
        registerBtn.interactable = !string.IsNullOrEmpty(registerLogin.text) && !string.IsNullOrEmpty(registerPassword.text) &&
                                    registerPassword.text == registerRePassword.text && registerToggleRules.isOn;
    }

    public void ChangeSortType(int sort)
    {
        sortBy = (SortyBy)sort;
        LoadResults();
    }

    private void LoadResults()
    {
        PanelTask panelTask = null;
        List<Result> results = new List<Result>();

        for (int i = 0; i < taskPanelArray.Length; i++)
        {

            panelTask = taskPanelArray[i];
            panelTask.results.Clear();

            foreach (Transform child in panelTask.contentParent) Destroy(child.gameObject);


            if (sortBy == SortyBy.CodeLines)
                results = Repository.ins.results.Where(x => x.levelID == i).OrderBy(x => x.codeLines).ToList();
            else if (sortBy == SortyBy.CodeTime)
                results = Repository.ins.results.Where(x => x.levelID == i).OrderBy(x => x.codeTime).ToList();
            else
                results = Repository.ins.results.Where(x => x.levelID == i).ToList();

            if (results.Count > panelTask.maxResultsCount)
                results.RemoveRange(panelTask.maxResultsCount - 1, results.Count - panelTask.maxResultsCount);

            foreach (Result result in results) panelTask.AddResult(result);
        }
    }

    public void OnVolumeChange()
    {
        AudioManager.ins.ChangeVolume(soundVolumeSlidger.value);
        SaveSettings();
    }

    public void ChangeGraphic(int id)
    {
        QualitySettings.SetQualityLevel(id);
        SaveSettings();
    }

    public void ChangeVolumeToggle()
    {
        if (soundOnOfToggle.isOn)
            soundVolumeSlidger.value = lastVolume;
        else
        {
            lastVolume = soundVolumeSlidger.value;
            soundVolumeSlidger.value = soundVolumeSlidger.minValue;
        }
    }

    public void SaveSettings()
    {
        //  PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlidger.value);
        //  PlayerPrefs.SetInt("Quality", QualitySettings.GetQualityLevel());
    }

    private void LoadSettings()
    {
        //   float volume = PlayerPrefs.GetFloat("SoundVolume");
        //   byte quality = (byte)PlayerPrefs.GetInt("Quality");

        //   Debug.Log(soundVolumeSlidger);

    }

    public void LogIn()
    {
        string login = loginLogin.text;
        string password = loginPassword.text;

        User findUser = Repository.ins.GetUser(login);

        if (findUser == null)
        {
            loginLogin.text = "";
            loginPassword.text = "";
            //    Debug.LogWarningFormat("Nie ma takiego użytkownika jak: <b>{0}</b>", login);
            InfoManager.ins.SetUpPanel(PanelType.Red, "Logowanie", string.Format("Nie ma takiego użytkownika jak: <b>{0}</b>", login));
        }
        else
        {
            if (findUser.password != password)
            {
                loginPassword.text = "";
                //         Debug.LogWarningFormat("Użytkownik <b>{0}</b> ma inne hasło", login);
                InfoManager.ins.SetUpPanel(PanelType.Red, "Logowanie", string.Format("Użytkownik <b>{0}</b> ma inne hasło", login));
            }
            else
            {
                Repository.ins.loggedUserID = findUser.id;
                //            Debug.Log("Zalogowano pomyślnie!");
                InfoManager.ins.SetUpPanel(PanelType.Green, "Logowanie", "Zalogowano pomyślnie");

                SceneManager.UnloadSceneAsync("Menu_Unknow user");
                SceneManager.LoadScene("Menu_Logged", LoadSceneMode.Additive);
            }
        }
    }

    public void Register()
    {
        string login = registerLogin.text;
        string password = registerPassword.text;

        User findUser = Repository.ins.GetUser(login);

        if (findUser != null)
        {
            //          Debug.LogWarningFormat("Użytkownik o loginie <b>{0}</b> już istnieje", login);
            InfoManager.ins.SetUpPanel(PanelType.Red, "Rejestracja", string.Format("Użytkownik o loginie <b>{0}</b> już istnieje.", login));
        }

        else
        {
            User.AddUser(login, password);
            //         Debug.Log("Zarejestrowano pomyślnie");
            InfoManager.ins.SetUpPanel(PanelType.Green, "Rejestracja", "Zarejestrowano pomyślnie");
        }
    }

    public void LogOut()
    {
        Repository.ins.loggedUserID = -1;
        PlayerPrefs.DeleteKey("UserID");

        SceneManager.UnloadSceneAsync("Menu_Logged");
        SceneManager.LoadScene("Menu_Unknow user", LoadSceneMode.Additive);
    }

    public void LoadLevel(int id)
    {
        SceneManager.UnloadSceneAsync("Menu_Logged");
        SceneManager.LoadScene(string.Format("Level_{0}", id), LoadSceneMode.Additive);

        AudioManager.ins.PlaySound(AudioManager.ins.gameSound);
    }
}

public enum SortyBy
{
    CodeLines = 0, CodeTime = 1
}
