using UnityEngine;

/// <summary>
/// The basic enemy class, extends from the Enemy_Abstract class
/// </summary>
public partial class Enemy_Basic : Enemy_Abstract
{
    /// <summary>
    /// Holds the model for the enemy as a gameobject
    /// </summary>
    [SerializeField] private GameObject _model;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    protected override void Start()
    {
        Dead = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        // If the health is equal to or less than 0, this enemy is dead
        if(Health <= 0)
        {
            Dead = true;
        }

        // Inverted if statement to reduce nesting
        if (!Dead) return;

        // Keep reseting health so that we dont get a stack overflow from shooting it too many times
        Health = 0;

        //Make a new material (So it doesnt make all red materials black) and apply it to the dead enemy
        Color black = new Color(0, 0, 0, 1);
        MeshRenderer mesh = _model.GetComponent<MeshRenderer>();
        Material blackMaterial = new Material(Shader.Find("Standard"));
        blackMaterial.color = black;
        mesh.material = blackMaterial;
    }
}