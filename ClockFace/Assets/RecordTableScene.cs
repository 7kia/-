using MyGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordTableScene : MonoBehaviour {

    public LevelManager levelManager;
    public CurrentLevelInfoManager currentLevelInfoManager;
    public Button[] replayButtons;
    // Use this for initialization
    void Start() {

        int lastFinishedLevel = currentLevelInfoManager.GetLastFinishedLevel();
        for (int index = 0; index < replayButtons.Length; ++index)
        {
            replayButtons[index].interactable = (index <= lastFinishedLevel);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(int levelId)
    {
        // TODO : возможно при переигрывании уровня уже пройденный придётся переоткрывать
        levelManager.CreateLevelInfo();
        currentLevelInfoManager.SetCurrentLevel(levelId);

        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

}
