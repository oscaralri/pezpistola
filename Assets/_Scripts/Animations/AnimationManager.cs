using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private GameObject _fishRod, _gun;
    [SerializeField] private float gunRotationTime = 0.5f;
    private Animator _fishRodAnimator;

    private void Start()
    {
        _fishRod = GameManager.Instance._fishRod;
        _gun = GameManager.Instance._gun;
        
        _fishRodAnimator = _fishRod.GetComponent<Animator>();
    }

    public void PlayAnim(Anim anim, Action callback)
    {
        switch (anim)
        {
            case Anim.FishRodThrow:
                FishRodThrowAnim(callback);
                break;

            case Anim.FishRodReturn:
                FishRodReturnAnim(callback);
                break;

            case Anim.Gun:
                StartCoroutine(GunAnim(callback));
                break;
        }
    }

    private void FishRodThrowAnim(Action callback)
    {
        _fishRodAnimator.Play("fishRodAnim");
        StartCoroutine(WaitForAnim(callback));
    }

    private void FishRodReturnAnim(Action callback)
    {
        _fishRodAnimator.Play("fishRodReturnAnim");
        StartCoroutine(WaitForAnim(callback));
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

    private IEnumerator WaitForAnim(Action callback)
    {
        AnimatorStateInfo stateInfo = _fishRodAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        callback?.Invoke();
    }
}

public enum Anim { FishRodThrow, Gun, FishRodReturn }
