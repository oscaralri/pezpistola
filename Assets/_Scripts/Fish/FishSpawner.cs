using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishPrefabs;
    [SerializeField] private Vector3 posToSpawn;
    [SerializeField] private Quaternion rotationSpawn;
    public GameObject fishInstance;
    public Vector3 force;
    private float timeDespawn = 2f;

    public void SpawnFish(Action callback)
    {
        StartCoroutine(SpawnCoroutine(callback));
    }

    private IEnumerator SpawnCoroutine(Action callback)
    {
        int randomTime = UnityEngine.Random.Range(2, 6); // AUMENTARLO AL ACABAR
        yield return new WaitForSeconds(randomTime);

        int random = UnityEngine.Random.Range(0, fishPrefabs.Count);
        fishInstance = Instantiate(fishPrefabs[random], posToSpawn, rotationSpawn);

        GameManager.Instance.gameState = GameState.CatchFishGame;

        StartCoroutine(Despawn(fishInstance, callback));
    }

    private IEnumerator Despawn(GameObject instance, Action callback)
    {
        yield return new WaitForSeconds(timeDespawn);
        if(GameManager.Instance.gameState != GameState.GunGame) 
        {
            Destroy(instance);
            callback?.Invoke();
        }
    }
}
