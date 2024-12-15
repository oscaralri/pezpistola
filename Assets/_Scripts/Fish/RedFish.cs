using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFish : IFish
{
    public FishType fishType {get; private set;} = FishType.Red;
    [SerializeField] private Collider hitCollider;
}
