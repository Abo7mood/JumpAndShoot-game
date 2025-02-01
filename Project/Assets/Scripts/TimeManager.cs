using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeManager : MonoBehaviour
{
    [Header("TimeEdit")]
    [SerializeField] float timeMulitplier;
    [SerializeField] float realtime;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Update()
    {
     
            Time.timeScale += timeMulitplier * Time.deltaTime;
        Mathf.Clamp(Time.timeScale, 0, 100);
        realtime = Time.timeScale;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        Time.timeScale = 1;
    }
}
