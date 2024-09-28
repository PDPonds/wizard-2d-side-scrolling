using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [Header("===== Interact =====")]
    [SerializeField] GameObject interactText;

    public void SetInteractText(string text)
    {
        TextMeshProUGUI textM = interactText.GetComponent<TextMeshProUGUI>();
        textM.text = text;
    }

    public void ShowInteractText()
    {
        interactText.SetActive(true);
    }

    public void HideInteractText()
    {
        interactText.SetActive(false);
    }

}
