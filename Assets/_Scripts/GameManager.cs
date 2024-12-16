using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum GameState { IdleGame, WaitForFish, CatchFishGame, GunGame }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState;
    private FishSpawner _fishSpawner;
    [SerializeField] private InputSystem _inputSystem;
    private CameraMovement _camera;
    [SerializeField] AnimationManager _animationManager;
    [SerializeField] public GameObject _gun, _fishRod;
    public ScoreManager scoreManager;

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
        scoreManager = GetComponent<ScoreManager>();
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
        switch (GameManager.Instance.gameState)
        {
            case GameState.IdleGame:
                BeforeWaitForFish();

                break;

            case GameState.WaitForFish:
                ReturnFishRod();
                break;

            case GameState.CatchFishGame:
                Debug.Log("Pez pillado");
                BeforeGunGame();

                break;

            case GameState.GunGame:
                Shoot();
                _animationManager.PlayAnim(Anim.Gun, FinishShot);
                break;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance.gameState == GameState.CatchFishGame)
        {
            GameManager.Instance.gameState = GameState.WaitForFish;
            _fishSpawner.SpawnFish(OnDestroy);
        }
        if (GameManager.Instance.gameState == GameState.GunGame)
        {
            _camera.ResetCamera();
            _gun.SetActive(false);
            _fishRod.SetActive(true);
            GameManager.Instance.gameState = GameState.IdleGame;
        }

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
        _inputSystem.enabled = false;
    }

    private void FinishShot()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _inputSystem.enabled = true;
    }

    private void BeforeWaitForFish()
    {
        _inputSystem.enabled = false;
        if (Cursor.visible) Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _fishSpawner.SpawnFish(OnDestroy);
        _animationManager.PlayAnim(Anim.FishRodThrow, FinishFishRodThrow);
    }

    private void FinishFishRodThrow()
    {
        _inputSystem.enabled = true;
        gameState = GameState.WaitForFish;
    }

    private void BeforeGunGame()
    {
        _inputSystem.enabled = false;
        _fishRod.SetActive(false);
        _camera.ZoomCamera();
        ApplyForce(_fishSpawner.fishInstance, _fishSpawner.force);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _gun.SetActive(true);
        _inputSystem.enabled = true;

        gameState = GameState.GunGame;
    }

    private void ReturnFishRod()
    {
        _animationManager.PlayAnim(Anim.FishRodThrow, () => gameState = GameState.IdleGame);
    }
}
