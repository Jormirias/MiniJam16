using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    public CharController controller;
    public Animator animator;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool IsJumping = false;

   // public AudioClip[] _clips;
   // private float _footstepCooldown = 0;

  //  private AudioSource footsteps;

    private void Awake()
    {
        //footsteps = GetComponent<AudioSource>();
    }

    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * _moveSpeed;

        if ((verticalMove != 0 || horizontalMove != 0) && !IsJumping)
        {
            animator.SetBool("Walking", true);
        } else { animator.SetBool("Walking", false); }
                

        if (Input.GetButtonDown("Jump") && Time.timeScale > 0)
        {

            jump = true;
            IsJumping = true;
        }
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            animator.SetTrigger("Jumps");

        }

        /*  if (horizontalMove != 0 && !IsJumping)
          {
              _footstepCooldown += Time.deltaTime;

              if (_footstepCooldown >= 0.5f || !footsteps.isPlaying)
              {
                  int rng = Random.Range(0, _clips.Length - 1);
                  footsteps.PlayOneShot(_clips[rng]);
                  _footstepCooldown = 0;
              }
          }
          else
          {
              _footstepCooldown = 0;
              footsteps.Stop();
          }*/

        if (IsJumping)
        {
            animator.SetBool("IsGrounded", false);
        } else { animator.SetBool("IsGrounded", true); }
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        controller.VerticalMove(verticalMove * Time.fixedDeltaTime);
        jump = false;
    }

    public void OnLanding()
    {

        IsJumping = false;
    }
}
