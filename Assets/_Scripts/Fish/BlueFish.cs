using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFish : MonoBehaviour
{
    public FishType fishType {get; private set;} = FishType.Blue;
    [SerializeField] private Collider hitCollider;
}
