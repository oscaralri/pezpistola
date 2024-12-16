using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int points {get; private set;} = 0;
    public int misses {get; private set;} = 0;
    [SerializeField] private TextMeshProUGUI _pointsText, _missesText;

    public void AddPoints()
    {
        points++;
        _pointsText.text = $"Points: {points}";
    }

    public void AddMisses()
    {
        if(++misses < 3)
        {
            _missesText.text = $"Misses: {misses}/3";
        }
        else
        {
            Debug.Log("has perdido");
        }
    }
}
