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
    public GameObject[] awardPanels;
    // Use this for initialization
    void Start() {

        int lastFinishedLevel = currentLevelInfoManager.GetLastFinishedLevel();
        for (int index = 0; index < replayButtons.Length; ++index)
        {
            replayButtons[index].interactable = (index <= lastFinishedLevel);
        }

        for(int index = 0; index < awardPanels.Length; ++index)
        {
            int award = levelManager.m_levelInfos[index].award;
            for (int childIndex = 0; childIndex < 3; ++childIndex)
            {
                var awardToggle = awardPanels[index].transform.GetChild(childIndex);
                awardToggle.GetComponent<Toggle>().interactable = ((childIndex) < award);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(int levelId)
    {
        // TODO : возможно при переигрывании уровня уже пройденный придётся переоткрывать
        currentLevelInfoManager.SetCurrentLevel(levelId);

        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

}
