using UnityEngine;

/// <summary>
/// Temporary GameManager (base NetworkManager off of this class)
/// </summary>
public partial class GameManager : MonoBehaviour
{
    /// <summary>
    /// Where the player spawns, connected to empty object
    /// </summary>
    public Transform Spawn;
    public GameObject PlayerPrefab;
    public Player_Control Instance;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
    }
}