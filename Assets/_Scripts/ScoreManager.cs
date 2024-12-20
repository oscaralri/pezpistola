using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int points {get; private set;} = 0;
    public int misses {get; private set;} = 0;
    [SerializeField] private TextMeshProUGUI _pointsText, _missesText;

    public void AddPoints()
    {
        points++;
        _pointsText.text = $"_{points}";
    }

    public void AddMisses()
    {
        if(++misses < 3)
        {
            _missesText.text = $"xxx {misses}/3";
        }
        else
        {
            Debug.Log("has perdido");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
}
