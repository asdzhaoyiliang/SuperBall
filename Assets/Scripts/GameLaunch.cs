﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    void Awake()
    {
        this.gameObject.AddComponent<GameApp>();
        // this.GameStart();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        // this.gameObject.AddComponent<GameApp>().EnterGame();
    }
}
