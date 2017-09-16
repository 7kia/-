using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private float m_currentTime = int.MaxValue;
    public bool m_endTime = false;
    private bool m_stopTimer = true;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_stopTimer)
        {
            m_currentTime -= Time.deltaTime;
            if (m_currentTime <= 0f)
            {
                m_endTime = true;
                StopTimer();
            }
        }
    }

    public void SetTime(float time)
    {
        m_currentTime = time;
        m_endTime = false;
    }

    public float GetTime()
    {
        return m_currentTime;
    }

    public void StopTimer()
    {
        m_stopTimer = true;
    }

    public void PlayTimer()
    {
        m_stopTimer = false;
    }

}
