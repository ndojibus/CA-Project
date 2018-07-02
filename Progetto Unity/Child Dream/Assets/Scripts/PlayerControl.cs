using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public LayerMask groundLayer;               // Layer che indica il terreno su cui deve camminare il personaggio
    public float speedMultiplier = 1f;          // Massima velocità di camminata
    public float jumpPower = 1f;                // Potenza del salto 
    public float fallMultiplier = 2.5f;         // Maggiorazione di accelerazione durante la caduta
    public float highJumpMultiplier = 2f;       // Potenziamento del salto quando si tiene premuto il tasto
    public float groundCheckRadius = 0.5f;      // Distanza del raycast per controllare se si è a terra
    public float jumpAttenuationValue = 0.1f;   // Attenuazione della velocità laterale mentre si è in volo

    Animator childAnimator;
    Rigidbody childBody;

    float axisValue;
    float currentSpeed;
    
    bool facingRight = true;
    public bool FacingRight { set { facingRight = value; } get { return facingRight; } }
    bool controllable = true;
    public bool Controllable { set { controllable = value; } }

    bool grounded = false;
    bool hasToJump = false;

    // DEBUG CANCELLARE

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
        StartCoroutine("RaycastCoroutine", 0.1f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (controllable)
        {
            // Input management
            if (Input.GetButtonDown("Jump") && grounded && childBody.velocity.y < 0.05f)
                hasToJump = true;

            axisValue = Input.GetAxis("Horizontal");

            if (Input.GetButton("Walk"))
                axisValue *= 0.5f;
        }
        else
        {
            hasToJump = false;
            axisValue = 0f;
        }
    }


    void FixedUpdate () {
        //better jump
        if (childBody.velocity.y < 0)
            childBody.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
        else if (childBody.velocity.y > 0 && Mathf.Abs(Input.GetAxis("Jump")) > 0.05)
            childBody.velocity += Vector3.up * Physics.gravity.y * highJumpMultiplier * Time.deltaTime;

        // Jump
        if (grounded && hasToJump)
        {
            grounded = false;
            
            float newVelocity;
            if (Mathf.Abs(axisValue) < 0.05f)
                newVelocity = 0f;
            else if (Mathf.Abs(axisValue) < 0.6f)
                newVelocity = speedMultiplier / 2;
            else
                newVelocity = speedMultiplier;

            Debug.Log("Velocity on y before is " + childBody.velocity.y);
            Debug.Log("Velocity on x before is " + childBody.velocity.x);
            childBody.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            childBody.velocity -=  Vector3.right * (childBody.velocity.x - Mathf.Sign(childBody.velocity.x) * newVelocity);
            Debug.Log("new velocity is "+ newVelocity + " and x offset is" + (childBody.velocity.x - Mathf.Sign(childBody.velocity.x) * newVelocity));
            StartCoroutine("DebugCoroutine");
            hasToJump = false;
        }

        childAnimator.SetBool("Grounded", grounded);

        // Set the correct idle/walk/run/jump animation
        childAnimator.SetFloat("Speed", Mathf.Abs(axisValue));
        if (!grounded || Mathf.Abs(childBody.velocity.y) > 0.5f)
        {

            childAnimator.SetFloat("VerticalSpeed", childBody.velocity.y);
            float newSpeed = jumpAttenuationValue * axisValue + childBody.velocity.x;
            if ((facingRight && newSpeed > speedMultiplier) || ((!facingRight && newSpeed < -speedMultiplier)))
                childBody.velocity = new Vector3(Mathf.Sign(childBody.velocity.x) * speedMultiplier, childBody.velocity.y, 0);
            else if ((facingRight && newSpeed < 0) || (!facingRight && newSpeed > 0))
                childBody.velocity = new Vector3(0, childBody.velocity.y, 0);
            else
                childBody.AddForce(new Vector3(jumpAttenuationValue * axisValue, 0, 0), ForceMode.VelocityChange);
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
        Gizmos.DrawRay(this.transform.position + Vector3.up * 0.5f + Vector3.right * 0.3f, -Vector3.up * groundCheckRadius);
        Gizmos.DrawRay(this.transform.position + Vector3.up * 0.5f + Vector3.right * (-0.3f), -Vector3.up * groundCheckRadius);
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
            if (Physics.Raycast(this.transform.position + Vector3.up * 0.5f + Vector3.right * 0.3f, -Vector3.up, groundCheckRadius, groundLayer)
                || Physics.Raycast(this.transform.position + Vector3.up * 0.5f + Vector3.right * (-0.3f), -Vector3.up, groundCheckRadius, groundLayer)
                )
                grounded = true;
            else
                grounded = false;

            yield return new WaitForSeconds(timeGrounded);
        }
    }
    IEnumerator DebugCoroutine() {
        yield return null;
        Debug.Log("Velocity on y after is " + childBody.velocity.y);
        Debug.Log("Velocity on x after is " + childBody.velocity.x);
    }
}
