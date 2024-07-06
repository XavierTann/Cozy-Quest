using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] GameObject levelUI;

    public void Start() {
        SetLevel(1);
    }

    public void SetLevel(int level) {
        levelUI.transform.GetChild(2).GetComponent<Text>().text = level.ToString();
    }
}