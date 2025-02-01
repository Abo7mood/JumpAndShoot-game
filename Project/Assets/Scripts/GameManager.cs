using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
 
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
            instance = this;
        }
       
    
    }
    public void LoadScene() => SceneManager.LoadScene(1);
    public void MainMenu() => SceneManager.LoadScene(0);


}
