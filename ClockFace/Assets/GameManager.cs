using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using CarFactoryNamespace;

public class GameManager : MonoBehaviour
{
    public TransportButton ClickedTransportBtn { get; private set; }
    public GameObject spawnLocation;
    private List<GameObject> transports = new List<GameObject>();
    private int lastIndex = 0;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PickTransport(TransportButton transportBtn)
    {
        this.ClickedTransportBtn = transportBtn;

        transports.Add(
            Instantiate(
                        transportBtn.TransportPrefab,
                        transportBtn.SpawnLocation.transform.position,
                        Quaternion.Euler(0, 0, 0)
                   ) as GameObject
              );

        transports[lastIndex].transform.parent = spawnLocation.transform;

        lastIndex++;
    }
}

