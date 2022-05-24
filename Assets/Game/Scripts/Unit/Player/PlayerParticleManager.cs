using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_effects;

    private GameObject[] effects
    {
        get
        {
            return m_effects;
        }
    }

    [Header("SpawnTransform")]
    [SerializeField]
    private Transform m_spawnFootTransform;
    private Transform spawnFootTransform
    {
        get
        {
            return m_spawnFootTransform;
        }
    }

    [SerializeField]
    private Transform m_spawnWallLeftTransform;
    private Transform spawnWallLeftTransform
    {
        get
        {
            return m_spawnFootTransform;
        }
    }

    [SerializeField]
    private Transform m_spawnWallRightTransform;
    private Transform spawnWallRightTransform
    {
        get
        {
            return m_spawnWallRightTransform;
        }
    }

    [SerializeField]
    private Transform m_spawnCenterTransform;
    private Transform spawnCenterTransform
    {
        get
        {
            return m_spawnCenterTransform;
        }
    }

    public void MoveEffect(float moveDirX)
    {
        bool isMoveDirLeft = false;
        GameObject effect2_0 = effects[2].transform.GetChild(0).gameObject;


        if (moveDirX < 0.0f)
            isMoveDirLeft = true;

        InstanceEffect0(spawnCenterTransform.position, isMoveDirLeft);
        InstacneEffectParticle(effect2_0, spawnFootTransform.position);
    }

    public void LandingEffect()
    {
        GameObject effect2_1 = effects[2].transform.GetChild(1).gameObject;
        InstacneEffectParticle(effect2_1, spawnFootTransform.position);
    }

    public void WallJumpEffect(bool isRight)
    {
        GameObject effect1 = effects[1];
        GameObject effect2_2 = effects[2].transform.GetChild(2).gameObject;

        Vector3 spawnPos = spawnWallLeftTransform.position;
        if (isRight)
            spawnPos = spawnWallRightTransform.position;

        InstacneEffectParticle(effect1, spawnPos);
        InstacneEffectParticle(effect2_2, spawnPos);
    }

    public void WallSlideEffect(bool isRight)
    {
        GameObject effect1 = effects[1];

        Vector3 spawnPos = spawnWallLeftTransform.position;
        if (isRight)
            spawnPos = spawnWallRightTransform.position;

        InstacneEffectParticle(effect1, spawnPos);
    }

    public void GroundPoundEffect()
    {
        GameObject effect1 = effects[1];
        GameObject effect2_3 = effects[2].transform.GetChild(3).gameObject;
        //GameObject effect4 = effects[4];

        Vector3 spawnPos = spawnFootTransform.position;

        InstacneEffectParticle(effect1, spawnPos);
        InstacneEffectParticle(effect2_3, spawnPos);
        //InstacneEffectParticle(effect4, spawnPos);
    }

    //public void HitEffect()
    //{
    //    GameObject effect3 = effects[3];

    //    Vector3 spawnPos = spawnHitTransform.position;

    //    InstacneEffectParticle(effect3, spawnPos);
    //}



    private GameObject InstacneEffectParticle(GameObject instacneObject, Vector3 spawnPos)
    {
        GameObject instanceEffectParticle = GameObject.Instantiate(instacneObject);
        instanceEffectParticle.transform.position = spawnPos;

        return instanceEffectParticle;
    }


    private void InstanceEffect0(Vector3 spawnPos, bool isLeft)
    {
        GameObject effect0 = InstacneEffectParticle(effects[0], spawnPos);

        ParticleSystem subEffect0 = effect0.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem subEffect1 = effect0.transform.GetChild(1).GetComponent<ParticleSystem>();

        if (isLeft)
        {
            var shape0 = subEffect0.shape;
            var shape1 = subEffect1.shape;

            shape0.rotation = -shape0.rotation;
            shape1.rotation = -shape1.rotation;
        }


    }

}
