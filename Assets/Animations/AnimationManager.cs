using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public void PlayAnim(Anim anim, Action callback)
    {
        switch(anim)
        {
            case Anim.FishRodThrow:
                break;
            
            case Anim.FishRodReturn:
                break;

            case Anim.Gun:
                break;
        }
    }

    private void FishRodThrowAnim(Action callback)
    {
        
    }

    private void FishRodReturnAnim(Action callback)
    {

    }

    private void GunAnim(Action callback)
    {

    }
}

public enum Anim {FishRodThrow, Gun, FishRodReturn}
