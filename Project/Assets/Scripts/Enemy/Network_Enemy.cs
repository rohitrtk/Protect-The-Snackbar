using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network_Enemy : MonoBehaviour
{

    private bool _isAlive = true;
    private Vector3 _position;
    private Quaternion _rotation;
    [SerializeField] private float _lerpSmoothing = 5f; //Smoothness, higher = more smooth but less accurate

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
