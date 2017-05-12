using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CarFactoryNamespace
{

public class CarFactory : MonoBehaviour {

    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

        // public List<CCarCounter> carCounters = new List<CCarCounter>();
    public List<int> transportIndex = new List<int>()
    {
        // -1 not index
        0// 1
        -1,// 2
        -1,// 3
        1,// 4
        -1,// 5
        -1,// 6
        -1,// 7
        -1,// 8
        -1,// 9
        -1,// 10
    };

        public class CCarCounter
    {
        public CCarCounter(int count, int carNumber)
        {
            m_maxCount = count;
            m_carNumber = carNumber;
        }

        public bool Increase()
        {
            if (m_count < m_maxCount)
            {
                m_count++;
                return true;
            }
            return false;
        }

        public bool Decrease()
        {
            if (m_count > 0)
            {
                m_count--;
                return true;
            }
            return false;
        }

        private int m_count = 0;
        private int m_maxCount = 0;
        private int m_carNumber = 0;
    }

    public CarFactory()
    {   
        //carCounters.Add(new CCarCounter(3, 1));
    }

    protected void CreateCar(int number)
    {
        switch (number)
        {
            case 1:
                //if(carCounters[0].Increase())
               // {
                    whatToSpawnClone[transportIndex[number - 1]] = Instantiate(
                        whatToSpawnPrefab[transportIndex[number - 1]],
                        spawnLocations[transportIndex[number - 1]].transform.position,
                        Quaternion.Euler(0, 0, 0)) as GameObject;

                //}
                break;
            case 4:
                //if(carCounters[1].Increase())
                // {
                whatToSpawnClone[transportIndex[number - 1]] = Instantiate(
                    whatToSpawnPrefab[transportIndex[number - 1]],
                    spawnLocations[transportIndex[number - 1]].transform.position,
                    Quaternion.Euler(0, 0, 0)) as GameObject;

                //}
                break;
            }
    }

}

}
