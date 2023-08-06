using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Avatar : MonoBehaviour
{
    public ParticleSystem Shape;
    public ParticleSystem Trail;
    public ParticleSystem Burst;
    public PointLight Light;
    public float DeathCountDown = -1f;

    private Player player;

    private void Awake()
    {
        player = transform.root.GetComponent<Player>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (DeathCountDown < 0f)
        {
            Shape.enableEmission = false;
            Trail.enableEmission = false;
            Burst.Emit(Burst.main.maxParticles);
            DeathCountDown = Burst.main.startLifetimeMultiplier;
        }
    }
    private void Update()
    {
        if (DeathCountDown >= 0f)
        {
            DeathCountDown -= Time.deltaTime;
            Light.range -= 0.1f; 
            if (DeathCountDown <= 0f)
            {
                DeathCountDown = -1f;
                Shape.enableEmission = true;
                Trail.enableEmission = true;
                player.Die();
            }
        }
    }
}
