using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("Inspector references")]

    //Collecting references
    public Transform orientation;
    public Transform player;
    public Transform playerObject;
    public Rigidbody rigidB;
    public KeyCode aimKey = KeyCode.Mouse1;

    public GameObject thirdPersonCam;
    public GameObject combatCam;

    public float rotationSpeed;

    public Transform combatLookat;
    public CameraStyle currentStyle;

    public enum CameraStyle
    {
        Basic,
        Combat
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(Input.GetKeyDown(aimKey)) SwitchCamStyle(CameraStyle.Combat);
        if(Input.GetKeyUp(aimKey)) SwitchCamStyle(CameraStyle.Basic);

        // Get rotation orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // Rotating the player object
        if(currentStyle == CameraStyle.Basic) 
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            Vector3 dirToCombatLookAt = combatLookat.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObject.forward = dirToCombatLookAt.normalized;
        }

    }


    // Changing the camera style for the player
    private void SwitchCamStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);

        if(newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if(newStyle == CameraStyle.Combat) combatCam.SetActive(true);

        currentStyle = newStyle;
    }    
}
