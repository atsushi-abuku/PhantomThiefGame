using UnityEngine;

public class ThiefAudio : MonoBehaviour
{
    AudioSource audioSource;

    AudioClip comfirmClip;
    AudioClip crouchClip;
    AudioClip dashClip;
    AudioClip jumpClip;
    AudioClip selectClip;
    AudioClip slidingClip;
    AudioClip transformClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        comfirmClip = Resources.Load<AudioClip>("Audio/comfirm");
        crouchClip = Resources.Load<AudioClip>("Audio/crouch");
        dashClip = Resources.Load<AudioClip>("Audio/dash");
        jumpClip = Resources.Load<AudioClip>("Audio/jump");
        selectClip = Resources.Load<AudioClip>("Audio/select");
        slidingClip = Resources.Load<AudioClip>("Audio/sliding");
        transformClip = Resources.Load<AudioClip>("Audio/transform");

    }

    void Play(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayComfirm() => Play(comfirmClip);//
    public void PlayCrouch() => Play(crouchClip);//
    public void PlayDash() => Play(dashClip);//
    public void PlayJump() => Play(jumpClip);//
    public void PlaySelect() => Play(selectClip);//
    public void PlaySliding() => Play(slidingClip);//
    public void PlayTransform() => Play(transformClip);//
}