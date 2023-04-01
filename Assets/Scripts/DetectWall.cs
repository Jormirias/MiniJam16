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
        transform.position = transform.position + new Vector3(0, 1, 0);
        bool flipDirection = CharControl.CheckFlipSide();
        int flipMultiplier = 1;

        if (!flipDirection)
            { flipMultiplier *= -1; }

        string groundSide = CharControl.CheckGroundSide();
        if (groundSide == "ground" && flipDirection)
            CharControl.ChangeGroundSide("right");
         else if (groundSide == "ground" && !flipDirection)
            CharControl.ChangeGroundSide("left");


        transform.Rotate(0.0f, 0.0f, 90.0f * flipMultiplier, Space.World);
    }

    void makeWallCorner()
    {
        transform.position = transform.position + new Vector3(0, 1, 0);
        bool flipDirection = CharControl.CheckFlipSide();
        int flipMultiplier = 1;

        if (!flipDirection)
        { flipMultiplier *= -1; }

        string groundSide = CharControl.CheckGroundSide();
        if (groundSide == "right" && flipDirection)
            CharControl.ChangeGroundSide("ceiling");
        else if (groundSide == "right" && !flipDirection)
            CharControl.ChangeGroundSide("ground");
        else if (groundSide == "left" && flipDirection)
            CharControl.ChangeGroundSide("ground");
        else if (groundSide == "left" && !flipDirection)
            CharControl.ChangeGroundSide("ceiling");


        transform.Rotate(0.0f, 0.0f, 90.0f * flipMultiplier, Space.World);
    }
}
