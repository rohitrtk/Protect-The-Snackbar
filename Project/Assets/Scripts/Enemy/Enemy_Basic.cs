using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy_Basic : Enemy_Abstract
{
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
        if(Health <= 0)
        {
            Dead = true;
        }

        if (Dead)
        {
            Health = 0; //Keep reseting health so that we dont get a stack overflow from shooting it too many times

            //Make a new material(So it doesnt make all red materials black) and apply it to the dead enemy
            Color black = new Color(0, 0, 0, 1);
            MeshRenderer mesh = _model.GetComponent<MeshRenderer>();
            Material blackMaterial = new Material(Shader.Find("Standard"));
            blackMaterial.color = black;
            mesh.material = blackMaterial;

        }
    }
}