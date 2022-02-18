//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class FlowerSpawner : MonoBehaviour
{
    public List<GameObject> Flowers;
    
    public static List<GameObject> paths;

    Vector3 _originalScale;
    Transform _flowerHolder;
    public ParticleSystem Confetti;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip ConfettiSFX;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

   

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<PathScript>())
        {
            int randomFlower = Random.Range(0, Flowers.Count); 
            GameObject go = Instantiate(Flowers[randomFlower],new Vector3( col.gameObject.transform.position.x, 0, col.gameObject.transform.position.z), Flowers[randomFlower].transform.rotation);
            // Vector3 defaultScale = go.transform.localScale;
            audioSource.PlayOneShot(audioClip);
            go.transform.DOPunchScale(new Vector3(5, 5, 5), 0.5f, 1, 10).SetEase(Ease.OutBack);
            
            //.OnComplete(()=> go.transform.localScale= new Vector3(3.5f, 3.5f, 3.5f));

            if (_flowerHolder != null)
            {
                go.transform.SetParent(_flowerHolder);
            }
        }
    }

    public bool RunLevelCompleteFlowerEffect(float flowerEffectDuration, Transform level)
    {
        _flowerHolder = new GameObject("FlowerHolder").transform;
        _flowerHolder.transform.SetParent(level, true);

        transform.DOScale(30f, 2.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() => ConfettiSapawner());

        return true;
        
       
    }

    public void ResetEffect()
    {
        transform.localScale = _originalScale;

        Destroy(_flowerHolder.gameObject);
    }

  public void ConfettiSapawner()
    {
        transform.localScale = Vector3.zero;
        audioSource.PlayOneShot(ConfettiSFX);
        Confetti.Play();



    }

   



}
