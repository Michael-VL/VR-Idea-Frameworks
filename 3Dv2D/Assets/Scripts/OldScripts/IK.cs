using UnityEngine;
using System.Collections;

public class IK : MonoBehaviour {

    Animator anim;

    public float ikWeight = 1;

    public Transform leftIKTarget;
    public Transform rightIKTarget;

    public Transform hintLeft;
    public Transform hintRight;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void OnAnimatorIK()
    {
        //IK position
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftIKTarget.position);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightIKTarget.position);

        //IK weight
        anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, ikWeight);
        anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, ikWeight);

        anim.SetIKHintPosition(AvatarIKHint.LeftElbow, hintLeft.position);
        anim.SetIKHintPosition(AvatarIKHint.RightElbow, hintRight.position);

        //IK rotation
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);

        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftIKTarget.rotation);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightIKTarget.rotation);
    }
}
