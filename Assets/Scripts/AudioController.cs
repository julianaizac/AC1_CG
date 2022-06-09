using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSourceMusicaDeFundo;
    public AudioSource audioSourceMusicaDeExplosao;
    public AudioClip[] musicasDeFundo;
    

    // Start is called before the first frame update
    void Start()
    {
        AudioClip musicaDeFundo = musicasDeFundo[0];
        audioSourceMusicaDeFundo.clip = musicaDeFundo;
        audioSourceMusicaDeFundo.loop = true;
        audioSourceMusicaDeFundo.Play();
    }

    public void ToqueAudioCollision(AudioClip clip)
    {
        audioSourceMusicaDeExplosao.clip = clip;
        audioSourceMusicaDeExplosao.Play();
    }

}
