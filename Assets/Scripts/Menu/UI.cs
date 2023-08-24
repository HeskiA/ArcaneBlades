using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject pause;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                pause.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pause.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
    public void Resume()
    {
        pause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
