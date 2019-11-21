using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause
{
    private bool isPause;

    public bool IsPause
    {
        get { return isPause; }
    }

    public void TogglePause()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            isPause = false;
        } else
        {
            Time.timeScale = 0;
            isPause = true;
        }
    }

}
