using UnityEngine;
using System.Collections;

public class ElephantController : EnemyController
{
    public override void Start()
    {
        base.Start();
        beatCanMove = 2;
    }
}
