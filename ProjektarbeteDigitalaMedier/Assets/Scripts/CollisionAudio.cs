using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;


private void OnTriggerEnter(Collider collision){
    audioSource.PlayOneShot(audioClip);
    }
}


