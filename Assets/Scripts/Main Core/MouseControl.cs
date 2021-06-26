using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class MouseControl : GameManger
{
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider collider;
    private bool swiping = false;
    private RaycastHit hit;//test with raycast
    [SerializeField]
    private AudioSource mouseSound;
    [SerializeField]
    private AudioClip onPickUpMouse;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        collider = GetComponent<BoxCollider>();
        trail.enabled = false;
        collider.enabled = false;
    }
    private void UpdateMousePosition()
    {
        //convert mouse postition into world point 
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f));
        //z = 9 because camera has the z position of -10 and background positio = 0
        transform.position = mousePos;
    }

    private void UpdateComponent()
    {
        //check if swipping trail and collider enable
        trail.enabled = swiping;
        collider.enabled = swiping;
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
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
            swiping = true;
            UpdateComponent();
            mouseSound.PlayOneShot(onPickUpMouse, 1);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //if mouse release turnof trail and collider and no update mouse position
            swiping = false;
            UpdateComponent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChange = true;
            health = 0;
            GameFinished();
        }
    }
}
