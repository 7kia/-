using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    class LevelInfo
    {
        int edgeNumber = 4;
        int countNumber = 3;
        int amountNumberToEdge = 9;
    }

    private List<LevelInfo> m_levelInfos = new List<LevelInfo>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(int levelId)
    {
        if (levelId != 1)
        {
            throw new Exception("levelId != 1");
        }

        // TODO : add new levels
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

}
