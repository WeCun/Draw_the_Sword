using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingAnimationTrigger : MonoBehaviour
{
    private PigKing pigking => GetComponentInParent<PigKing>();

    //死亡后Destory()
    public void DestroySelf()
    {
        Destroy(pigking.gameObject);
    }
}
