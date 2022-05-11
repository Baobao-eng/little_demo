using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float jumpHeight;

    [Header("Gravity")]
    [SerializeField] float groundCheckDis;
    [SerializeField] float gravity;
    [SerializeField] bool isGround;
    [SerializeField] LayerMask groundMask;
   
    private Vector3 Direction;
    private Vector3 Velocity;
    private CharacterController playerControl;
    private Animator playerAnimation;

    // Start is called before the first frame update
    void Awake()
    {

        playerControl = GetComponent<CharacterController>();
        playerAnimation = GetComponentInChildren<Animator>();
     
    }

    // Update is called once per frame

    private void LateUpdate()
    {
        PlayerMovement();
        Gravity();
    }

   
    void PlayerMovement()
    {
        isGround = Physics.CheckSphere(transform.position, groundCheckDis, groundMask);

        float move = Input.GetAxis("Vertical");
        Direction = new Vector3(0, 0, move);
        
        Direction = transform.TransformDirection(Direction);

      
        if (isGround)
        {
            if (Direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
           
            }
            else if(Direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
               
            }
            else if (Direction == Vector3.zero)
            {
                Idle();
       
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
         

        }
      
        Direction *= speed;
        playerControl.Move(Velocity * Time.deltaTime);
        playerControl.Move(Direction * Time.deltaTime);
     
    }
        

    void Gravity()
    {
        if (isGround && Velocity.y < 0)
        {
            Velocity.y = -2;

        }

        Velocity.y += gravity * Time.deltaTime;
       
    }
    void Walk()
    {
        speed = walkSpeed;
        playerAnimation.SetFloat("Animation", 0.5f,0.1f, Time.deltaTime);
    }

    void Run()
    {
        speed = runSpeed;
        playerAnimation.SetFloat("Animation", 1.0f, 0.1f, Time.deltaTime);
    }

    void Idle()
    {
        speed = 0;
        playerAnimation.SetFloat("Animation", 0f, 0.1f, Time.deltaTime);
    }
    
    void Jump()
    {
        Velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        
        playerAnimation.SetTrigger("Jump");
        

    }

    
}
