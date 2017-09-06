using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CurrentLevelInfoManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    const string CURRENT_LEVEL_INFO_PATH = "CurrentLevel.xml";

    public void SetCurrentLevel(int levelId)
    {
        XmlDocument xmlDoc = new XmlDocument();

        if (xmlDoc.FirstChild == null)
        {
            xmlDoc.LoadXml(GenerateNewXMLLevelInfoString());
        }

        XmlNode currentLevelInfo = xmlDoc.ChildNodes[1];
        //var currentLevelInfo = levelsNode.SelectSingleNode("currentLevel");

        SetAtribute(ref currentLevelInfo, "id", levelId.ToString());


        xmlDoc.Save(CURRENT_LEVEL_INFO_PATH);
    }

    public int GetCurrentLevelId()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(CURRENT_LEVEL_INFO_PATH);

        XmlNode currentLevelInfo = xmlDoc.ChildNodes[1];

        return Convert.ToInt32(currentLevelInfo.Attributes["id"].Value);
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
            "<currentLevel id=\"0\"/> \n";
    }

}
