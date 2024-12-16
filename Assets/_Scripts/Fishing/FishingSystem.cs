using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSystem : MonoBehaviour, IFishingSystem 
{
    private FishSpawner _fishSpawner;

    private void Start()
    {
        _fishSpawner = GetComponent<FishSpawner>();
    }
/*
    private void Update()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameState.IdleGame:
                // esperando a realizar accion
                break;

            case GameState.FishingGame:
                // spawnear pez
                break;
            case GameState.CatchFishGame:
                // aparece pez -> comprobar si lo coge
                break;
            case GameState.GunGame:
                // ha pillado pez
                break;
        }
    }
    */
}
