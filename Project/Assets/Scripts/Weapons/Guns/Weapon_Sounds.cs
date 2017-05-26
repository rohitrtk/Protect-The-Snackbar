using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sounds : MonoBehaviour
{
    private Weapon_Abstract _weapon;
    private Transform _transform;

    [SerializeField] private AudioClip[] _sounds;

    private void Start()
    {
        _weapon = GetComponent<Weapon_Abstract>();
        _transform = gameObject.transform;
    }

    public void PlayShotSound()
    {
        int i = Random.Range(0, _sounds.Length);

        AudioSource.PlayClipAtPoint(_sounds[i], _transform.position, 0.3f);
    }
}