using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField]
    AudioSource soundEffect;
    [SerializeField]
    AudioClip onPickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        soundEffect.PlayOneShot(onPickUpSound, 1);
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
