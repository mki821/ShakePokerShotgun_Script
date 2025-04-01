using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public void TimeStop()
    {
        Time.timeScale = 0;
    }

    public void TimeStart()
    {
        Time.timeScale = 1;
    }
}
