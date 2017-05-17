using System.Collections;
using UnityEngine;

public class TransportButton : MonoBehaviour
{
    [SerializeField]
    private GameObject transportPrefab;
    [SerializeField]
    private GameObject spawnLocation;
    [SerializeField]
    public int m_couterIndex;


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
