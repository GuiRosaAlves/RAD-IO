using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : Repetidor
{
    public float adjust;
    protected override void AddParticle(GameObject coll)
    {
        base.AddParticle(coll);
        particuleReference.GetComponent<ParticleSystem>().startSpeed = coll.GetComponent<ParticleSystem>().startSpeed * adjust;
    }
}
