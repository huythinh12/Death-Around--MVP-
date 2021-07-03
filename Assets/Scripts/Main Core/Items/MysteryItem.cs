using UnityEngine;

public class MysteryItem : Items
{
    [SerializeField]
    private AudioSource _soundItem;
    [SerializeField]
    private AudioClip _onPickUpItem;
    private Rigidbody _rigidbodyMysteryItem;

    void Start()
    {
        if (_isGameActive)
        {
            _soundItem.PlayOneShot(_onPickUpItem, 1);
            _rigidbodyMysteryItem = GetComponent<Rigidbody>();
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
        _rigidbodyMysteryItem.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
    }

    protected override void RandomTorquees()
    {
        _rigidbodyMysteryItem.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            Instantiate(_effectItemBlack, transform.position, Quaternion.identity);
            RandomScore();
            _isChange = true;
            Destroy(gameObject);
        }
    }

    private void RandomScore()
    {
        if (Random.Range(0, 3) == 1)
            _score += 5;
        else
            _score--;
    }

    // Start is called before the first frame update

}
