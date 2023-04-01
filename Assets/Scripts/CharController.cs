using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private LayerMask m_WhatIsWall;
    // [SerializeField] private Transform m_CeilingCheck;

    private string currentGround = "ground";
    private float oGravity;
    private float k_GroundedRadius = .3f;
    private bool m_Grounded;
    private bool m_Walled;
    private Rigidbody2D m_Rigidbody;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;

    enum GravityDirection { Down, Left, Up, Right };
    GravityDirection m_GravityDirection;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_GravityDirection = GravityDirection.Down;
        oGravity = m_Rigidbody.gravityScale;

        //gameObject.GetComponent<Transform>().position = new Vector3(0, 0, 0);
        //use scene parameters?

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void FixedUpdate()
    {     
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        m_Walled = false;
        m_Rigidbody.gravityScale = oGravity;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

         for (int i = 0; i < colliders.Length; i++)
              {

                  if (colliders[i].gameObject != gameObject)
                  {
                      m_Grounded = true;
                      if (!wasGrounded && m_Rigidbody.velocity.y < -0.001f)
                      {
                          OnLandEvent.Invoke();
                      }
                  }
              }

        Collider2D[] colliders_wall = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsWall);

        for (int i = 0; i < colliders_wall.Length; i++)
        {

            if (colliders_wall[i].gameObject != gameObject)
            {
                m_Walled = true;
                m_Rigidbody.gravityScale = 0.0f;
            }
        }

        switch (m_GravityDirection)
        {
            case GravityDirection.Down:
                //Change the gravity to be in a downward direction (default)
                Physics2D.gravity = new Vector2(0, -9.8f);
                //Press the enter key to switch to the left direction
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    m_GravityDirection = GravityDirection.Left;
                    Debug.Log("Left");
                }
                break;

            case GravityDirection.Left:
                //Change the gravity to go to the left
                Physics2D.gravity = new Vector2(-9.8f, 0);
                //Press the enter key to change the direction of gravity
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    m_GravityDirection = GravityDirection.Up;
                    Debug.Log("Up");
                }
                break;

            case GravityDirection.Up:
                //Change the gravity to be in a upward direction
                Physics2D.gravity = new Vector2(0, 9.8f);
                //Press the enter key to change the direction
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    m_GravityDirection = GravityDirection.Right;
                    Debug.Log("Right");
                }
                break;

            case GravityDirection.Right:
                //Change the gravity to go in the right direction
                Physics2D.gravity = new Vector2(9.8f, 0);
                //Press the enter key to change the direction
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    m_GravityDirection = GravityDirection.Down;
                    Debug.Log("Down");
                }

                break;
        }
    }

    public void Move(float move, bool jump)
    {
        Debug.Log(jump);
        if (m_Grounded || m_AirControl)
        {            
            Vector3 targetVelocity = new Vector3(move * 10f, m_Rigidbody.velocity.y, 0);

            m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }

            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }

            if (m_Grounded && jump)
            {
                m_Grounded = false;
                m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce));
            }

        }
    }

    public void VerticalMove(float move)
    {
        if (m_Walled)
        {

            Vector3 targetVelocity = new Vector3(0, move * 10f, 0);
            m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        if (currentGround == "left" && ((move < 0 && !m_FacingRight) || (move > 0 && m_FacingRight)))
        {
            Debug.Log(currentGround);
            Flip();
        }

        else if (currentGround == "right" && ((move < 0 && m_FacingRight) || (move > 0 && !m_FacingRight)))
        {
            Flip();
        }
    }

        private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public bool CheckFlipSide()
    {
        return m_FacingRight;
    }

    public string CheckGroundSide()
    {
        return currentGround;
    }

    public void ChangeGroundSide(string groundType)
    {
        currentGround = groundType;
    }

}
