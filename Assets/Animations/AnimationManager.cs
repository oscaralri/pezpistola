using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject _fishRod, _gun;
    [SerializeField] private float gunRotationTime = 0.5f;

    public void PlayAnim(Anim anim, Action callback)
    {
        switch(anim)
        {
            case Anim.FishRodThrow:
                break;
            
            case Anim.FishRodReturn:
                break;

            case Anim.Gun:
                StartCoroutine(GunAnim(callback));
                break;
        }
    }

    private void FishRodThrowAnim(Action callback)
    {
        
    }

    private void FishRodReturnAnim(Action callback)
    {

    }

    private IEnumerator GunAnim(Action callback)
    {
        float elapsedTime = 0f;
        float targetRotation = 360f;

        while (elapsedTime < gunRotationTime)
        {
            float rotationAmount = Mathf.Lerp(0, targetRotation, elapsedTime / gunRotationTime);
            _gun.transform.rotation = Quaternion.Euler(-rotationAmount, 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _gun.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        callback();
    }
}

public enum Anim {FishRodThrow, Gun, FishRodReturn}
