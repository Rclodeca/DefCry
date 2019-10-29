using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets instance;

    private void Awake()
    {
        instance = this;
    }

    public Transform pfProjectile;
    public Transform pfEnemy;
    public Transform pfSpawner;
    public Transform pfWave;
}
