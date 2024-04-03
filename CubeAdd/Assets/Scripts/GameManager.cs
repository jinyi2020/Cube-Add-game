using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int cubeCount = 0;
    public GameObject cubePrefab;
    private DateTime startTime = DateTime.MaxValue ;
    public bool timerStart = false;
    public TextMeshProUGUI scoreText;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        createCube();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStart) { startTime = DateTime.UtcNow ; timerStart = false; }
        DateTime currentTime = DateTime.UtcNow;
        var diff = currentTime - startTime;
        if (diff.TotalSeconds >= 1)
        {
            createCube();
            startTime = DateTime.MaxValue;
        }
        scoreText.text = score.ToString();
    }

    void createCube()
    {
        Instantiate(cubePrefab, cubePrefab.transform.position, cubePrefab.transform.rotation);
    }

    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
