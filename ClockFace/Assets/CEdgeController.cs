using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEdgeController : MonoBehaviour {

    public CEdge[] m_edges;
    public int m_resultNumber;
    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update()
    {

        bool numberIsCompute = true;
        foreach(CEdge edge in m_edges)
        {
            int summEdge = 0;
            foreach(GameObject transportCell in edge.m_transportList)
            {
                if (transportCell.transform.childCount == 1)
                {
                    summEdge += transportCell.GetComponentInChildren<CTransport>().m_transportNumber;
                }
            }
            numberIsCompute &= (summEdge == m_resultNumber);
            //Debug.Log("Edge result = " + m_resultNumber.ToString());
            // TODO : delete debug info
            if (summEdge == m_resultNumber)
            {
              // Debug.Log("Edge result = " + m_resultNumber.ToString());
            }
        }
        // TODO : delete debug info
        if (numberIsCompute)
        {
            Debug.Log("Compute number = " + m_resultNumber.ToString());
        }


    }

   
}
