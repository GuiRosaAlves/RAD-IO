using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplificador : Repetidor
{
    public float adjust;
    protected override void AddParticle(GameObject coll)
    {
        base.AddParticle(coll);
        particuleReference.GetComponent<ParticleSystem>().startLifetime = coll.GetComponent<ParticleSystem>().startLifetime * adjust;
    }
}
