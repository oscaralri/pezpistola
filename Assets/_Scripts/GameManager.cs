using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {IdleGame, FishingGame, GunGame}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public GameState gameState = GameState.IdleGame;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("@GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
