using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class NonPlayableDemo : MonoBehaviour
{
    public List<GameObject> _npcDogs;
    public FreeLookCam _freeLookCamScript;
    GameObject _randomDog;
    Animator _animator;

    private bool _isWalking;
    private bool _isRunning;
    private bool _triggerSit;
    private bool _triggerLieDown;
    private bool _triggerGetUp;
    private bool _triggerJump;
    private bool _triggerSurprise;
    private bool _triggerBark;
    private bool _triggerBite;
    private bool _triggerDeath;

    // Use this for initialization
    void Start()
    {
        _randomDog = Instantiate(_npcDogs[Random.Range(0, _npcDogs.Count)]);
        _animator = _randomDog.GetComponent<Animator>();
        _freeLookCamScript.Target = _randomDog.transform;
    }

    private void Update()
    {
        UpdateAnimator();
    }

    public void ToggleWalk()
    {
        _isWalking = !_isWalking;
    }

    public void ToggleRunning()
    {
        _isRunning = !_isRunning;
    }

    public void Sit()
    {
        _triggerSit = true;
    }

    public void LayDown()
    {
        _triggerLieDown = true;
    }

    public void GetUp()
    {
        _triggerGetUp = true;
    }

    public void Bite()
    {
        _triggerBite = true;
    }

    public void Bark()
    {
        _triggerBark = true;
    }

    public void Surprise()
    {
        _triggerSurprise = true;
    }

    public void Die()
    {
        _triggerDeath = true;
    }

    public void Jump()
    {
        _triggerJump = true;
    }

    private void UpdateAnimator()
    {
        if (_triggerDeath && !Methods.AnimationBeingPlayed(_animator, AnimationNames.Die))
        {
            _animator.SetTrigger(AnimationParameters.Die);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.Idle) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Walk) || Methods.AnimationBeingPlayed(_animator, AnimationNames.Run))
        {
            _animator.SetBool(AnimationParameters.IsWalking, _isWalking);
            _animator.SetBool(AnimationParameters.IsRunning, _isRunning);

            if (_triggerJump)
                _animator.SetTrigger(AnimationParameters.Jump);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.Idle))
        {
            if (_triggerSit)
                _animator.SetTrigger(AnimationParameters.Sit);
            else if (_triggerLieDown)
                _animator.SetTrigger(AnimationParameters.LayDown);
            else if (_triggerSurprise)
                _animator.SetTrigger(AnimationParameters.Surprise);
            else if (_triggerBite)
                _animator.SetTrigger(AnimationParameters.Bite);
            else if (_triggerBark)
                _animator.SetTrigger(AnimationParameters.Bark);
        }

        if (Methods.AnimationBeingPlayed(_animator, AnimationNames.SitIdle) || Methods.AnimationBeingPlayed(_animator, AnimationNames.LyingDownIdle))
        {
            if (_triggerGetUp)
            {
                _animator.SetTrigger(AnimationParameters.GetUp);
            }
        }

        _triggerSit = false;
        _triggerLieDown = false;
        _triggerJump = false;
        _triggerGetUp = false;
        _triggerSurprise = false;
        _triggerBite = false;
        _triggerBark = false;
    }
}
