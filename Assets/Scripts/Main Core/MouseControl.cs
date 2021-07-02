using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class MouseControl : GameManger
{
    [SerializeField]
    private AudioSource _mouseSound;
    [SerializeField]
    private AudioClip _onPickUpMouse;
    private Vector3 _mousePos;
    private TrailRenderer _trail;
    private BoxCollider _collider;
    private RaycastHit hit;//test with raycast
    private bool _swiping = false;
 

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _trail = GetComponent<TrailRenderer>();
        _collider = GetComponent<BoxCollider>();
        _trail.enabled = false;
        _collider.enabled = false;
    }
    private void UpdateMousePosition()
    {
        //convert mouse postition into world point 
        _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f));
        //z = 9 because camera has the z position of -10 and background positio = 0
        transform.position = _mousePos;
    }

    private void UpdateComponent()
    {
        //check if swipping trail and collider enable
        _trail.enabled = _swiping;
        _collider.enabled = _swiping;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_health <= 0)
        {
            return;
        }

        //---Test with raycast but still fail when it goes too fast----
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    if(hit.collider.tag == "Player")
        //    {
        //        print("chay khi va cham player ");
        //    }
        //}

        UpdateMousePosition();

        if (Input.GetMouseButtonDown(0))
        {
            //when mouse down turn on trail and collider along with update mouse position
            _swiping = true;
            UpdateComponent();
            _mouseSound.PlayOneShot(_onPickUpMouse, 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //if mouse release turnof trail and collider and no update mouse position
            _swiping = false;
            UpdateComponent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isChange = true;
            _health = 0;
            GameFinished();
        }
    }
}
