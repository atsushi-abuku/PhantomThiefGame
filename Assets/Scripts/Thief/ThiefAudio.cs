using UnityEngine;

public class ThiefAudio : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip alertClip;
    AudioClip comfirmClip;
    AudioClip crouchClip;
    AudioClip dashClip;
    AudioClip gameclearClip;
    AudioClip gameoverClip;
    AudioClip jumpClip;
    AudioClip selectClip;
    AudioClip slidingClip;
    AudioClip transformClip;
    AudioClip treasureClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        alertClip = Resources.Load<AudioClip>("Audio/alert");
        comfirmClip = Resources.Load<AudioClip>("Audio/comfirm");
        crouchClip = Resources.Load<AudioClip>("Audio/crouch");
        dashClip = Resources.Load<AudioClip>("Audio/dash");
        gameclearClip = Resources.Load<AudioClip>("Audio/gameclear");
        gameoverClip = Resources.Load<AudioClip>("Audio/gameover");
        jumpClip = Resources.Load<AudioClip>("Audio/jump");
        selectClip = Resources.Load<AudioClip>("Audio/select");
        slidingClip = Resources.Load<AudioClip>("Audio/sliding");
        transformClip = Resources.Load<AudioClip>("Audio/transform");
        treasureClip = Resources.Load<AudioClip>("Audio/treasure");
    }

    void Play(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayAlert() => Play(alertClip);
    public void PlayComfirm() => Play(comfirmClip);//
    public void PlayCrouch() => Play(crouchClip);//
    public void PlayDash() => Play(dashClip);//
    public void PlayGameclear() => Play(gameclearClip);//
    public void PlayGameover() => Play(gameoverClip);//
    public void PlayJump() => Play(jumpClip);//
    public void PlaySelect() => Play(selectClip);//
    public void PlaySliding() => Play(slidingClip);//
    public void PlayTransform() => Play(transformClip);//
    public void PlayTreasure() => Play(treasureClip);
}