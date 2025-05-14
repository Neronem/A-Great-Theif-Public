using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticleControl : MonoBehaviour
{
    public ParticleSystem dustParticle; // 더스트 파티클 시스템

    public void ReplaceParticle(ParticleSystem newDustParticle)
    {
        if (newDustParticle != null)
            Destroy(dustParticle.gameObject); // 기존 파티클 시스템 삭제

        dustParticle = Instantiate(newDustParticle, transform);

    }

    public void CreateDust()
    {
        if (dustParticle == null) return;

        dustParticle.Stop();
        dustParticle.Play();
    }
    
}