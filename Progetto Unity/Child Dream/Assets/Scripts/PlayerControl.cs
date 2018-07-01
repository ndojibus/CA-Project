using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public LayerMask groundLayer;
    public float speedMultiplier = 1f;
    public float jumpPower = 1f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundCheckRadius = 0.5f;

    Animator childAnimator;
    Rigidbody childBody;

    float axisValue;
    float currentSpeed;
    

    bool facingRight = true;
    bool grounded = false;
    bool hasToJump = false;

    // DEBUG CANCELLARE
    bool oldGrounded = false;

    private void Awake()
    {
        childAnimator = GetComponent<Animator>();
        if (childAnimator == null)
            Debug.LogError(this.name + ": Animator don't found!");

        childBody = GetComponent<Rigidbody>();
        if (childAnimator == null)
            Debug.LogError(this.name + ": Rigidbody don't found!");
    }

    private void Start()
    {
        StartCoroutine("RaycastCoroutine", 0.25f);
    }

    // Update is called once per frame
    private void Update()
    {
        // Input management
        if (Input.GetButtonDown("Jump") && grounded)
            hasToJump = true;

        axisValue = Input.GetAxis("Horizontal");

        if (Input.GetButton("Walk"))
            axisValue *= 0.5f;
    }


    void FixedUpdate () {
        //better jump
        if (childBody.velocity.y < 0)
            childBody.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        else if (childBody.velocity.y > 0 && Mathf.Abs(Input.GetAxis("Jump")) > 0.05)
            childBody.velocity += Vector3.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime;

        // Jump
        if (grounded && hasToJump)
        {
            grounded = false;
            childBody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            //childBody.velocity = new Vector3(childBody.velocity.y, jumpPower, 0);
            hasToJump = false;
        }

        childAnimator.SetBool("Grounded", grounded);

        // Set the correct idle/walk/run/jump animation
        childAnimator.SetFloat("Speed", Mathf.Abs(axisValue));
        if (!grounded || Mathf.Abs(childBody.velocity.y) > 0.5f)
        {

            childAnimator.SetFloat("VerticalSpeed", childBody.velocity.y);
            float newSpeed = 0.1f * axisValue + childBody.velocity.x;

            Debug.Log(childBody.velocity.x + " actual speed at time " + Time.time);
            Debug.Log(newSpeed + " new speed at time " + Time.time);
            if ((facingRight && newSpeed > speedMultiplier) || ((!facingRight && newSpeed < -speedMultiplier)))
            {
                childBody.velocity = new Vector3(Mathf.Sign(childBody.velocity.x) * speedMultiplier, childBody.velocity.y, 0);
                Debug.Log("HIGHSPEED");
            }
            else if ((facingRight && newSpeed < 0) || (!facingRight && newSpeed > 0))
            {
                childBody.velocity = new Vector3(0, childBody.velocity.y, 0);
                Debug.Log("LOWSPEED");
            }
            else
            {
                childBody.AddForce(new Vector3(0.1f * axisValue, 0, 0), ForceMode.VelocityChange);
                Debug.Log("SETSPEED");
            }
        }
        else
        {
            childAnimator.SetFloat("VerticalSpeed", -5);

            // Set current character velocity
            currentSpeed = axisValue * speedMultiplier;
            childBody.velocity = new Vector3(currentSpeed, childBody.velocity.y, 0);

            // Flip the child if is facing in the opposite direction
            if ((facingRight && axisValue < 0) || (!facingRight && axisValue > 0))
            {
                Flip();
                facingRight = !facingRight;
            }
        }

	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, -Vector3.up * groundCheckRadius);
    }

    void Flip() {
        if (facingRight)
            this.transform.Rotate(0, 180, 0);
        else
            this.transform.Rotate(0, -180, 0);
    }

    IEnumerator RaycastCoroutine(float timeGrounded)
    {
        while (true)
        {
            if (Physics.Raycast(this.transform.position, -Vector3.up, groundCheckRadius, groundLayer))
            {
                grounded = true;
                yield return new WaitForSeconds(timeGrounded);
            }
            else
                grounded = false;

            yield return null;
        }
    }
}
