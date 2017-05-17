using System;
using UnityEngine;

/// <summary>
/// Handles setting up the player and holds reference to other scripts the player uses.
/// </summary>
[Obsolete("Player_Control is deprecated, Please use the Player_Handler class instead.", true)]
public partial class Player_Control : MonoBehaviour
{
    // Players camera
    [SerializeField] private Camera _mainCamera;

    // Cameras offset
    [SerializeField] private Vector3 _cameraOffset;

    // Is this player paused?
    private bool _paused;                    

    // PlayerWeapons gameobject (Holds reference to the attached weapons)
    [SerializeField] private Transform _playerWeapons;                       

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start ()
    {
        // Set this players camera to the scenes main camera
        _mainCamera = Camera.main;

        // Default player is not paused
        _paused = false;

        // Lock the player's cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update ()
    {
        // Sets the cursor lock state based on if this player is paused
        Cursor.lockState = (_paused) ? CursorLockMode.None : CursorLockMode.Locked;

        // Checks for key presses and mouse movements
        CheckKeysAndMouse();

        // Moves the players camera
        MovePlayerCamera();
    }

    /// <summary>
    /// Handles the moving and rotation of the players camera
    /// </summary>
    private void MovePlayerCamera()
    {
        // Move camera to player
        _mainCamera.transform.position = transform.position;
        _mainCamera.transform.Translate(_cameraOffset);

        // Sets the players camera rotation to the rotation of the player
        var xRot = Input.GetAxis("Mouse X") * 60f;
        var yRot = Input.GetAxis("Mouse Y") * -60f;

        // Fixes camera tilt 
        transform.Rotate(xRot * Vector3.up * Time.deltaTime, Space.World);
        transform.Rotate(yRot * Vector3.right * Time.deltaTime);

        // Sets the main cameras rotation to the players rotation
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
        if(Input.GetButton("Fire1"))
        {
            Weapon_Abstract gun = _playerWeapons.GetComponentInChildren<Weapon_Abstract>();
            gun.Fire();
        }
    }
}