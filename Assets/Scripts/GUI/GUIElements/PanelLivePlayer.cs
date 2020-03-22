using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLivePlayer : MonoBehaviour
{
   [SerializeField] private Transform content;
    private GameObject liveGamebjectPrefab;
    private List<GameObject> lives;
    private bool isInit = false;

    public void Init(int countLive, GameObject liveGamebjectPrefab)
    {
        if (isInit) return;
        lives = new List<GameObject>();
        this.liveGamebjectPrefab = liveGamebjectPrefab;
        for (int i = 0; i < countLive; i++)
        {
            lives.Add(Instantiate(this.liveGamebjectPrefab, content));
        }

        isInit = true;
        SpaceShooterGame.Instance.DamagePlayerAction += () => DisplayLive(SpaceShooterGame.Instance.CountLive);
    }

    public void DisplayLive(int countLive)
    {
        for (var index = 0; index < lives.Count; index++)
        {
            var live = lives[index];
            live.SetActive(index < countLive);
        }
    }

}
