using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplificador : Repetidor
{
    public override void AddParticle(GameObject coll)
    {
        if (gameObject.tag != "Amplificador")
        {
            base.AddParticle(coll);
            reference.GetComponent<ParticleSystem>().startLifetime = coll.GetComponent<ParticleSystem>().startLifetime * 2;
        }
    }
}
