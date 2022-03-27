using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckBtn : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            LoadingSceneManager.LoadScene("PlayScene");
        }

        if(Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
