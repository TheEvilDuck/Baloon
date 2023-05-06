using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public void BaloonMotionEnd()
    {
        gameObject.SetActive(false);
    }
    public void ExplosionParticlesEnded()
    {
        gameObject.SetActive(false);
    }
}
