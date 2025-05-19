using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigKingAnimationTrigger : MonoBehaviour
{
    private PigKing pigking => GetComponentInParent<PigKing>();

    public void DestroySelf()
    {
        Destroy(pigking.gameObject);
    }
}
