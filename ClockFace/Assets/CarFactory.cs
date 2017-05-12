using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFactory : MonoBehaviour {

    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

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

    private CCarCounter[] carCounters;
    public CarFactory()
    {   
        carCounters[0] = new CCarCounter(3, 1);
    }

    void CreateCar(int number)
    {
        switch (number)
        {
            case 1:
                if(carCounters[0].Increase())
                {
                    whatToSpawnClone[0] = Instantiate(
                        whatToSpawnPrefab[0],
                        spawnLocations[0].transform.position,
                        Quaternion.Euler(0, 0, 0)) as GameObject;

                }
                break;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //  CreateCar(1);
        }
    }
}
