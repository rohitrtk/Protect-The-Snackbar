using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkPlayer : Photon.MonoBehaviour {

    [SerializeField] private GameObject _myCamera;

    private bool _isAlive = true;
    private Vector3 _position;
    private Quaternion _rotation;
    [SerializeField] private float _lerpSmoothing = 5f; //Smoothness, higher = more smooth but less accurate

	// Use this for initialization
	void Start () {

        if (photonView.isMine) //If this is my client's instance
        {

            _myCamera.SetActive(true); //Set my camera to the main one.
            GetComponent<Player_Handler>().enabled = true;//Allow youself to use the control script

        }
        else //If this isnt my client's instace
        {
            StartCoroutine("Alive");
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Are we reading our writing to the stream


        if (stream.isWriting)
        {
            //LAYOUT MATTERS!

            //WRITING
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else 
        {
            //READING
            _position = (Vector3)stream.ReceiveNext();
            _rotation = (Quaternion)stream.ReceiveNext();
        }
    }

    //while alive, do this
    IEnumerator Alive()
    {
        while (_isAlive)
        {
            //Set a smoothed out Rotation and position as live so its not exactly what host is saying but it keeps the game from looking gittery.
            transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime * _lerpSmoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, Time.deltaTime * _lerpSmoothing);

            yield return null;
        }
    }
}
