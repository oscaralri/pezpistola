using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFishingSystem
{
   FishingState _fishingState {get; set;}
}

public enum FishingState {Waiting, Fishing}
