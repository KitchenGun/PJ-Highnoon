using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oculus_Haptic : MonoBehaviour
{
    public bool useHaptics = false;
    public bool useSound = true;

    public OVRInput.Controller controller;

    private AudioSource cachedSource;
    private OVRHapticsClip hapticsClip;
    private float hapticsClipLength;
    private float hapticsTimeout;

    void OnTriggerEnter(Collider c)
    {
        if (useHaptics)
            PlayHaptics(c);

        if (useSound)
            PlaySound(c);
    }

    void OnCollisionEnter(Collision c)
    {
        if (useHaptics)
            PlayHaptics(c.collider);

        if (useSound)
            PlaySound(c.collider);
    }

    void PlayHaptics(Collider c)
    {
        var source = c.GetComponent<AudioSource>();
        if (source == null)
            return;

        if (source != cachedSource)
        {
            hapticsClip = new OVRHapticsClip(source.clip);
            hapticsClipLength = source.clip.length;
            cachedSource = source;
        }

        if (Time.time < hapticsTimeout)
            return;

        hapticsTimeout = Time.time + hapticsClipLength;

        if (controller == OVRInput.Controller.LTouch)
            OVRHaptics.LeftChannel.Preempt(hapticsClip);
        else
            OVRHaptics.RightChannel.Preempt(hapticsClip);
    }

    void PlaySound(Collider c)
    {
        var source = c.GetComponent<AudioSource>();
        if (source && !source.isPlaying)
            source.PlayDelayed(0.1f);
    }
}