using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Xml;
using System.Collections.Generic;


namespace MyGame
{
    using System.IO;
    using TimeList = List<MyTime>;

    public class MyTime
    {
        public MyTime(int minutes, int seconds)
        {
            m_minutes = minutes;
            m_seconds =seconds;
        }
        public int m_minutes = 0;
        public int m_seconds = 0;
    }

    public class LevelManager : MonoBehaviour
    {

        #region Info for create levels
        //private Li
        const int EDGE_NUMBER_COUNT = 3;
        const int SIMPLE_FUGURE_EDGE_COUNT = 4;
        const int COMPLEX_FUGURE_EDGE_COUNT = 6;
        public const int START_COMPLEX_SHAPE_LEVEL = 3;
        public const int MAX_LEVEL = 5;
        public Dictionary<int, int> carCounts = new Dictionary<int, int>();
        private List<List<int>> edgeNumbers = new List<List<int>>();
        private int m_curretnLevel = 0;
        public int m_resultNumber = 0;
        public void SetCurrentLevel(int levelId)
        {
            m_curretnLevel = levelId;
            if (levelId < START_COMPLEX_SHAPE_LEVEL)
            {
                FillEdges(SIMPLE_FUGURE_EDGE_COUNT);

            }
            else
            {
                FillEdges(COMPLEX_FUGURE_EDGE_COUNT);
            }


            RecreateCarCounts();
            foreach(var numbers in edgeNumbers)
            {
                foreach(var number in numbers)
                {
                    if(carCounts.ContainsKey(number))
                    {
                        ++carCounts[number];
                    }
                }
            }
            
        }

        private void FillEdges(int edgeCount)
        {
            int transportNumber = (m_curretnLevel < START_COMPLEX_SHAPE_LEVEL) 
                ? (m_curretnLevel + 1) 
                : m_curretnLevel - START_COMPLEX_SHAPE_LEVEL + 1;



            m_resultNumber = 0;
            edgeNumbers.Clear();
            for (int index = 0; index < edgeCount; ++index)
            {
                List<int> edge = new List<int>();

                // TODO : для следующих граней, край от прошлой грани не считаем
                if (index == 0)
                {
                    edge.Add(transportNumber * 2);
                }
                edge.Add(1);
                if (index != (edgeCount - 1))
                {
                    edge.Add(transportNumber * 2);
                }

                // TODO : добавь новых машинок
                edgeNumbers.Add(edge);
            }
            SetAmountFromEdge(edgeNumbers[0]);
        }

        private void SetAmountFromEdge(List<int> numbers)
        {
            if (m_resultNumber == 0)
            {
                foreach (var value in numbers)
                {
                    m_resultNumber += value;
                }
            }
        }
        #endregion


        public class LevelInfo
        {
            public int edgeNumber = 4;
            public int countNumber = 3;
            public int amountNumberToEdge = 9;
            public int minutes = 0;
            public int seconds = 0;
            public int award = 0;
        }

        const string LEVEL_INFO_PATH = "Levels.xml";
        const string TIMES_INFO_PATH = "Times.xml";

        const int LEVEL_COUNT = 6;
        public List<LevelInfo> m_levelInfos = new List<LevelInfo>();
        private List<TimeList> levelTimeList = new List<TimeList>();
        public List<MyTime> m_timeForDefeat = new List<MyTime>();
        // Use this for initialization
        void Start()
        {
            LoadTimesForLevels();
            RecreateCarCounts();
            LoadLevelInfo();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void RecreateCarCounts()
        {
            carCounts.Clear();
            carCounts = new Dictionary<int, int>();

            carCounts.Add(1, 0);
            for (int index = 1; index < MAX_LEVEL; ++index)
            {
                carCounts.Add(index * 2, 0);
            }
        }

        public void LoadLevelInfo()
        {
            m_levelInfos.Clear();
            XmlDocument xmlDoc = new XmlDocument();
    
            xmlDoc.Load(LEVEL_INFO_PATH);

            XmlNode levelsNode = xmlDoc.ChildNodes[1];
            var levelList = levelsNode.SelectNodes("level");

            for (int index = 0; index < LEVEL_COUNT; ++index)
            {
                var levelInfo = new LevelInfo();
                if (levelList.Count != 0)
                {
                    XmlNode currNode = levelList[index];
                    levelInfo.minutes = Convert.ToInt32(GetAtribute(ref currNode, "minutes"));
                    levelInfo.seconds = Convert.ToInt32(GetAtribute(ref currNode, "seconds"));
                    levelInfo.award = Convert.ToInt32(GetAtribute(ref currNode, "award"));
                }
                m_levelInfos.Add(levelInfo);
            }
            
            xmlDoc.Save(LEVEL_INFO_PATH);
        }

        public void CreateLevelInfo()
        {
            m_levelInfos.Clear();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(GenerateNewXMLLevelInfoString());

            XmlNode levelsNode = xmlDoc.ChildNodes[1];
            var levelList = levelsNode.SelectNodes("level");

            for (int index = 0; index < LEVEL_COUNT; ++index)
            {
                if (levelList.Count != 0)
                {
                    XmlNode currNode = levelList[index];
                    SetAtribute(ref currNode, "minutes", "0");
                    SetAtribute(ref currNode, "seconds", "0");
                    SetAtribute(ref currNode, "award", "0");
                    continue;
                }
                levelsNode.AppendChild(AddLevelInfo(xmlDoc, index));
                m_levelInfos.Add(new LevelInfo());
            }
            
            xmlDoc.Save(LEVEL_INFO_PATH);
        }

        #region Load award times

        private void LoadTimesForLevels()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(TIMES_INFO_PATH);

            XmlNode levelsNode = xmlDoc.ChildNodes[1];
            var levelList = levelsNode.SelectNodes("level");


            for (int index = 0; index < LEVEL_COUNT; ++index)
            {
                if (levelList[index].Attributes["id"].Value != index.ToString())
                {
                    throw new Exception("time level id not correspond necessary order");
                }

                levelTimeList.Add(GetLevelAwardTimeList(levelList[index]));
            }

            for (int index = 0; index < LEVEL_COUNT; ++index)
            {
                m_timeForDefeat.Add(new MyTime(levelTimeList[index][0].m_minutes, levelTimeList[index][0].m_seconds));
            }
        }

        private TimeList GetLevelAwardTimeList(XmlNode levelInfoNode)
        {
            TimeList timelist = new TimeList();

            var awardList = levelInfoNode.ChildNodes;
            for (int awardId = 0; awardId < awardList.Count; ++awardId)
            {
                if (awardList[awardId].Attributes["id"].Value != awardId.ToString())
                {
                    throw new Exception("time level id not correspond necessary order");
                }
                timelist.Add(
                    new MyTime(
                        Convert.ToInt32(awardList[awardId].Attributes["minutes"].Value),
                        Convert.ToInt32(awardList[awardId].Attributes["seconds"].Value)
                    )
                );
            }

            return timelist;
        }

        #endregion

        #region Change and resave level

        private void SetAtribute(
            ref XmlNode currNode,
            string atributeName,
            string atributeValue
        )
        {
            currNode.Attributes[atributeName].Value = atributeValue;
        }

        private string GetAtribute(
            ref XmlNode currNode,
            string atributeName
        )
        {
            return currNode.Attributes[atributeName].Value;
        }

        public void ChangeLevel(int levelId, int minutes, int seconds)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(LEVEL_INFO_PATH);


            var awardTimeList = levelTimeList[levelId];
            int awardValue = 1;
            // TODO : Check check-algorithm
            for (int index = awardTimeList.Count - 1; index >= 0; --index)
            {
                // 0 43

                // 1 0
                // 0 43
                if ((awardTimeList[index].m_minutes * 60 + awardTimeList[index].m_seconds) 
                    >= (minutes * 60 + seconds))
                {
                    awardValue = index + 1;
                    break;
                }
            }


            XmlNode levelsNode = xmlDoc.ChildNodes[1];
            var levelList = levelsNode.SelectNodes("level");
            var editNode = levelList[levelId];

            SetAtribute(ref editNode, "minutes", minutes.ToString());
            SetAtribute(ref editNode, "seconds", seconds.ToString());
            SetAtribute(ref editNode, "award", awardValue.ToString());

            Debug.Log("levelId " + levelId.ToString());
            Debug.Log("minutes " + minutes.ToString());
            Debug.Log("seconds " + seconds.ToString());
            Debug.Log("award " + awardValue.ToString());

            xmlDoc.Save(LEVEL_INFO_PATH);

        }

        #endregion

        #region Add XML elements and attributes

        private string GenerateNewXMLLevelInfoString()
        {
            string xml = "<?xml version=\"1.0\"?> \n" +
                "<levels> \n" +
                "</levels>";
            return xml;
        }

        private void AddAtribute(
            XmlDocument doc,
            ref XmlElement xmlElement,
            string atributeName,
            string atributeValue
        )
        {
            XmlAttribute attribute = doc.CreateAttribute(atributeName);
            attribute.Value = atributeValue;
            xmlElement.Attributes.Append(attribute);
        }


        public XmlElement AddLevelInfo(XmlDocument doc, int levelId)
        {
            XmlElement levelInfoNode = doc.CreateElement("level");

            AddAtribute(doc, ref levelInfoNode, "id", levelId.ToString());
            AddAtribute(doc, ref levelInfoNode, "minutes", "0");
            AddAtribute(doc, ref levelInfoNode, "seconds", "0");
            AddAtribute(doc, ref levelInfoNode, "award", "0");

            return levelInfoNode;
        }

        #endregion


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

}
