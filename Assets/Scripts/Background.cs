﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    private LevelManager levelManager;
    private List<Sprite> backgroundList = new List<Sprite>();

    public Sprite background1;
    public Sprite background2;
    public Sprite background3;
    public Sprite background4;
    public List<Sprite> BackgroundList { get { return backgroundList; } set { value = backgroundList; } }

    void Start()
    {
        levelManager = LevelManager.instance;
        backgroundList.Add(background1);
        backgroundList.Add(background2);
        backgroundList.Add(background3);
        backgroundList.Add(background4);

        gameObject.GetComponent<Image>().sprite = backgroundList[levelManager.CurrentLevel - 1];
    }
}
