using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectType
{
    PlayerAttack,
    Damage,
    BossDamage,
    LevelUp 
}
public class EffectPool :GameObjectPool<Effect>
{
   

}