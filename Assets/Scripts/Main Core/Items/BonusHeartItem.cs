using UnityEngine;

public class BonusHeartItem : Items
{
    [SerializeField]
    private AudioSource _soundItem;
    [SerializeField]
    private AudioClip _onPickUpItem;
    [SerializeField]
    private ShowUp _showUP;
    private Rigidbody _rigidbodyBonusHeartItem;

    void Start()
    {
        if (_isGameActive)
        {
            _soundItem.PlayOneShot(_onPickUpItem, 1);
            _rigidbodyBonusHeartItem = GetComponent<Rigidbody>();
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
        _rigidbodyBonusHeartItem.AddTorque(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10), ForceMode.Impulse);
    }

    protected override void RandomTorquees()
    {
        _rigidbodyBonusHeartItem.AddForce(Vector3.up * Random.Range(16, 20), ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mouse"))
        {
            Instantiate(_effectItemRed, transform.position, Quaternion.identity);
            ShowUpEffective();
            _health++;
            _isChange = true;
            Destroy(gameObject);
        }
    }

    private void ShowUpEffective()
    {
        var showUpPos = new Vector3(transform.position.x, transform.position.y + 0.3f, 0);
        var showUp = Instantiate(_showUP, showUpPos, Quaternion.identity);
        showUp.showUpScore = "+ 1-life";
    }
}
