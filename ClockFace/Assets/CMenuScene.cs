using MyGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMenuScene : MonoBehaviour {

    public LevelManager levelManager;
    public CurrentLevelInfoManager currentLevelInfoManager;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Exit()
    {
        Application.Quit();
    }

    public void LoadRecordTable()
    {
        SceneManager.LoadScene("RecordTableScene", LoadSceneMode.Single);
    }

    public void StartNewGame()
    {
        levelManager.CreateLevelInfo();
        currentLevelInfoManager.SetCurrentLevel(0);

        // TODO : reset info, load 1 level
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
