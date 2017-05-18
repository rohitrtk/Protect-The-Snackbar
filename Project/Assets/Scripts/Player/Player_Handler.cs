using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player input and the like
/// </summary>
public partial class Player_Handler : MonoBehaviour
{
    /// <summary>
    /// The speed at which the player moves at
    /// </summary>
    [SerializeField] private float _moveSpeed;

    /// <summary>
    /// Speed is divided by this, basically makes the input values
    /// easier to work with (i.e _moveSpeed = 1 as opposed to _moveSpeed = 0.27)
    /// </summary>
    [SerializeField] private float _moveSpeedScale;

    /// <summary>
    /// Players camera
    /// </summary>
    [SerializeField] private Camera _mainCamera;

    /// <summary>
    /// Cameras offset
    /// </summary>
    [SerializeField] private Vector3 _cameraOffset;

    /// <summary>
    /// Object used as the visual repersentation of the player
    /// </summary>
    [SerializeField] private GameObject _playerBody;

    /// <summary>
    /// Is this player paused?
    /// </summary>
    private bool _paused;

    /// <summary>
    /// PlayerWeapons gameobject (Holds reference to the attached weapons)
    /// </summary>
    [SerializeField] private Transform _playerWeapons;

    /// <summary>
    /// The players current primary weapon
    /// </summary>
    Weapon_Abstract _primaryWeapon;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected void Start()
    {
        // Default player is not paused
        _paused = false;

        // Gets the weapon abstract class from the weapon
        _primaryWeapon = _playerWeapons.GetComponentInChildren<Weapon_Abstract>();

        //Set the player's body to the "Player" layer so that its own camera doesnt see it. Other cams can still see it becuase this info is never sent to the network
        _playerBody.layer = 8;

        // Lock the player's cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    protected void Update()
    {
        Move();
        Rotate();

        // Moves the players camera
        MovePlayerCamera();

        // Check for key presses and mouse clicks
        CheckKeysAndMouse();

        // Sets the cursor lock state based on if the player is paused or not
        Cursor.lockState = (_paused) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /// <summary>
    /// Handles the movement of the player
    /// </summary>
    protected void Move()
    {
        // Stores the horizontal movement of the player
        float horizontalMove = Input.GetAxis("Horizontal") * (_moveSpeedScale / _moveSpeed);

        // Stores the vertical movement of the player
        float verticalMove = Input.GetAxis("Vertical") * (_moveSpeedScale / _moveSpeed);

        // Stores the horizontal and vertical movement as a vector
        Vector3 move = new Vector3(horizontalMove, 0.0f, verticalMove);

        // Sets the direction of the move vector to that of the players camera direction
        move = _mainCamera.transform.TransformDirection(move);

        // Stops players y from increasing
        move.y = 0f;

        // Move the player based on the move vector relative to the world
        transform.Translate(move , Space.World);
    }

    /// <summary>
    /// <summary>
    /// Handles the rotation of the player
    /// </summary>
    protected void Rotate()
    {
        // Rotation along the x axis
        float xRot = Input.GetAxis("Mouse X");

        // Rotation along the y axis
        float yRot = Input.GetAxis("Mouse Y") * -1;

        // Get the rotation as a Vector3 and add the mouse rotations to it
        Vector3 t = transform.localEulerAngles;
        t.y += xRot;
        t.x += yRot;

        // Converts the angle to a negative value if the angle is greater than 180
        if (t.x > 180) t.x -= 360;

        // Clamp the angle between -45 degrees and 45 degrees
        t.x = Mathf.Clamp(t.x, -45, 45);

        // Convert the angle back to a positive value if it less than 0
        if (t.x < 0) t.x += 360;

        // Set the rotation to the Vector3 rotation
        transform.localEulerAngles = t;
    }

    /// <summary>
    /// Handles the moving and rotation of the players camera
    /// </summary>
    protected void MovePlayerCamera()
    {
        // Sets the cameras position to the players position
        _mainCamera.transform.position = transform.position;

        // Translates the cameras position by the camera offset
        _mainCamera.transform.Translate(_cameraOffset);

        // Sets the rotation of the camera to the players rotation
        _mainCamera.transform.rotation = transform.rotation;
    }

    /// <summary>
    /// Checks for player key presses and mouse presses that aren't related to movement
    /// </summary>
    protected void CheckKeysAndMouse()
    {
        //If the user hits escape, give them their cursor back
        if (Input.GetKeyDown("escape")) _paused = !_paused;

        // If the user presses & holds left shift, set speed scale to sprint speed scale
        // else return it to walk speed scale
        _moveSpeedScale = (Input.GetKey("left shift")) ? PlayerSpeeds.Sprint : PlayerSpeeds.Walk;

        // TODO: make this more efficient also need to make a bool for primary weapon
        if (Input.GetButtonDown("Fire1")) FireWeapon(false);
        else if (Input.GetButton("Fire1")) FireWeapon(true);
    }

    /// <summary>
    /// Called to fire the players weapon. Parameter stores whether
    /// or not the mouse is being held down or was single clicked
    /// </summary>
    /// <param name="held"></param>
    protected void FireWeapon(bool held)
    {
        _primaryWeapon.Fire(held);
        print(held);
    }
}

/// <summary>
/// Struct contains constants for _moveSpeedScale in the Player_Handler class
/// KEEP THIS IN THE Player_Handler SCRIPT!!!
/// </summary>
public struct PlayerSpeeds
{
    /// <summary>
    /// Walk speed scale
    /// </summary>
    public const float Walk = 0.75f;

    /// <summary>
    /// Sprint speed scale
    /// </summary>
    public const float Sprint = 1.25f;
}