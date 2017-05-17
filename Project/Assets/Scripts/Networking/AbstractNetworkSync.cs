using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractNetworkSync : Photon.MonoBehaviour {

    /// <summary>
    /// Smoothness, higher = more smooth but less accurate
    /// </summary>
    [SerializeField] private float _lerpSmoothing = 5f;



    protected abstract void Start();
    protected abstract void Update();
    protected abstract void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info);


    /// <summary>
    /// Smoothness, higher = more smooth but less accurate
    /// </summary>
    public float LerpSmoothing
    {
        get
        {
            return _lerpSmoothing;
        }
        set
        {
            _lerpSmoothing = value;
        }
    }

}
