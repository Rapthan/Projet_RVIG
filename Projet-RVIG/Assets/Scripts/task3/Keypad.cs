using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_Text screenText;
    private string enteredCode = "";
    private Color defaultColor;

    void Start()
    {
        // Sauvegarde de la couleur de base du texte
        defaultColor = screenText.color;

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable[] interactables = GetComponentsInChildren<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        foreach (var interactable in interactables)
        {
            if (interactable != null)
            {
                interactable.selectEntered.AddListener(_ => HandleButtonPress(interactable.GetComponentInChildren<TMP_Text>().text));
            }
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
            screenText.color = defaultColor; // Remettre la couleur par défaut
        }
    }

    public void RemoveLastNumber()
    {
        if (enteredCode.Length > 0)
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
            screenText.text = enteredCode;
            screenText.color = defaultColor; // Remettre la couleur par défaut
        }
    }

    public void ValidateCode()
    {
        if (enteredCode == "176528") 
        {
            Debug.Log("Code correct !");
            screenText.color = Color.green; // Change le texte en vert
            ClearCodeDelayed(); // Efface après un délai
        }
        else
        {
            Debug.Log("Code incorrect !");
            screenText.color = Color.red; // Change le texte en rouge
            ClearCodeDelayed(); // Efface après un délai
        }
    }

    private void ClearCodeDelayed()
    {
        Invoke("ClearCode", 1.5f); // Efface après 1.5 secondes
    }

    public void ClearCode()
    {
        enteredCode = "";
        screenText.text = ""; 
        screenText.color = defaultColor; // Remet la couleur de base
    }
}
