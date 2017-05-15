using System.Collections;
using UnityEngine;

public class TransportButton : MonoBehaviour
{
    [SerializeField]
    private GameObject transportPrefab;

    public GameObject TransportPrefab
    {
        get
        {
            return transportPrefab;
        }
    }
}
