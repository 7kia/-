using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using UnityEngine.UI;
//using CarFactoryNamespace;

public class GameManager : MonoBehaviour
{
    public MyGame.LevelManager levelManager;
    public GameObject[] buttons;
    public CEdgeController edgeController;
    public Text amountText;
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
        SetActiveStateTransportButtons(carCounts);
        amountText.text = levelManager.m_resultNumber.ToString();

        edgeController.m_resultNumber = levelManager.m_resultNumber;
        Debug.Log(edgeController.m_resultNumber.ToString());
        Debug.Log(levelManager.m_resultNumber.ToString());

    }

    private void SetActiveStateTransportButtons(List<int> carCounts)
    {
        for(int index = 0; index < carCounts.Count; ++index)
        {
            Debug.Log(index.ToString() + " " + (carCounts[index] > 0).ToString());
            buttons[index].SetActive(carCounts[index] > 0);
        }
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

