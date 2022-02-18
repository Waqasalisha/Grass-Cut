using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour
{
    public GameObject grass;
    public static int trisggercount;
    public ParticleSystem GrassCutParticle;
    public ParticleSystem bubbleParticle;

    //public ParticleSystem GlowCutterParticle;

    public LevelManager _lm { get; private set; }
    public AudioSource audioSource;
    public AudioClip cutSound;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cutter")
        {
            if (grass.activeSelf)
            {

                grass.SetActive(false);
                if (_lm._triggerCount > 0)
                {
                    audioSource.PlayOneShot(cutSound);
                    
                }
                GrassCutParticle.Play();
                bubbleParticle.Play();
                _lm.IncreamentTriggerCount();
            }
        }
        //print("triggerCount"+ triggercount);
    }

    public void SetLevelManager(LevelManager lm)
    {
        this._lm = lm;
    }
}



