using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_Text screenText;
    private string enteredCode = "";

    void Start()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(() => HandleButtonPress(btn.GetComponentInChildren<TMP_Text>().text));
            Debug.Log("button setup!");
        }
    }

    public void HandleButtonPress(string buttonValue)
    {
        if (buttonValue == "x")
        {
            RemoveLastNumber();
        }
        else if (buttonValue == "/") 
        {
            ValidateCode();
        }
        else 
        {
            AddNumber(buttonValue);
        }
    }

    public void AddNumber(string number)
    {
        if (enteredCode.Length < 6)
        {
            enteredCode += number;
            screenText.text = enteredCode;
        }
    }

    public void RemoveLastNumber()
    {
        if (enteredCode.Length > 0)
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
            screenText.text = enteredCode;
        }
    }

    public void ValidateCode()
    {
        if (enteredCode == "176528") 
        {
            Debug.Log("Code correct !");
            ClearCode(); 
        }
        else
        {
            Debug.Log("Code incorrect !");
            ClearCode(); 
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        screenText.text = ""; 
    }
}