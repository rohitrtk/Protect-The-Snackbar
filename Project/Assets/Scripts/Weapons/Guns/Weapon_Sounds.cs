using UnityEngine;

/// <summary>
/// Handles the playing of gunshots
/// </summary>
public class Weapon_Sounds : MonoBehaviour
{
    /// <summary>
    /// The weapon the gunshot will be played by
    /// </summary>
    private Weapon_Abstract _weapon;

    /// <summary>
    /// The weapons current transform (So an audio sorce can be spawned there)
    /// </summary>
    private Transform _transform;

    /// <summary>
    /// Array of sounds to be played
    /// </summary>
    [SerializeField] private AudioClip[] _sounds;

    /// <summary>
    /// Called on object creation
    /// </summary>
    private void Start()
    {
        _weapon = GetComponent<Weapon_Abstract>();
        _transform = gameObject.transform;
    }

    /// <summary>
    /// Plays a random sound from the array of weapon sounds
    /// </summary>
    public void PlayShotSound()
    {
        int i = Random.Range(0, _sounds.Length);

        AudioSource.PlayClipAtPoint(_sounds[i], _transform.position, 0.3f);
    }
}