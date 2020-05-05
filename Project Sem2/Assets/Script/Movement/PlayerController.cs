﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string EventGrimpe = "";
    [FMODUnity.EventRef]
    public string EventAccrocheEdge = "";
    [FMODUnity.EventRef]
    public string EventSaut = "";

    public float drag_grounded;
    public float drag_inair;

    public DetectObs detectVaultObject; //checks for vault object
    public DetectObs detectVaultObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to vault
    public DetectObs detectClimbObject; //checks for climb object
    public DetectObs detectClimbObstruction; //checks if theres somthing in front of the object e.g walls that will not allow the player to climb
    public DetectObs detectCorniche; //checks if theres somthing in front of the object e.g walls that will not allow the player to climb


    public DetectObs DetectWallL; //detects for a wall on the left
    public DetectObs DetectWallR; //detects for a wall on the right

    public Animator cameraAnimator;

    public float WallRunUpForce;
    public float WallRunUpForce_DecreaseRate;
    private float upforce;

    public float WallJumpUpVelocity;
    public float WallJumpForwardVelocity;
    public float drag_wallrun;
    public bool WallRunning;
    public bool WallrunningLeft;
    public bool WallrunningRight;
    private bool canwallrun; // ensure that player can only wallrun once before needing to hit the ground again, can be modified for double wallruns
    
    public bool IsParkour;
    private float t_parkour;
    private float chosenParkourMoveTime;

    [SerializeField] private Camera cam;

    private bool CanVault;
    public float VaultTime; //how long the vault takes
    public Transform VaultEndPoint;

    private bool CanClimb;
    public float ClimbTime; //how long the vault takes
    public Transform ClimbEndPoint;
    private bool canCorniche;
    private bool isOnCorniche;
    private bool timeWait;

    private float timer;
    [SerializeField] private float timeMaxWait;

    public int JumpForceInCorniche;

    private RigidbodyFirstPersonController rbfps;
    private Rigidbody rb;
    private Vector3 RecordedMoveToPosition; //the position of the vault end point in world space to move the player to
    private Vector3 RecordedStartPosition; // position of player right before vault
    // Start is called before the first frame update
    void Start()
    {
        rbfps = GetComponent<RigidbodyFirstPersonController>();
        rb = GetComponent<Rigidbody>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (rbfps.Grounded)
        {
            rb.drag = drag_grounded;
            canwallrun = true;
        }
        else
        {
            rb.drag = drag_inair;
        }
        if(WallRunning)
        {
            rb.drag = drag_wallrun;

        }
        //vault
        if (detectVaultObject.Obstruction && !detectVaultObstruction.Obstruction && !CanVault && !IsParkour && !WallRunning
            && (Input.GetKey(KeyCode.Space) || !rbfps.Grounded) && Input.GetAxisRaw("Vertical") > 0f)
        // if detects a vault object and there is no wall in front then player can pressing space or in air and pressing forward
        {
            CanVault = true;
        }

        if (CanVault)
        {
            CanVault = false; // so this is only called once
            rb.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = VaultEndPoint.position;
            RecordedStartPosition = transform.position;
            IsParkour = true;
            chosenParkourMoveTime = VaultTime;

            FMODUnity.RuntimeManager.PlayOneShot(EventGrimpe, transform.position);
            cameraAnimator.CrossFade("Vault",0.1f);
        }

        //climb
        if (detectClimbObject.Obstruction &&!detectClimbObstruction.Obstruction && !CanClimb && !IsParkour && !WallRunning
            && (Input.GetKey(KeyCode.Space) || !rbfps.Grounded) && Input.GetAxisRaw("Vertical") > 0f)
        {
            CanClimb = true;
        }

        if (CanClimb)
        {
            CanClimb = false; // so this is only called once
            rb.isKinematic = true; //ensure physics do not interrupt the vault
            RecordedMoveToPosition = ClimbEndPoint.position;
            RecordedStartPosition = transform.position;
            IsParkour = true;
            chosenParkourMoveTime = ClimbTime;

            FMODUnity.RuntimeManager.PlayOneShot(EventGrimpe, transform.position);
            cameraAnimator.CrossFade("Climb", 0.1f);

        }

        //corniche
        if(detectCorniche.Obstruction && isOnCorniche == false && !IsParkour && !rbfps.Grounded && timeWait == false)
        {
            canCorniche = true;
        }

        if (canCorniche)
        {
            FMODUnity.RuntimeManager.PlayOneShot(EventAccrocheEdge, transform.position);

            canCorniche = false;
            isOnCorniche = true;
            rb.isKinematic = true;
        }

        if (isOnCorniche)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.isKinematic = false;
                timeWait = true;
                isOnCorniche = false;

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                FMODUnity.RuntimeManager.PlayOneShot(EventSaut, transform.position);

                rb.isKinematic = false;
                timeWait = true;
                isOnCorniche = false;
                rb.AddForce(cam.gameObject.transform.forward * JumpForceInCorniche, ForceMode.Impulse);

            }
        }

        if (timeWait)
        {
            timer += Time.deltaTime;

            if(timer >= timeMaxWait)
            {
                timeWait = false;
                timer = 0;
            }
        }


        //Parkour movement
        if (IsParkour && t_parkour < 1f)
        {
            t_parkour += Time.deltaTime / chosenParkourMoveTime;
            transform.position = Vector3.Lerp(RecordedStartPosition, RecordedMoveToPosition, t_parkour);

            if (t_parkour >= 1f)
            {
                IsParkour = false;
                t_parkour = 0f;
                rb.isKinematic = false;

            }
        }


        //Wallrun
        /*if (DetectWallL.Obstruction && !rbfps.Grounded && !IsParkour && canwallrun) // if detect wall on the left and is not on the ground and not doing parkour(climb/vault)
        {
            WallrunningLeft = true;
            canwallrun = false;
            upforce = WallRunUpForce;
        }

        if (DetectWallR.Obstruction && !rbfps.Grounded && !IsParkour && canwallrun) // if detect wall on thr right and is not on the ground
        {
            WallrunningRight = true;
            canwallrun = false;
            upforce = WallRunUpForce;
        }
        if (WallrunningLeft && !DetectWallL.Obstruction || Input.GetAxisRaw("Vertical") <= 0f || rbfps.relativevelocity.magnitude < 1f) // if there is no wall on the lef tor pressing forward or forward speed < 1 (refer to fpscontroller script)
        {
            WallrunningLeft = false;
            WallrunningRight = false;
        }
        if (WallrunningRight && !DetectWallR.Obstruction || Input.GetAxisRaw("Vertical") <= 0f || rbfps.relativevelocity.magnitude < 1f) // same as above
        {
            WallrunningLeft = false;
            WallrunningRight = false;
        }

        if (WallrunningLeft || WallrunningRight) 
        {
            WallRunning = true;
            rbfps.Wallrunning = true; // this stops the playermovement (refer to fpscontroller script)
        }
        else
        {
            WallRunning = false;
            rbfps.Wallrunning = false;
        }

        if (WallrunningLeft)
        {     
            cameraAnimator.SetBool("WallLeft", true); //Wallrun camera tilt
        }
        else
        {
            cameraAnimator.SetBool("WallLeft", false);
        }
        if (WallrunningRight)
        {           
            cameraAnimator.SetBool("WallRight", true);
        }
        else
        {
            cameraAnimator.SetBool("WallRight", false);
        }

        if (WallRunning)
        {
            rb.AddRelativeForce(0f, upforce, 0f);
            upforce -= WallRunUpForce_DecreaseRate * Time.deltaTime; //so the player will have a curve like wallrun

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = transform.forward * WallJumpForwardVelocity + transform.up * WallJumpUpVelocity; //walljump
                WallrunningLeft = false;
                WallrunningRight = false;
            }
            if(rbfps.Grounded)
            {
                WallrunningLeft = false;
                WallrunningRight = false;
            }
        }*/


    }
  
}
