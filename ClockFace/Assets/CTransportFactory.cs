using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTransportFactory : MonoBehaviour {

    public GameObject spawnLocations;
    public GameObject[] whatToSpawnClone;
    public List<CCarCounter> carCounter = new List<CCarCounter>()
    {
        new CCarCounter(0),
        new CCarCounter(0),
        new CCarCounter(0),
        new CCarCounter(0),
        new CCarCounter(0)

    };
    private int lastIndex = 0;

    public class CCarCounter
    {
        public CCarCounter(int count)
        {
            m_maxCount = count;
        }

        public void SetMax(int value)
        {
            m_maxCount = value;
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

        public int m_count = 0;
        private int m_maxCount = 0;

        public void Reset()
        {
            m_count = 0;
        }
    }

    public void UpdateCounters(List<int> countTransports)
    {
        
        for(int index = 0; index < countTransports.Count; index++)
        {
            carCounter[index].Reset();
            Debug.Log(countTransports[index]);
            Debug.Log(carCounter[index].m_count);
            carCounter[index].SetMax(countTransports[index]);
        }
    }

    public void CreateCar(TransportButton transportBtn)
    {
        int counterIndex = transportBtn.m_couterIndex;
        if (carCounter[counterIndex].Increase())
        {
            whatToSpawnClone[lastIndex] = Instantiate(
                transportBtn.TransportPrefab,
                spawnLocations.transform.position,
                Quaternion.Euler(0, 0, 0)
                ) as GameObject;

            whatToSpawnClone[lastIndex].transform.parent = spawnLocations.transform;
            //++lastIndex;
            Debug.Log("Car number " + counterIndex.ToString() + " amount = " + carCounter[counterIndex].m_count.ToString());

        }
    }

    
}
