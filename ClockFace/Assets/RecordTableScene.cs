using MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordTableScene : MonoBehaviour {

    public LevelManager levelManager;
    public CurrentLevelInfoManager currentLevelInfoManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(int levelId)
    {
        // TODO : возможно при переигрывании уровня уже пройденный придётся переоткрывать
        levelManager.CreateLevelInfo();
        currentLevelInfoManager.SetCurrentLevel(levelId);

        // TODO : reset info, load 1 level
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

}
