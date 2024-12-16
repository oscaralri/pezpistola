using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState {IdleGame, WaitForFish, CatchFishGame, GunGame}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState;
    private FishSpawner _fishSpawner;
    [SerializeField] private InputSystem _inputSystem;
    private CameraMovement _camera;
    [SerializeField] AnimationManager _animationManager;

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
        _camera = Camera.main.GetComponent<CameraMovement>();
        Cursor.visible = false;
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
                if(Cursor.visible) Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _fishSpawner.SpawnFish(OnDestroy);
                gameState = GameState.WaitForFish;
                break;

            case GameState.WaitForFish:
                break;

            case GameState.CatchFishGame:
                Debug.Log("Pez pillado");
                _camera.ZoomCamera();
                ApplyForce(_fishSpawner.fishInstance, _fishSpawner.force);
                Cursor.visible = true;
                gameState = GameState.GunGame;
                break;

            case GameState.GunGame:
                Shoot();
                _animationManager.PlayAnim(Anim.Gun, FinishShot);
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
        rb.AddTorque(Vector3.left * 10f, ForceMode.Force);
    }

    private void Shoot()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _inputSystem.GetComponent<PlayerInput>().enabled = false;
    }

    private void FinishShot()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _inputSystem.GetComponent<PlayerInput>().enabled = true;
    }
}
