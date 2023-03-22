using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{ 
    [SerializeField] private AudioSource introSource;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip loop;
    

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (introSource.isPlaying || loopSource.isPlaying) return;
        StartCoroutine(PlayLooping());
    }

    private IEnumerator PlayLooping()
    {
<<<<<<< HEAD
        introSource.Stop();
        loopSource.Stop();
=======
>>>>>>> parent of 35a53fd (Merge branch 'main' of github.com:SFMBa01029956/CBJAM23)
        introSource.Play(); 
        loopSource.PlayScheduled(AudioSettings.dspTime + intro.length);
        loopSource.PlayScheduled(AudioSettings.dspTime + loop.length);
        yield return null;
    }

}
