using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TaskData : ScriptableObject
{
    [Tooltip("Pomocniczna nazwa zadania")]
    public string taskName = "Level_0";
    [Tooltip("Maksymalna ilość linii kodu, jaką można wykorzystać do tego zadania")]
    public int maxLineNumber = 128;

    [Space]
    [Tooltip("Typy wartości generowane na wejściu")]
    public InputTypes[] inputsType;
    [Tooltip("Filtr mówiący, jakie wartości zostaną przekazane do wyjścia")]
    public OutputTask taskType;


    private string chars = "ABCDEFGHIJKLMNOPQRSTUWXYZ";
    private sbyte minNumber = -15;
    private byte maxNumber = 15;

    /// <summary>
    /// Wartości, które są generowane na wejściu
    /// </summary>
    public List<string> inputs { get; private set; }
    /// <summary>
    /// Wartości, które muszą pojawić się na wyjściu, aby zaliczyć zadanie
    /// </summary>
    public List<string> output { get; private set; }

    public void SetUp()
    {
        try
        {
            inputs = new List<string>();
            //   output = new List<string>();

            #region Ustawianie wejść
            foreach (InputTypes iT in inputsType)
            {
                if (iT == InputTypes.None) throw new System.Exception("Typ wartości nie może być nieokreślony!");

                sbyte val;

                switch (iT)
                {
                    case InputTypes.Char:
                        val = (sbyte)Random.Range(0, chars.Length);
                        inputs.Add(chars[val].ToString());
                        break;
                    case InputTypes.Number:
                        val = (sbyte)Random.Range(minNumber, maxNumber + 1);
                        inputs.Add(val.ToString());
                        break;
                    case InputTypes.Number_0:
                        inputs.Add(0.ToString());
                        break;
                    case InputTypes.Number_not_0:
                        val = (sbyte)Random.Range(minNumber, maxNumber);
                        while (val == 0) val = (sbyte)Random.Range(minNumber, maxNumber);
                        inputs.Add(val.ToString());
                        break;
                    case InputTypes.Number_Even:
                        val = (sbyte)Random.Range(minNumber, maxNumber);
                        while (val % 2 != 0) val = (sbyte)Random.Range(minNumber, maxNumber);
                        inputs.Add(val.ToString());
                        break;
                    case InputTypes.Number_Odd:
                        val = (sbyte)Random.Range(minNumber, maxNumber);
                        while (val % 2 == 0) val = (sbyte)Random.Range(minNumber, maxNumber);
                        inputs.Add(val.ToString());
                        break;
                    case InputTypes.Number_Positive:
                        val = (sbyte)Random.Range(1, maxNumber + 1);
                        inputs.Add(val.ToString());
                        break;
                    case InputTypes.Number_Negative:
                        val = (sbyte)Random.Range(minNumber, 0);
                        inputs.Add(val.ToString());
                        break;
                }
            }
            #endregion

            #region Ustawienie wyjść
            switch (taskType)
            {
                case OutputTask.All:
                    output = Output_All();
                    break;
                case OutputTask.Chars:
                    output = Output_CharsOnly();
                    break;
                case OutputTask.Numbers:
                    output = Output_NumbersOnly();
                    break;
                case OutputTask.Numbers_Positive:
                    output = Output_NumbersPositiveOnly();
                    break;
                case OutputTask.Numbers_Negative:
                    output = Output_NumbersNegativeOnly();
                    break;
                case OutputTask.Numbers_0:
                    output = Output_NumbersZeroOnly();
                    break;
                case OutputTask.Numbers_Not_0:
                    output = Output_NumbersNotZeroOnly();
                    break;
            }

            if (output.Count == 0) throw new System.Exception("Zadanie na wyjściu musi mieć jakieś wartości!");

            #endregion
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);

            inputs.Clear();
            output.Clear();
        }
    }

    public string GetInput()
    {
        if (inputs.Count == 0) throw new System.Exception("Nie można pobrać inputa, jeżeli tablica wejściowa jest pusta!");
        string input = inputs[0];
        inputs.RemoveAt(0);
        return input;
    }

    public string GetListAsString(List<string> list)
    {
        string s = "";

        foreach (string i in list)
        {
            if(i.Length == 1)
                s += string.Format("[ {0} ] ", i);
            else if(i.Length == 2)
                s += string.Format("[{0} ] ", i);
            else if(i.Length == 3)
                s += string.Format("[{0}] ", i);
        }

        return s.TrimEnd(' ');
    }

    private List<string> Output_All()
    {
        List<string> all = new List<string>();

        for (int i = 0; i < inputs.Count; i++)
            all.Add(inputs[i]);

        return all;
    }

    private List<string> Output_CharsOnly()
    {
        return inputs.Where(x => x.Length == 1 && x[0] >= 'A' && x[0] <= 'Z').ToList();
    }

    private List<string> Output_NumbersOnly()
    {
        int i = 2;
        return inputs.Where(x => int.TryParse(x, out i) == true).ToList();
    }

    private List<string> Output_NumbersPositiveOnly()
    {
        int i = 2;
        return inputs.Where(x => int.TryParse(x, out i) == true && int.Parse(x) > 0).ToList();
    }

    private List<string> Output_NumbersNegativeOnly()
    {
        int i = 2;
        return inputs.Where(x => int.TryParse(x, out i) == true && int.Parse(x) < 0).ToList();
    }

    private List<string> Output_NumbersZeroOnly()
    {
        int i = 2;
        return inputs.Where(x => int.TryParse(x, out i) == true && int.Parse(x) == 0).ToList();
    }

    private List<string> Output_NumbersNotZeroOnly()
    {
        int i = 2;
        return inputs.Where(x => int.TryParse(x, out i) == true && int.Parse(x) != 0).ToList();
    }

}

public enum InputTypes
{
    None, Char, Number, Number_0, Number_not_0, Number_Even, Number_Odd, Number_Positive, Number_Negative
}

public enum OutputTask
{
    All, Chars, Numbers, Numbers_Positive, Numbers_Negative, Numbers_0, Numbers_Not_0
}