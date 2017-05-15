using System.Collections;
using UnityEngine;

public class TransportButton : MonoBehaviour
{
    [SerializeField]
    private GameObject transportPrefab;
    [SerializeField]
    private GameObject spawnLocation;

    public GameObject TransportPrefab
    {
        get
        {
            return transportPrefab;
        }
    }
    public GameObject SpawnLocation
    {
        get
        {
            return spawnLocation;
        }
    }
}
