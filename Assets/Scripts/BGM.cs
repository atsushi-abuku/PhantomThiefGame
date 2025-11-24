using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip treasureClip;
    AudioClip alertClip;
    AudioClip gameclearClip;
    AudioClip gameoverClip;

    AudioClip titleClip;
    AudioClip gameClip;
    AudioClip escapeClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        treasureClip = Resources.Load<AudioClip>("Audio/treasure");
        alertClip = Resources.Load<AudioClip>("Audio/alert");
        gameclearClip = Resources.Load<AudioClip>("Audio/gameclear");
        gameoverClip = Resources.Load<AudioClip>("Audio/gameover");

        titleClip = Resources.Load<AudioClip>("Audio/FlameKeeper_title");
        gameClip = Resources.Load<AudioClip>("Audio/Prometheus-Game ver. - 2025_10_19 22.24");
        escapeClip = Resources.Load<AudioClip>("Audio/FlameKeeper_escape");
    }

    private void Start()
    {
        PlayGame();
    }

    //ƒ‹[ƒv
    void PlayBGM(AudioClip clip)
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.clip = clip;

        if (clip == gameclearClip || clip == gameoverClip)
            audioSource.loop = false;
        else
            audioSource.loop = true;

        audioSource.Play();
    }
    
    public IEnumerator PlayTreasureThenAlert(System.Action onFinished, float delay = 0.5f)
    {
        audioSource.clip = treasureClip;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(treasureClip.length  + delay);

        audioSource.clip = alertClip;
        audioSource.loop = false;
        audioSource.Play();
        yield return new WaitForSeconds(alertClip.length);

        onFinished?.Invoke();
    }

    public void PlayTitle()
    {
        audioSource.volume = 0.6f;
        PlayBGM(titleClip);
    }
    public void PlayGame()
    {
        audioSource.volume = 1.0f;
        PlayBGM(gameClip);
    }
    public void PlayEscape()
    {
        audioSource.volume = 0.2f;
        PlayBGM(escapeClip);
    }
    public void PlayGameclear() => PlayBGM(gameclearClip);
    public void PlayGameover() => PlayBGM(gameoverClip);
}