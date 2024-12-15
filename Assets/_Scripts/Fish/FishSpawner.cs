using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> fishPrefabs;
    [SerializeField] private Vector3 posToSpawn;
    [SerializeField] private Quaternion rotationSpawn;

    public void SpawnFish()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        int randomTime = Random.Range(2, 6); // AUMENTARLO AL ACABAR
        yield return new WaitForSeconds(randomTime);

        int random = Random.Range(0, 3);
        Instantiate(fishPrefabs[random], posToSpawn, rotationSpawn);      
        GameManager.Instance.gameState = GameState.FishingGame;
    }

    // PARA DEBUG (BORRAR EN FUTURO)
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpawnFish();
        }
    }

}
