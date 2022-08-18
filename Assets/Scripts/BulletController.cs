using System;
using UnityEngine;


public class BulletController : MonoBehaviour
{

    [SerializeField] private GameObject _bulletDecal;

    [SerializeField]
    private float _bulletSpeed = 50.0f;
    [SerializeField]
    private float _timeToDestroy = 2.0f;

    [Tooltip("[Readonly] Goal or Target (Bullet's Destination)")]
    [SerializeField]
    private Vector3 _target;
    //
    public Vector3 Target { get => _target; set => _target = value; }

    [field: Tooltip("Boolean to say if the Bullet Hit a Target")]
    [SerializeField]
    public bool isThereABulletHit { get; set; }

    private float _zeroSquareThreshold = 0.01f;


    private void OnEnable()
    {
        // We schedule a 'future' Destroy operation, after some seconds...:
        //
        Destroy(gameObject, _timeToDestroy);
    }

    
    private void Update()
    {
        // We move the Bullet towards its Destination:
        //
        transform.position = Vector3.MoveTowards(transform.position, _target, _bulletSpeed * Time.deltaTime);
        
        // Check to see if there is a Collision (Bullet vs. Target GameObject)
        //
        if (!isThereABulletHit && ((transform.position - _target).sqrMagnitude < _zeroSquareThreshold))
        {
            Destroy(gameObject);
            
        }// End if
    }


    private void OnCollisionEnter(Collision collision)
    {

        ContactPoint contactPoint = collision.GetContact(0);
        //
        // Instantiate the Bullet Decal in the Wall/Object where it is Colliding:
        //
        GameObject.Instantiate(_bulletDecal, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
        
        Destroy(gameObject);
    }
}