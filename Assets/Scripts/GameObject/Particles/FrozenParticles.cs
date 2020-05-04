using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenParticles : Particles
{ 

    public override void Start()
    {
        //延迟5.1s之后消失
        Destroy(this.gameObject, 5.1f);
    }

}
