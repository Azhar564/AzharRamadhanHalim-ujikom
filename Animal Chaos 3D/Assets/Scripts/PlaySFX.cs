using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip sfx;
    public void PlaySFXAudio()
    {
        AudioSource.PlayClipAtPoint(sfx, Vector3.zero);
    }
}
