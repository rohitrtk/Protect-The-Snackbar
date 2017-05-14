using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles setting up the player and holds reference to other scripts the player uses.
/// </summary>
public class Player_Control : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;            // Players camera
    [SerializeField] private Vector3 _cameraOffset;         // Cameras offset

    private Player_Movement _playerMovement;                // Player movement object
    private bool _paused;

    /// <summary>
    /// Referance of the Player gameObject
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

        // Lock the player's cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update ()
    {
        MovePlayerCamera();

        //If the user hits escape, give them their cursor back
        if (Input.GetKeyDown("escape")) { _paused = !_paused; } //TODO Later this line should be moved to wherever we are going to handel all the other getKey actions


        if (_paused)
        {
            Debug.Log("Unlocked cursor");
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //Weird, you have to click for the lock to lock back to the game again. //TODO <<< fix this <<<
            //Debug.Log("Locked cursor");
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    /// <summary>
    /// Handles the moving and rotation of the players camera
    /// </summary>
    private void MovePlayerCamera()
    {
        //Move camera to player
        //_mainCamera.transform.position = transform.position + _cameraOffset;
        _mainCamera.transform.position = transform.position;                    // This looks cooler
        _mainCamera.transform.Translate(_cameraOffset);

        // Sets the players camera rotation to the rotation of the player
        _mainCamera.transform.rotation = transform.rotation;
    }
}
