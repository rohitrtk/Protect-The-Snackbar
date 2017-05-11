using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles setting up the player and holds reference to other scripts the player uses.
/// </summary>
public class Player_Control : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;        // Players camera
    [SerializeField] private Vector3 _cameraOffset;     // Cameras offset

    private Player_Movement _playerMovement;            // Player movement object

    public GameObject Instance;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start ()
    {
        _mainCamera = Camera.main;

        _playerMovement = GetComponent<Player_Movement>();
	}

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update ()
    {
        MovePlayerCamera();
	}

    /// <summary>
    /// Handles the moving of the players camera
    /// </summary>
    private void MovePlayerCamera()
    {
        _mainCamera.transform.position = transform.position;
        _mainCamera.transform.Translate(_cameraOffset);
    }
}
