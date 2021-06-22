using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class MouseControl : MonoBehaviour
{
    GameManger gameManager;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider collider;
    private bool swiping = false;

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
        //assign object position to mouse position
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
        //if click mouse then update mouse position
        if (swiping)
        {
            UpdateMousePosition();
        }

        if (Input.GetMouseButtonDown(0))
        {
            //when mouse down turn on trail and collider along with update mouse position
            swiping = true;
            UpdateComponent();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //if mouse release turnof trail and collider and no update mouse position
            swiping = false;
            UpdateComponent();
        }

    }
}
