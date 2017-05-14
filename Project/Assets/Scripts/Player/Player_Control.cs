using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles setting up the player and holds reference to other scripts the player uses.
/// </summary>
public partial class Player_Control : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;            // Players camera
    [SerializeField] private Vector3 _cameraOffset;         // Cameras offset

    private Player_Movement _playerMovement;                // Player movement object
    private bool _paused;

    private Transform _playerWeapons;                       // PlayerWeapons gameobject (Holds reference to the attached weapons)

    /// <summary>
    /// Referance of the Player gameObject
    /// Will be deleted
    /// </summary>
    public GameObject Instance;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start ()
    {
        _mainCamera = Camera.main;
        _paused = false;
        _playerMovement = GetComponent<Player_Movement>();

        _playerWeapons = transform.Find("PlayerWeapons");

        // Lock the player's cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update ()
    {
        CheckKeys();
        MovePlayerCamera();
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
    /// Checks for player key presses that aren't related to movement
    /// </summary>
    private void CheckKeys()
    {
        //If the user hits escape, give them their cursor back
        if (Input.GetKeyDown("escape")) _paused = !_paused;
    }
}