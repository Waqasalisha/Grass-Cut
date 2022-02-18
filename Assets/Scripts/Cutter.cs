using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GG.Infrastructure.Utils.Swipe;
using DG.Tweening;
using System.Linq;

public class Cutter : MonoBehaviour
{
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private LayerMask wallsAndRoadsLayer;
    [SerializeField] ParticleSystem sparkParticle;
    [Space]
    [SerializeField] float rotationSpeed = 20f;
    
     float MoveDuration = 0.14f;


    private const float Max_Ray_Distance = 200f;

    Rigidbody _cutterRb;
    bool _isMoving;
    Queue<Vector3> _bufferedInputs = new Queue<Vector3>();
    Vector3 _moveDirection;
    //Change
    private void Awake()
    {
        _cutterRb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        swipeListener.OnSwipe.AddListener(swipe =>
        {
            switch (swipe)
            {
                case "Right":
                    _moveDirection = Vector3.right;
                    break;

                case "Left":
                    _moveDirection = Vector3.left;
                    break;

                case "Up":
                    _moveDirection = Vector3.forward;
                    break;

                case "Down":
                    _moveDirection = Vector3.back;
                    break;
            }
            MoveCutter(_moveDirection,MoveDuration);
        });
    }

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed);
        //print(_bufferedInputs.Count);    
    }



    void MoveCutter(Vector3 newDirection,float moveDur)
    {
        if (_isMoving)
        {
            if (_bufferedInputs.Count <= 2)
            { 
                _bufferedInputs.Enqueue(newDirection);
               
            }
        }

        else
        {
            Ray moveDirRay = new Ray(transform.position, newDirection);
            RaycastHit moveRayHitInfo;

            Vector3 targetPosition = Vector3.zero;

            if (Physics.Raycast(moveDirRay, out moveRayHitInfo, Max_Ray_Distance, LayerMask.NameToLayer("Walls And Road"), QueryTriggerInteraction.Ignore))
            {
                targetPosition = moveRayHitInfo.collider.transform.position - (newDirection + new Vector3(0f, -0.5f, 0f));
                sparkParticle.transform.position = targetPosition;
                
            }

            _isMoving = true;
            transform
            .DOMove(targetPosition, MoveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(()=>{
                _isMoving = false;

                PlaySparkParticle();
                MoveCutter(_bufferedInputs.Dequeue(), moveDur - ( 0.05f * _bufferedInputs.Count));
                
            
            
            });
           
        }
       
    }


    void PlaySparkParticle()
    {
        
        sparkParticle.Play();
    }




}