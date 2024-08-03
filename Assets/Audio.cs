using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is 
    [SerializeField] AudioClip smackSound;
    [SerializeField] AudioClip coinSound;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySmackSound()
    {
        audioSource.clip = smackSound;
        audioSource.Play();
    }
    public void PlayCoinSound()
    {
        audioSource.clip = coinSound;
        audioSource.Play();
    }
}
