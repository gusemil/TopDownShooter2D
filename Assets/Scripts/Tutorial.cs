using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    private int tutorialPhase;
    private LevelManager lvlManager;
    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        lvlManager = LevelManager.instance;

        currentLevel = LevelManager.instance.CurrentLevel;
        tutorialPhase = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
