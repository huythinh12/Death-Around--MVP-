using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField]
    AudioSource _soundEffect;
    [SerializeField]
    AudioClip _onPickUpSound;
    // Start is called before the first frame update
    void Start()
    {
        _soundEffect.PlayOneShot(_onPickUpSound, 1);
        Destroy(gameObject, 2);
    }
}
