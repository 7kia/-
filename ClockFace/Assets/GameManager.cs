using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
//using CarFactoryNamespace;

public class GameManager : MonoBehaviour
{
    public MyGame.LevelManager levelManager;
    public CTransportFactory transportFactory;
    public CurrentLevelInfoManager currentLevelInfoManager;
    public int currentLevel = 0;

    #region For pause
    private bool m_isPause = false;

    #endregion
    void Start()
    {
        levelManager.SetCurrentLevel(currentLevelInfoManager.GetCurrentLevelId());

        List<int> carCounts = new List<int>();
        foreach(var value in levelManager.carCounts.Values)
        {
            carCounts.Add(value);
        }
        transportFactory.UpdateCounters(carCounts);
    }

    void Update()
    {
        
    }

    public void RecreateLevel()
    {
        Start();
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}

