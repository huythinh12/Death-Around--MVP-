using UnityEngine;

public class Items : GameManger
{
    [SerializeField]
    private GameObject _effectItemBlack, _effectItemOrange, _effectItemRed;
    [SerializeField]
    private AudioSource _soundItem;
    [SerializeField]
    private AudioClip _onPickUpItem;
    private Rigidbody _itemRb;
    public int _pointValue;

    // Start is called before the first frame update
    void Start()
    {
        if (_isGameActive)
        {
            _soundItem.PlayOneShot(_onPickUpItem, 1);
            _itemRb = GetComponent<Rigidbody>();
            RandomTorque();
            RandomForce();
            Vector3 RandomPos = new Vector3(Random.Range(-11, 11), -8, -1f);
            transform.position = RandomPos;
        }
    }
    private void Update()
    {
        //check condition for destroy item
        if (!_isGameActive || transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //check condition when mouse hit item 
        if (other.CompareTag("Mouse"))
        {
            if (transform.name.StartsWith("Lucky"))
            {
                Instantiate(_effectItemOrange, transform.position, Quaternion.identity);
                _isInvisible = true;
            }
            else if (transform.name.StartsWith("Bad"))
            {
                Instantiate(_effectItemBlack, transform.position, Quaternion.identity);
                if (_score > 0)
                {
                    _score--;
                    _isChange = true;
                }
            }
            else if (transform.name.StartsWith("Explosion"))
            {
                Instantiate(_effectItemRed, transform.position, Quaternion.identity);
                _isExplosion = true;
            }
            Destroy(gameObject);
        }
    }

    private void RandomTorque()
    {
        _itemRb.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
    }

    private void RandomForce()
    {
        _itemRb.AddForce(Vector3.up * Random.Range(12, 16), ForceMode.Impulse);
    }
}
