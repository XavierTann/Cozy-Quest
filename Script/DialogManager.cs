using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject optionsBox;
    [SerializeField] GameObject shopBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond;

    Dialog dialog;
    int currentLine = 0;
    bool isTyping = false;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance {get; private set;}

    public void Awake() {
        Instance = this;
    }

    public IEnumerator ShowDialog(Dialog dialog) {
        this.dialog = dialog;
        yield return new WaitForEndOfFrame();
        OnShowDialog?.Invoke(); // Change state of Game Controller
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.DialogLines[0]));
    }

    public void HandleUpdate() {
        if (Input.GetKeyDown(KeyCode.E) && !isTyping) {
            ++ currentLine;
            if (currentLine < dialog.DialogLines.Count ) {
                StartCoroutine(TypeDialog(dialog.DialogLines[currentLine]));
            }
            else {
                // Show options
                optionsBox.SetActive(true);
            }

        }
        
    }

    // Shows text letter by letter
    public IEnumerator TypeDialog(string line) {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line.ToCharArray()) {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/lettersPerSecond);
        }
        isTyping = false;
    }


    // Dialog Options

    public void Leave() {
        dialogBox.SetActive(false);
        optionsBox.SetActive(false);
        currentLine = 0;
        OnHideDialog?.Invoke(); // Change state of game controller back to Free Roam
    }

    public void GoToShop() {
        dialogBox.SetActive(false);
        optionsBox.SetActive(false);
        currentLine = 0;
        shopBox.SetActive(true);

        // Change state to shopping
        
        Debug.Log("Shop Opened!");
    }
       
}
