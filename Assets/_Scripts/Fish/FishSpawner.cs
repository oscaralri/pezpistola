using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishPrefabs;
    [SerializeField] private Vector3 posSpawn;
    [SerializeField] private float timeOnGun = 2f;
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
        fishInstance = Instantiate(fishPrefabs[random], posSpawn, Quaternion.Euler(90f, 0f, 0f));
        Rigidbody rb = fishInstance.GetComponent<Rigidbody>();

        if (GameManager.Instance.gameState == GameState.WaitForFish) GameManager.Instance.gameState = GameState.CatchFishGame;

        StartCoroutine(Despawn(fishInstance, callback));
    }

    private IEnumerator Despawn(GameObject instance, Action callback)
    {
        yield return new WaitForSeconds(timeDespawn);
        if (GameManager.Instance.gameState == GameState.CatchFishGame)
        {
            //Destroy(instance);
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
            foreach(var fish in fishes)
            {
                Destroy(fish);
            }
            callback?.Invoke();
        }
        if(GameManager.Instance.gameState == GameState.GunGame)
        {
            yield return new WaitForSeconds(timeOnGun);
            //Destroy(instance);
            GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
            foreach (var fish in fishes)
            {
                Destroy(fish);
            }
            callback?.Invoke();
        }
    }
}
