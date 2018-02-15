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
        //Gambiarra pra achar o cilindro filho, pra caso for uma interferencia. Desculpa, Abu, se tiver deixando seu código feio xD
        if (coll.tag == "Interferencia")
        {
            transform.GetChild(transform.childCount - 1).transform.GetChild(0).GetComponent<CapsuleCollider>().radius *= adjust;
        }
    }
}
