using UnityEngine;

public class InteractionGrapping : InteractionBase
{
    [SerializeField]
    private GameObject m_icon;
    [SerializeField]
    private CircleCollider2D m_clickAbleCollider;

    private bool m_isCanInteraction;
    public bool isCanInteraction
    {
        private set
        {
            m_isCanInteraction = value;
        }
        get
        {
            return m_isCanInteraction;
        }
    }

    private int m_grappingRangeIndex;
    public int grappingRangeIndex
    {
        private set
        {
            m_grappingRangeIndex = value;
        }
        get
        {
            return m_grappingRangeIndex;
        }
    }

    protected override void Enter(Collider2D collision)
    {
        base.Enter(collision);
        if (collision.tag != "Grapping")
            return;


        GrappingGunHandler handler = collision.GetComponent<GrappingGunHandler>();
        if (handler == null)
            return;

        OffInteraction();
        grappingRangeIndex = handler.grapplingGun.AddInteraction(this);

        //if (handler.grapplingGun.currentState == GrapplingGun.State.None && 
        //    handler.grapplingGun.IsOnInteractionRange(transform.position))
        //    OnInteraction();

    }

    protected override void Exit(Collider2D collision)
    {
        base.Exit(collision);
        if (collision.tag != "Grapping")
            return;

        GrappingGunHandler handler = collision.GetComponent<GrappingGunHandler>();
        if (handler == null)
            return;

        handler.grapplingGun.RemoveInteraction(this);
        grappingRangeIndex = -1;
        OffInteraction();
    }

    public void OffInteraction()
    {
        m_icon.SetActive(false);
        isCanInteraction = false;
    }

    public void OnInteraction()
    {
        m_icon.SetActive(true);

        isCanInteraction = true;

    }

    public float GetClickRange()
    {
        return m_clickAbleCollider.radius;
    }

}
