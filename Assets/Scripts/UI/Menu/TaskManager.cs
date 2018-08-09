using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

    /// <summary>
    /// Data z danymi wejściowymi, wyjściowymi oraz typem zadania
    /// </summary>
    public TaskData taskData;

    /// <summary>
    /// Tekst ekranu monitora dla wejścia/wyjścia
    /// </summary>
    public Text pcInput, pcOutput;

    /// <summary>
    /// Obiekt na scenie, który jest przenoszony przez postać
    /// </summary>
    public GameObject inputPrefab;

    /// <summary>
    /// Aktualny stan wartości wyjściowych
    /// </summary>
    private List<string> currentOutputs = new List<string>();

    /// <summary>
    /// Aktualnie przenoszony input
    /// </summary>
    private string input;

    private void Start()
    {
        taskData.SetUp();
        pcInput.text = taskData.GetListAsString(taskData.inputs);
        pcOutput.text = "";
    }



    public void GetInput()
    {
        // W przyszłości należy dodać tutaj bajer, aby gracz dostawał tego inputa w łapkę.
        // W przypadku, gdy mamy w ręce jakiś przedmiot, to należy go wyrzucić.

        input = taskData.GetInput(); // tego inputa musi mieć gracz w łapkach.
        pcInput.text = taskData.GetListAsString(taskData.inputs);
    }

    public void SetOutput()
    {
        // W przyszłości należy usunać z łapków bohaterów te klocuchy z inputem.
        if (string.IsNullOrEmpty(input)) throw new System.Exception("Nie możesz odstawić inputu, jeżeli go nie masz w dłoniach!");
        currentOutputs.Add(input);
        input = "";
        pcOutput.text = taskData.GetListAsString(currentOutputs);

    }

    public bool CheckResult()
    {
        bool result = true;

        if (currentOutputs.Count != taskData.output.Count) result = false;
        else
        {
            for (int i = 0; i < currentOutputs.Count; i++)
            {
                if(!currentOutputs[i].Equals(taskData.output[i]))
                {
                    result = false;
                    break;
                }
            }
        }

        if(result) GameHUD.ins.SetUp_SuccessPanel();
        else GameHUD.ins.SetUp_FaillPanel("Zadanie nie zostało poprawnie wykonane. Na wyjściu znajdują się inne wartości niż było to oczekiwane.");

        return result;
    }
}
