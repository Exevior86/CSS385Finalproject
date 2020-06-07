﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public float timer = 20f;
    public GameObject player;
    private static Vector2 screenBounds;
    private static string newpowerup;
    private static Vector3 newpos;
    private static PowerupManager instance;
    private int currentKills;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (currentKills != ScoreScript.VirusKilled)
        {
            currentKills = ScoreScript.VirusKilled;
            SpawnCheck();
        }
    }

    void SpawnCheck()
    {
        int wide = (ScoreScript.VirusKilled + 1) % (81 / LevelController.difficulty) + (5 * LevelController.difficulty);
        int rapid = (ScoreScript.VirusKilled + 1) % (91 / LevelController.difficulty) + (5 * LevelController.difficulty);
        int heart = (ScoreScript.VirusKilled + 1) % (101 / LevelController.difficulty) + (5 * LevelController.difficulty);
        int shield = (ScoreScript.VirusKilled + 1) % (76 / LevelController.difficulty) + (5 * LevelController.difficulty);

        newpos = player.transform.position;
        if (wide == 0)
        {
            newpowerup = "Prefabs/powerupWide";
            SpawnReal();
        }
        if (rapid == 0)
        {
            newpowerup = "Prefabs/RapidFire";
            SpawnReal();
        }
        if (heart == 0)
        {
            newpowerup = "Prefabs/Health";
            SpawnReal();
        }
        if (shield == 0)
        {
            newpowerup = "Prefabs/Shield";
        }
    }

    public static void Spawn(string powerup, Vector3 pos)
    {
        newpowerup = powerup;
        newpos = pos;
        instance.Invoke("SpawnReal", instance.timer);
    }

    public void SpawnReal()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        bool powerupSpawned = false;
        while (!powerupSpawned)
        {
            Vector3 powerupPosition = new Vector3(Random.Range(0, screenBounds.x), Random.Range(0, screenBounds.y), 0f);
            if ((powerupPosition - newpos).magnitude < 1)
            {
                continue;
            }
            else
            {
                Object loaded = Resources.Load(newpowerup);
                Instantiate(loaded, powerupPosition, Quaternion.identity);
                powerupSpawned = true;
            }
        }
    }
}
