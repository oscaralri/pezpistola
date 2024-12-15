using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingSystem : MonoBehaviour, IFishingSystem 
{
    public FishingState _fishingState { get; private set; } = FishingState.Waiting;
    [SerializeField] private InputSystem _inputSystem;

    private FishSpawner _fishSpawner;

    private void Start()
    {
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

    private void Update()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameState.IdleGame:
                _fishSpawner.SpawnFish();
                break;

            case GameState.FishingGame:
                break;

            case GameState.GunGame:
                break;
        }
    }

    private void OnClick()
    {
        _fishingState = _fishingState == FishingState.Waiting ? FishingState.Fishing : FishingState.Waiting;
        Debug.Log("_fishingState: " + _fishingState);
    }
}
