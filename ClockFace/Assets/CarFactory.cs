//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace CarFactoryNamespace
//{

//    public class CarFactory : MonoBehaviour
//    {
//        public Transform spawnLocations;
//        public GameObject whatToSpawnPrefab;
//        public GameObject[] whatToSpawnClone;
//        public CCarCounter carCounter = new CCarCounter(0);


//        public class CCarCounter
//        {
//            public CCarCounter(int count)
//            {
//                m_maxCount = count;
//            }

//            public void SetMax(int value)
//            {
//                m_maxCount = value;
//            }

//            public bool Increase()
//            {
//                if (m_count < m_maxCount)
//                {
//                    m_count++;
//                    return true;
//                }
//                return false;
//            }

//            public bool Decrease()
//            {
//                if (m_count > 0)
//                {
//                    m_count--;
//                    return true;
//                }
//                return false;
//            }

//            private int m_count = 0;
//            private int m_maxCount = 0;
//        }


//        public CarFactory()
//        {
//        }

//        public void CreateCar()
//        {
//            //if (carCounter.Increase())
//            //{
//                //whatToSpawnClone.Insert
//                whatToSpawnClone[0] = Instantiate(
//                    GameManager.Instance.ClickedTransportBtn.TransportPrefab,
//                    spawnLocations.transform.position,
//                    Quaternion.Euler(0, 0, 0)) as GameObject;

//            //}

//        }

//        void Update()
//        {
//            if (Input.GetMouseButtonDown(0))
//            {
//               // CreateCar();
//            }
//        }
//    }

   

//}
