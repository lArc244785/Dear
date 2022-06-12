using UnityEngine;

public class PlayerParticleManager : MonoBehaviour
{
    [Header("ImfeactObject")]
    [SerializeField]
    private GameObject m_move;
    [SerializeField]
    private GameObject m_jump;
    [SerializeField]
    private GameObject m_landing;
    [SerializeField]
    private GameObject m_wallJump;
    [SerializeField]
    private GameObject m_wallSlide;
    [SerializeField]
    private GameObject m_groundPound;
    [SerializeField]
    private GameObject m_hit;
    [SerializeField]
    private GameObject m_ropePick;




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

    //GroundInfo에 맞추어서 넣어주세요
    [Header("GroundInfoColors")]
    [SerializeField]
    private Color[] m_groundInfoColors;

    public void MoveEffect(float moveDirX)
    {

        GameObject Imfect = InstacneEffectParticle(m_move, spawnFootTransform.position);
        if (moveDirX > 0.0f)
        {
            Vector3 scale = Imfect.transform.localScale;
            scale.x *= -1.0f;
            Imfect.transform.localScale = scale;
        }

    }

    public void JumpEffect()
    {
        GameObject Imfect = InstacneEffectParticle(m_jump, spawnCenterTransform.position);
        Imfect.transform.parent = spawnCenterTransform;
    }


    public void LandingEffect(float lookDirX)
    {
        GameObject Imfect = InstacneEffectParticle(m_landing, spawnFootTransform.position);
        if (lookDirX > 0.0f)
        {
            Vector3 scale = Imfect.transform.localScale;
            scale.x *= -1.0f;
            Imfect.transform.localScale = scale;
        }
    }

    public void WallJumpEffect(bool isRight, GroundInfo.Type type)
    {
        Vector3 spawnPos = spawnWallLeftTransform.position;
        if (isRight)
            spawnPos = spawnWallRightTransform.position;


        GameObject Imfect = InstacneEffectParticle(m_wallJump, spawnPos);
        ParticleSystem particle = Imfect.GetComponent<ParticleSystem>();

        //타입에 따라서 파티클 색상을 변경해야된다.
        particle.startColor = GetGroundTypeColor(type);
    }

    public void WallSlideEffect(bool isRight, GroundInfo.Type type)
    { 
        Vector3 spawnPos = spawnWallLeftTransform.position;
        if (isRight)
            spawnPos = spawnWallRightTransform.position;

        GameObject Imfect = InstacneEffectParticle(m_wallJump, spawnPos);
        ParticleSystem particle = Imfect.GetComponent<ParticleSystem>();

        //타입에 따라서 파티클 색상을 변경해야된다.
        particle.startColor = GetGroundTypeColor(type);
    }

    public void GroundPoundEffect(GroundInfo.Type type)
    {
        GameObject Imfect =  InstacneEffectParticle(m_groundPound, m_spawnFootTransform.position);
        ParticleSystem particle = Imfect.GetComponent<ParticleSystem>();

        particle.startColor = GetGroundTypeColor(type);
    }

    public void HitEffect()
    {
        InstacneEffectParticle(m_hit, m_spawnCenterTransform.position);
    }

    public void RopePickEffect(Vector3 pos)
    {
        InstacneEffectParticle(m_ropePick, pos);
    }


    private Color GetGroundTypeColor(GroundInfo.Type type)
    {
        Color color = Color.white;


        if(m_groundInfoColors.Length > (int)type)
        color = m_groundInfoColors[(int)type];
        
        return color;
    }



    private GameObject InstacneEffectParticle(GameObject instacneObject, Vector3 spawnPos)
    {
        GameObject instanceEffectParticle = GameObject.Instantiate(instacneObject);
        instanceEffectParticle.transform.position = spawnPos;

        return instanceEffectParticle;
    }




}
