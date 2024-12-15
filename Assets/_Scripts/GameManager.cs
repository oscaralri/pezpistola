using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState {IdleGame, WaitForFish, CatchFishGame, GunGame}

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    private FishSpawner _fishSpawner;


    [SerializeField] private InputSystem _inputSystem;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameState = GameState.IdleGame;
        _fishSpawner = GetComponent<FishSpawner>();
    }

    private void OnEnable()
    {
        _inputSystem.click += OnClick;
    }
    
    private void OnDisable()
    {
        _inputSystem.click -= OnClick;
    }

    private void OnClick()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameState.IdleGame:
                // esperando a realizar accion
                _fishSpawner.SpawnFish(OnDestroy);
                gameState = GameState.WaitForFish;
                break;

            case GameState.WaitForFish:
                break;

            case GameState.CatchFishGame:
                // aparece pez -> comprobar si lo coge
                Debug.Log("Pez pillado");
                gameState = GameState.GunGame;
                ApplyForce(_fishSpawner.fishInstance, _fishSpawner.force);
                break;

            case GameState.GunGame:
                // ha pillado pez
                break;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.gameState = GameState.WaitForFish;
        _fishSpawner.SpawnFish(OnDestroy);
    }

    private void ApplyForce(GameObject instance, Vector3 force)
    {
        
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse); 
    }
}
