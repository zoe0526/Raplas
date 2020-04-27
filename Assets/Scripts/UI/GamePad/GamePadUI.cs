using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePadUI : MonoBehaviour
{ 
    public void AttackButtonClick()
    {
        AnimationController.Instance.AttackAnim(ePlayerAnimation.IsAttack.ToString());
    }
    public void SkillButtonClick(int id)
    { 
        AnimationController.Instance.mAnimator.SetTrigger(SkillButton.Instance.mSkillDataArr[id].Anim); 
        AnimationController.Instance.ActivateSkill(id);
    } 
}
