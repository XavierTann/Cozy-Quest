using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] List<string> dialogLines;
    
    // DialogLines is a read only property, only has a getter method.
    public List<string> DialogLines
    {
        get {return dialogLines;}
    }


}
