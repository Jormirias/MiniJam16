using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectWall : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsWall;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_CornerCheck;
    [SerializeField] private CharController CharControl; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_CornerCheck.position, 0.2f, m_WhatIsWall);

        for (int i = 0; i < colliders.Length; i++)
        {

            if (colliders[i].gameObject != gameObject)
            {
                makeGroundCorner();
 
            }
        }

        Collider2D[] collidersGround = Physics2D.OverlapCircleAll(m_CornerCheck.position, 0.1f, m_WhatIsGround);

        for (int i = 0; i < collidersGround.Length; i++)
        {

            if (collidersGround[i].gameObject != gameObject)
            {
                makeWallCorner();
   
            }
        }
    }

    void makeGroundCorner()
    {
        bool flipDirection = CharControl.CheckFlipSide();
        int flipMultiplier = 1;
        
        if (!flipDirection)
            { flipMultiplier *= -1; }

        string groundSide = CharControl.CheckGroundSide();
        if (groundSide == "ground" && flipDirection)
        {
            CharControl.ChangeGroundSide("right");
            transform.position = transform.position + new Vector3(0, 1, 0);
            Debug.Log(groundSide);
        }
         else if (groundSide == "ground" && !flipDirection)
        {
            transform.position = transform.position + new Vector3(0, 1, 0);
            CharControl.ChangeGroundSide("left");
        }
        else if (groundSide == "ceiling" && !flipDirection)
        {
            transform.position = transform.position + new Vector3(0, -1, 0);
            CharControl.ChangeGroundSide("right");
        }
        else if (groundSide == "ceiling" && flipDirection)
        {
            transform.position = transform.position + new Vector3(0, -1, 0);
            CharControl.ChangeGroundSide("left");
        }

        transform.Rotate(0.0f, 0.0f, 90.0f * flipMultiplier, Space.World);
    }

    void makeWallCorner()
    {
        bool flipDirection = CharControl.CheckFlipSide();
        int flipMultiplier = 1;

        if (!flipDirection)
        { flipMultiplier *= -1; }

        string groundSide = CharControl.CheckGroundSide();
        if (groundSide == "right" && flipDirection)
        {
            CharControl.ChangeGroundSide("ceiling");
            transform.position = transform.position + new Vector3(-1, 0, 0);
        }
        else if (groundSide == "right" && !flipDirection)
        { 
            CharControl.ChangeGroundSide("ground");
            transform.position = transform.position + new Vector3(-1, 0, 0);
        }
        else if (groundSide == "left" && flipDirection)
        { 
            CharControl.ChangeGroundSide("ground");
            transform.position = transform.position + new Vector3(1, 0, 0);
        }
        else if (groundSide == "left" && !flipDirection)
        { 
            CharControl.ChangeGroundSide("ceiling");
            transform.position = transform.position + new Vector3(1, 0, 0);
        }

        transform.Rotate(0.0f, 0.0f, 90.0f * flipMultiplier, Space.World);
    }
}
