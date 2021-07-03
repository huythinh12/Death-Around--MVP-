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
    //private Touch _touch;
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
        //convert mouse position into world point 
        _mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f));
        //z = 9 because camera has the z position of -10 and background position = 0
        transform.position = _mousePos;
    }

    private void UpdateComponent()
    {
        //check if swiping trail and collide enable
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
        UpdateMousePosition();
        UpdateTouchSwipe();

    }

    private void UpdateTouchSwipe()
    {

        //if (HasTouchInput())
        //{
        //    if (_touch.phase == TouchPhase.Began)
        //        _mouseSound.PlayOneShot(_onPickUpMouse, 1);
        //    if (_touch.phase == TouchPhase.Moved)
        //    {
        //        _swiping = true;
        //        UpdateComponent();

        //    }
        //    else if (_touch.phase == TouchPhase.Ended)
        //    {
        //        _swiping = false;
        //        UpdateComponent();
        //    }
        //}
         if (Input.GetMouseButtonDown(0))
        {

            //_touch.phase == TouchPhase.Moved
            //when mouse down turn on trail and collide along with update mouse position
            _swiping = true;
            UpdateComponent();
            _mouseSound.PlayOneShot(_onPickUpMouse, 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //if mouse release turnoff trail and collide and no update mouse position
            _swiping = false;
            UpdateComponent();
        }

    }
    //private bool HasTouchInput()
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        _touch = Input.GetTouch(0);
    //        return true;
    //    }
    //    return false;
    //}
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
