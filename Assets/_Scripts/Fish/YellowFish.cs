using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowFish : MonoBehaviour
{
    public FishType fishType {get; private set;} = FishType.Yellow;
    [SerializeField] private Collider hitCollider;
}
