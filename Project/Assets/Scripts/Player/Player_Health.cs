using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Responsible for updating the players health UI and checking if the player is dead
/// </summary>
public class Player_Health : MonoBehaviour 
{
    /// <summary>
    /// Base health the player has
    /// </summary>
    private const float _baseHealth = 100f;

    /// <summary>
    /// How much health the player currently has
    /// </summary>
    [SerializeField] private float _playerHealth;

    /// <summary>
    /// Slider object used for health
    /// </summary>
    [SerializeField] private Slider _slider;

    /// <summary>
    /// Image being used to show how much health the player has
    /// </summary>
    [SerializeField] private Image _image;

    /// <summary>
    /// The colour of the health image
    /// </summary>
    [SerializeField] private Color _healthColour = new Color(255, 60, 60, 90);

	// Use this for initialization
	protected void Start ()
    {
        // Initial setups
        _playerHealth = _baseHealth;
        _slider.value = _playerHealth;
        _image.color = _healthColour;
	}
	
    /// <summary>
    /// Used to make the player take damage and update their UI
    /// </summary>
    /// <param name="damageTaken"></param>
    public void TakeDamage(float damageTaken)
    {
        _playerHealth -= damageTaken;
        _slider.value = _playerHealth;
    }

    /// <summary>
    /// Returns true if the players health is less than or equal to 0
    /// </summary>
    /// <returns></returns>
    public bool CheckDead()
    {
        if (_playerHealth <= 0) return true;
        return false;
    }
}