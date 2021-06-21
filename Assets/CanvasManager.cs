using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR //check if running in unity editor
using UnityEditor;
#endif 
using UnityEngine;

public class CanvasManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
    Application.Quit();
    #endif
    }
}
