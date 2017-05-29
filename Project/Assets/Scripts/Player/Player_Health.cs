using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Health : MonoBehaviour {

    private const float _baseHealth = 100f;
    [SerializeField] private float _playerHealth;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _image;
    [SerializeField] private Color _healthColour = Color.red;

	// Use this for initialization
	void Start ()
    {
        _playerHealth = _baseHealth;
        _slider.value = _playerHealth;
        _image.color = _healthColour;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}