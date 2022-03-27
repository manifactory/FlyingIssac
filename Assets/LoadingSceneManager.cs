using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneManager : MonoBehaviour 
{ 
    public static string nextScene;

    float fillAmount;

    private void Start() 
    { 
        StartCoroutine(LoadScene()); 
    } 

    public static void LoadScene(string sceneName) 
    { 
        nextScene = sceneName;
        SceneManager.LoadScene("LoadScene"); 
    } 

    IEnumerator LoadScene() 
    { 
        yield return null; AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); 
        op.allowSceneActivation = true;
        Debug.Log(op.isDone);
        while (!op.isDone) 
        { 
            yield return null;
            Debug.Log(op.progress);
        }
        Debug.Log(op.isDone); 
    } 
}
