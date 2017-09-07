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
    public GameObject[] carButtons;
    public GameObject[] otherButtons;
    public CEdgeController m_edgeController;
    public Text amountText;
    public GameObject victoryDisplay;
    public GameObject defeatDisplay;
    public GameObject canvas;
    public CTransportFactory transportFactory;
    public CurrentLevelInfoManager currentLevelInfoManager;
    public int currentLevel = 0;

    private bool m_canUpdate = true;
    #region For pause
    private bool m_isPause = false;

    #endregion
    void Start()
    {
        victoryDisplay.SetActive(false);
        defeatDisplay.SetActive(false);
        SetActiveStateButtons(true);

        levelManager.SetCurrentLevel(currentLevelInfoManager.GetCurrentLevelId());
        currentLevel = currentLevelInfoManager.GetCurrentLevelId();

        List<int> carCounts = new List<int>();
        foreach(var value in levelManager.carCounts.Values)
        {
            carCounts.Add(value);
        }
        transportFactory.UpdateCounters(carCounts);
        SetActiveStateTransportButtons(carCounts);
        amountText.text = levelManager.m_resultNumber.ToString();

        m_edgeController.m_resultNumber = levelManager.m_resultNumber;
        Debug.Log(m_edgeController.m_resultNumber.ToString());
        Debug.Log(levelManager.m_resultNumber.ToString());
        m_canUpdate = true;
    }

    private void SetActiveStateTransportButtons(List<int> carCounts)
    {
        Debug.Log(carCounts.Count + " carCounts.Count");

        for (int index = 0; index < carCounts.Count; ++index)
        {
            Debug.Log(index.ToString() + " " + (carCounts[index] > 0).ToString());
            carButtons[index].SetActive(carCounts[index] > 0);
        }
    }

    void Update()
    {
        if (m_edgeController.numberIsCompute && m_canUpdate)
        {
            m_edgeController.numberIsCompute = false;
            m_canUpdate = false;
            LoadVictoryDisplay();
        }
    }

    #region Game pause
    public void StopGame()
    {
        BlockTransportToCanvas();
        BlockTransportToCells();
        SetActiveStateButtons(false);
    }

    private void BlockTransportToCanvas()
    {
        for (int index = 0; index < canvas.transform.childCount; ++index)
        {
            var child = canvas.transform.GetChild(index);
            if (child.GetComponent<CTransport>() != null)
            {
                child.GetComponent<CanvasGroup>().interactable = false;
                child.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    private void BlockTransportToCells()
    {
        for (int index = 0; index < m_edgeController.m_edges.Count(); ++index)
        {
            var edge = m_edgeController.m_edges[index];

            for (int index2 = 0; index2 < edge.m_transportList.Count(); ++index2)
            {
                var cell = edge.m_transportList[index2];
                if (cell.transform.childCount > 1)
                {
                    int childCount = cell.transform.childCount;
                    for (int childIndex = 0; childIndex < childCount; ++childIndex)
                    {
                        var child = cell.transform.GetChild(childIndex);
                        child.GetComponent<CanvasGroup>().interactable = false;
                        child.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    }
                }
            }

        }
    }

    private void SetActiveStateButtons(bool isActive)
    {
        for(int index = 0; index < carButtons.Count(); ++index)
        {
            carButtons[index].GetComponent<Button>().interactable = isActive;
        }

        for (int index = 0; index < otherButtons.Count(); ++index)
        {
            otherButtons[index].GetComponent<Button>().interactable = isActive;
        }
    }
    #endregion

    #region DeleteTrasports
    private void DeleteTrasports()
    {
        var shape = canvas.transform.GetChild(1);
        if (currentLevel > 2)
        {
            shape = canvas.transform.GetChild(2);
        }
        var edgeController = shape.GetChild(0);

        for (int index2 = 0; index2 < m_edgeController.m_edges.Count(); ++index2)
        {
            DeleteTrasportFromEdge(m_edgeController.m_edges[index2]);
        }

        //Debug.Log("edgeController Destroy");
        //int index = 0;
        //while (index < m_edgeController.m_edges.Count())
        //{
        //    DeleteTrasportFromEdge(m_edgeController.m_edges[index]);
        //    index++;
        //}
    }

    private void DeleteTrasportFromEdge(CEdge edge)
    {
        //Debug.Log(edge.name + " " + edge.childCount);
        for (int transportListIndex = 0; transportListIndex < edge.m_transportList.Count(); ++transportListIndex)
        {
            DeleteTrasportFromCell(edge.m_transportList[transportListIndex]);
        }
        //for (int index2 = 0; index2 < edge.m_transportList.Count(); ++index2)
        //{
        //    var cell = edge.m_transportList[index2];
        //    if (cell.transform.childCount > 1)
        //    {
        //        int childCount = cell.transform.childCount;
        //        for (int childIndex = 0; childIndex < childCount; ++childIndex)
        //        {
        //            var child = cell.transform.GetChild(childIndex);
        //            Destroy(child.gameObject);
        //        }
        //    }

        //}
    }

    private void DeleteTrasportFromCell(GameObject cell)
    {
        cell.transform.DetachChildren();
        for (int index = cell.transform.childCount - 1; index >= 0 ; --index)
        {
            Destroy(cell.transform.GetChild(index));
        }
    }
    #endregion

    public void LoadVictoryDisplay()
    {
        StopGame();
        victoryDisplay.SetActive(true);
    }

    public void LoadDefeatDisplay()
    {
        StopGame();
        defeatDisplay.SetActive(true);
    }

    public void LoadNewLevel()
    {
        ++currentLevel;
        currentLevelInfoManager.SetCurrentLevel(currentLevel);
        Debug.Log("Load level = " + currentLevel.ToString());
        if (currentLevel < levelManager.levelCount)
        {
            RecreateLevel();
        }
    }

    public void RecreateLevel()
    {
        DeleteTrasports();
        Start();
    }

    public void Replay()
    {
        SaveLevelInfo();
        RecreateLevel();
    }

    public void SaveLevelInfo()
    {
        // TODO : нет таймера
        levelManager.ChangeLevel(currentLevel, 0, 0);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}

