using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Ctrl : MonoBehaviour
{

    public ParticleSystem G_MF;

	void Play()
    {
        G_MF.Play();
    }
}
