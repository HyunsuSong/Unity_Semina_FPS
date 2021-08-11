using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairUIController : MonoBehaviour
{
    [SerializeField]
    private Animator crossHairAnimator;

    public float poseAccuracy = 0.0f;

    public void WalkingAnim(bool _isWalk)
    {
        crossHairAnimator.SetBool("IsWalk", _isWalk);
    }

    public void RunningAnim(bool _isRun)
    {
        crossHairAnimator.SetBool("IsRun", _isRun);
    }

    public void FireAnim()
    {
        if(crossHairAnimator.GetBool("IsWalk"))
        {
            crossHairAnimator.SetTrigger("WalkFire");
            poseAccuracy = 0.1f;
        }
        else
        {
            crossHairAnimator.SetTrigger("IdleFire");
            poseAccuracy = 0.08f;
        }
    }
}
