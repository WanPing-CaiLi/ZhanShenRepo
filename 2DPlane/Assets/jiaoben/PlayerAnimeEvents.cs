using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimeEvents : MonoBehaviour
{
    private BoFangQi player;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<BoFangQi>();
    }

    // Update is called once per frame
    private void AnimationTriggers()
    {
        player.AttackOver();
    }
}
