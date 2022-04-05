using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    
    public void Restart()
    {
        gameObject.SetActive(false);
        GameManager.Instance.Enable();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
