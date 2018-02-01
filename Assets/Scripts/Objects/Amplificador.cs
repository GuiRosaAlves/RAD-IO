using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplificador : Repetidor
{
    public float adjust;
    protected override void AddParticle(GameObject coll)
    {
        base.AddParticle(coll);
#pragma warning disable CS0618 // Type or member is obsolete
        particuleReference.GetComponent<ParticleSystem>().startLifetime *= adjust;
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
