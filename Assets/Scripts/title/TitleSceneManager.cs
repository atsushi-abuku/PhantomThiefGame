using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] AudioSource startButtonAudio;
    TitleInput titleInput;
    int selectId;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectId = 0;
        titleInput = new TitleInput();
        startButton.onClick.AddListener(StartGame);
        startButtonAudio = startButton.GetComponent<AudioSource>();

        titleInput.select.decide.performed += ctx =>
        {
            Decide();
        };

        titleInput.select.move_left.performed += ctx =>
        {
            selectId--;
        };

        titleInput.select.move_left.performed += ctx =>
        {
            selectId++;
        };

        titleInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Decide()
    {
        StartGame();
    }

    public void StartGame()
    {
        startButtonAudio.Play();
        startButton.enabled = false;
        titleInput.Disable();
        SceneManager.LoadScene("ActionScene");
    }
}
