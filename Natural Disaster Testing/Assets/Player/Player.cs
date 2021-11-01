using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public PlayerData data;
        Rigidbody rb;

        public Vector2 point;
        public Vector2 currentPoint;

        public Vector3 jumpColliderDimensions;
        public Vector3 jumpColliderPos;

        public PlayerState state;

        public bool canJump;

        public Vector3 gravity;
        float globalGravity = -9.81f;

        #region Parameters
        public float moveAcceleration => data.moveAcceleration;
        public float moveSpeedCap => data.moveSpeedCap;
        public float jumpForce => data.jumpForce;
        public float playerGravity => data.playerGravity;
        #endregion

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
            canJump = true;
            gravity = globalGravity * playerGravity * Vector3.up;

        }
        public void FixedUpdate()
        {
            rb.AddForce(gravity, ForceMode.Acceleration);
        }
        public enum PlayerState
        {
            Walking,
            Jumping,
            Floating,


        }
        
        void Start()
        {
            state = PlayerState.Walking;
            

        }


        void Update()
        {
            HandleInput();

        }
        
        private void OnDrawGizmos()
        {

            Gizmos.DrawWireCube(jumpColliderPos + transform.position, jumpColliderDimensions);
        }
        void CheckGround()
        {
            Collider[] col = Physics.OverlapBox(jumpColliderPos + transform.position, jumpColliderDimensions / 2, Quaternion.identity ); 
            foreach(Collider c in col)
            {
                if(c.gameObject.layer == 3)
                {
                    canJump = true;
                }
                
            }
        }


        

        void HandleInput()
        {
            
            

            CheckGround();
            
            Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
           
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {

                Jump();
            }
        }
        

        

        private Vector3 velocity = Vector3.zero;
        void Move(float x, float z)
        {
       
            Vector3 targetVelocity = new Vector3(moveSpeedCap * x, rb.velocity.y, moveSpeedCap * z);
            Quaternion rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);
            
            targetVelocity = rotation * targetVelocity;
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, moveAcceleration );




        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            
        }
        
        void Jump()
        {

            canJump = false;

            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpForce ));
            state = PlayerState.Jumping;
            
        }
    }
}

