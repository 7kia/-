using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using CarFactoryNamespace;

public class GameManager : MonoBehaviour
{
    public CTransportFactory transportFactory;

    void Start()
    {
        transportFactory.UpdateCounters(new List<int>{ 4, 4} );
    }

    void Update()
    {
        
    }
    
}

