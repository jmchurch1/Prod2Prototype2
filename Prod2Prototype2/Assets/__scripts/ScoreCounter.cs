using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI killCounter;
    private ArrayList _enemyList = new ArrayList();

    private int _kills = 0;

    public static ScoreCounter ScoreInstance;

    private void Awake()
    {
        ScoreInstance = this;
    }

    private void IncrementKillCounter()
    {
        _kills += 1;
        killCounter.text = "Kills: " + _kills;
    }
    
    public void AddEnemy(GameObject currEnemy)
    {
        _enemyList.Add(currEnemy);
    }

    public void RemoveEnemy(GameObject currEnemy)
    {
        _enemyList.Remove(currEnemy);
        IncrementKillCounter();
    }
    
}
