using UnityEngine;

public class ExplosionItem : Items
{
    [SerializeField]
    private AudioSource _soundItem;
    [SerializeField]
    private AudioClip _onPickUpItem;
    private Rigidbody _rigidbodyExplosionItem;

    // Start is called before the first frame update
    void Start()
    {
        if (_isGameActive)
        {
            _soundItem.PlayOneShot(_onPickUpItem, 1);
            _rigidbodyExplosionItem = GetComponent<Rigidbody>();
            RandomForcee();
            RandomTorquees();
            Vector3 RandomPos = RandomPosSpawn;
            transform.position = RandomPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChecktoDestroyItem();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            Instantiate(_effectItemRed, transform.position, Quaternion.identity);
            _isExplosion = true;
            Destroy(gameObject);
        }
    }
 
    protected override void ChecktoDestroyItem()
    {
        //check condition for destroy item
        if (!_isGameActive || transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }
    protected override void RandomForcee()
    {
        _rigidbodyExplosionItem.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
    }

    protected override void RandomTorquees()
    {
        _rigidbodyExplosionItem.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
    }
}
