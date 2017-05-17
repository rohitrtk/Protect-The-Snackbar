using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles local player
/// </summary>
public partial class Player_Handler : MonoBehaviour
{
    [SerializeField] private float _moveSpeedScale;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Camera _mainCamera;            // Players camera
    [SerializeField] private Vector3 _cameraOffset;         // Cameras offset

    [SerializeField] private GameObject _playerBody; //Object used as the visual repersentation of the player

    private bool _paused;                                   // Is this player paused?                    

    // PlayerWeapons gameobject (Holds reference to the attached weapons)
    [SerializeField] private Transform _playerWeapons;

    protected void Start()
    {
        _paused = false;

        //Set the player's body to the "Player" layer so that its own camera doesnt see it. Other cams can still see it becuase this info is never sent to the network
        _playerBody.layer = 8; 

        // Lock the player's cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected void Update()
    {
        Move(); 
        Rotate();

        Cursor.lockState = (_paused) ? CursorLockMode.None : CursorLockMode.Locked;

        CheckKeysAndMouse();
        MovePlayerCamera();
    }

    /// <summary>
    /// Handels the movement of the player
    /// </summary>
    protected void Move()
    {
        //var horizontalMove = Input.GetAxis("Horizontal") * (_moveSpeedScale / _moveSpeed);
        //var verticalMove = Input.GetAxis("Vertical") * (_moveSpeedScale / _moveSpeed);
        //Vector3 temp = new Vector3(verticalMove, 0.0f, horizontalMove);

        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        //transform.localPosition += temp;
        //transform.Translate(horizontalMove, 0f, verticalMove);
    }

    /// <summary>
    /// Handles the rotation of the player
    /// </summary>
    [System.Obsolete]
    protected void Rotate()
    {
    }

    /// <summary>
    /// Handles the moving and rotation of the players camera
    /// </summary>
    private void MovePlayerCamera()
    {
        // Move camera to player
        _mainCamera.transform.position = transform.position + _cameraOffset;
        //_mainCamera.transform.position = transform.position;                    // This looks cooler
        //_mainCamera.transform.Translate(_cameraOffset);

        // Sets the players camera rotation to the rotation of the player
        var xRot = Input.GetAxis("Mouse X");        // Rotation along the x axis
        var yRot = Input.GetAxis("Mouse Y") * -1;   // Rotation along the y axis

        Vector3 t = transform.localEulerAngles;
        t.y += xRot;
        t.x += yRot;

        transform.localEulerAngles = t;

        // Fixes camera tilt 
        //transform.Rotate(xRot * Vector3.up * Time.deltaTime, Space.World);
        //transform.Rotate(yRot * Vector3.right * Time.deltaTime);

        //Vector3 rotation = transform.localRotation.eulerAngles;
        //rotation.x = Mathf.Clamp(rotation.x, -45, 45);
        //transform.localRotation = Quaternion.Euler(rotation);

        _mainCamera.transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Checks for player key presses and mouse presses that aren't related to movement
    /// </summary>
    private void CheckKeysAndMouse()
    {
        //If the user hits escape, give them their cursor back
        if (Input.GetKeyDown("escape")) _paused = !_paused;

        // If the user presses & holds left shift, set speed scale to sprint speed scale
        // else return it to walk speed scale
        _moveSpeedScale = (Input.GetKey("left shift")) ? PlayerSpeeds.Sprint : PlayerSpeeds.Walk;

        // TODO: make this more efficient
        // Also need to make a bool for primary weapon
        //if(Input.GetMouseButton(0))
        if (Input.GetButton("Fire1"))
        {
            Weapon_Abstract gun = _playerWeapons.GetComponentInChildren<Weapon_Abstract>();
            gun.Fire();
        }
    }
}

/// <summary>
/// Struct contains constants for _moveSpeedScale in the Player_Handler class
/// KEEP THIS IN THE Player_Handler SCRIPT!!!
/// </summary>
public struct PlayerSpeeds
{
    public const float Walk = 0.75f;
    public const float Sprint = 1.25f;
}