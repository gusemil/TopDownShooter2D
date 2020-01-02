using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    //private float bloodEffectTime = 2f;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Destroy(gameObject, bloodEffectTime);
    }

    public void SetBurstCount(float count)
    {
        Debug.Log(particleSystem.emission.burstCount);
        //particleSystem.emission.SetBurst(
        //emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, 100, 200), new ParticleSystem.Burst(1.0f, 10, 20) })
    }
}
