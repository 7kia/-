using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class CurrentLevelInfoManager : MonoBehaviour
{
    const string CURRENT_LEVEL_INFO_PATH = "CurrentLevel.xml";

    // Use this for initialization
    void Start()
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (!File.Exists(CURRENT_LEVEL_INFO_PATH))
        {
            xmlDoc.LoadXml(GenerateNewXMLLevelInfoString());
        }
        else
        {
            xmlDoc.Load(CURRENT_LEVEL_INFO_PATH);
        }

        xmlDoc.Save(CURRENT_LEVEL_INFO_PATH);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentLevel(int levelId)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (xmlDoc.FirstChild == null)
        {
            xmlDoc.LoadXml(GenerateNewXMLLevelInfoString());
        }

        XmlNode currentLevelInfo = xmlDoc.ChildNodes[1].ChildNodes[0];
        SetAtribute(ref currentLevelInfo, "id", levelId.ToString());

        xmlDoc.Save(CURRENT_LEVEL_INFO_PATH);
    }

    public void SetLastFinishedLevel(int levelId)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(CURRENT_LEVEL_INFO_PATH);

        XmlNode lastFinishedLevel = xmlDoc.ChildNodes[1].ChildNodes[1];
        if (levelId > Convert.ToInt32(lastFinishedLevel.Attributes["id"].Value))
        {
            SetAtribute(ref lastFinishedLevel, "id", levelId.ToString());
        }

        xmlDoc.Save(CURRENT_LEVEL_INFO_PATH);

    }

    public int GetCurrentLevelId()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(CURRENT_LEVEL_INFO_PATH);

        XmlNode currentLevelInfo = xmlDoc.ChildNodes[1].ChildNodes[0];

        return Convert.ToInt32(currentLevelInfo.Attributes["id"].Value);
    }

    public int GetLastFinishedLevel()
    {
        if(!File.Exists(CURRENT_LEVEL_INFO_PATH))
        {
            return -1;
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(CURRENT_LEVEL_INFO_PATH);

        XmlNode lastFinishedLevel = xmlDoc.ChildNodes[1].ChildNodes[1];

        return Convert.ToInt32(lastFinishedLevel.Attributes["id"].Value);
    }


    private void SetAtribute(
        ref XmlNode currNode,
        string atributeName,
        string atributeValue
    )
    {
        currNode.Attributes[atributeName].Value = atributeValue;
    }

    private string GenerateNewXMLLevelInfoString()
    {
        return "<?xml version=\"1.0\"?> \n" +
            "<levelInfo> \n" +
            "<currentLevel id=\"0\" /> \n" +
            "<lastFinishedLevel id=\"-1\"/> \n" +
            "</levelInfo>";
    }

}
