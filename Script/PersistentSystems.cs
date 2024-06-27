using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PersistentSystems : MonoBehaviour
{
    public PersistentSystems Instance {get; private set;}
    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
