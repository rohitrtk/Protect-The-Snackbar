using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handels local player
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
        var horizontalMove = Input.GetAxis("Horizontal") * (_moveSpeedScale / _moveSpeed);
        var verticalMove = Input.GetAxis("Vertical") * (_moveSpeedScale / _moveSpeed);

        transform.Translate(horizontalMove, 0f, verticalMove);
    }

    /// <summary>
    /// Handles the rotation of the player
    /// </summary>
    protected void Rotate()
    {
    }

    /// <summary>
    /// Handles the moving and rotation of the players camera
    /// </summary>
    private void MovePlayerCamera()
    {
        // Move camera to player
        //_mainCamera.transform.position = transform.position + _cameraOffset;
        _mainCamera.transform.position = transform.position;                    // This looks cooler
        _mainCamera.transform.Translate(_cameraOffset);

        // Sets the players camera rotation to the rotation of the player
        var xRot = Input.GetAxis("Mouse X") * 60f;
        var yRot = Input.GetAxis("Mouse Y") * -60f;

        // Fixes camera tilt 
        transform.Rotate(xRot * Vector3.up * Time.deltaTime, Space.World);
        transform.Rotate(yRot * Vector3.right * Time.deltaTime);

        _mainCamera.transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Checks for player key presses and mouse presses that aren't related to movement
    /// </summary>
    private void CheckKeysAndMouse()
    {
        //If the user hits escape, give them their cursor back
        if (Input.GetKeyDown("escape")) _paused = !_paused;

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
