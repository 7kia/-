using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Tuple;
namespace ConsoleApplication1
{
    using TimeList = List<MyTime>;

    class MyTime
    {
        public MyTime(int minutes, int seconds)
        {
            m_minutes = minutes;
            m_seconds = seconds;
        }
        public int m_minutes = 0;
        public int m_seconds = 0;
    }
    class Program
    {
                
        class LevelManager
        {
class LevelInfo
            {
                int edgeNumber = 4;
                int countNumber = 3;
                int amountNumberToEdge = 9;
                int minutes = 0;
                int seconds = 0;
                int award = 0;
            }

            const string LEVEL_INFO_PATH = "Levels.xml";
            const string TIMES_INFO_PATH = "Times.xml";

            const int LEVEL_COUNT = 6;
            const string NO_TIME = "...";
            private List<LevelInfo> m_levelInfos = new List<LevelInfo>();
            private List<TimeList > levelTimeList = new List<TimeList>();

            void Start()
            {
                LoadTimesForLevels();
                ChangeLevel(2, 1, 33);
            }


            public void CreateLevelInfo()
            {
                m_levelInfos.Clear();
                XmlDocument xmlDoc = new XmlDocument();

                if (xmlDoc.FirstChild == null)
                {
                    xmlDoc.LoadXml(GenerateNewXMLLevelInfoString());
                }

                XmlNode levelsNode = xmlDoc.ChildNodes[1];
                var levelList = levelsNode.SelectNodes("level");

                for (int index = 0; index < LEVEL_COUNT; ++index)
                {
                    if (levelList.Count != 0)
                    {
                        XmlNode currNode = levelList[index];
                        SetAtribute(ref currNode, "Minutes", NO_TIME);
                        SetAtribute(ref currNode, "Seconds", NO_TIME);
                        SetAtribute(ref currNode, "Award", "1");
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



            public void ChangeLevel(int levelId, int minutes, int seconds)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(LEVEL_INFO_PATH);


                var awardTimeList = levelTimeList[levelId];
                int awardValue = 0;
                // TODO : Check check-algorithm
                for (int index = awardTimeList.Count - 1; index >= 0; --index)
                {
                    if (awardTimeList[index].m_minutes >= minutes)
                    {
                        if (awardTimeList[index].m_seconds >= seconds)
                        {
                            awardValue = index + 1;
                            break;
                        }
                    }
                }


                XmlNode levelsNode = xmlDoc.ChildNodes[1];
                var levelList = levelsNode.SelectNodes("level");
                var editNode = levelList[levelId];

                SetAtribute(ref editNode, "minutes", minutes.ToString());
                SetAtribute(ref editNode, "seconds", seconds.ToString());
                SetAtribute(ref editNode, "award", awardValue.ToString());

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
                AddAtribute(doc, ref levelInfoNode, "minutes", NO_TIME);
                AddAtribute(doc, ref levelInfoNode, "seconds", NO_TIME);
                AddAtribute(doc, ref levelInfoNode, "award", "0");

                return levelInfoNode;
            }

            #endregion

        }


        static void Main(string[] args)
        {
            LevelManager l = new LevelManager();
            l.CreateLevelInfo();
        }

    }




}

