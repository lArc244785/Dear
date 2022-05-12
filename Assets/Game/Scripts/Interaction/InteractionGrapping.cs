using UnityEngine;

public class InteractionGrapping : InteractionBase
{
    [SerializeField]
    private GameObject m_icon;
    private GameObject icon
    {
        get
        {
            return m_icon;
        }
    }

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

        if (handler.grapplingGun.currentState == GrapplingGun.State.None)
            OnInteraction();

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
        icon.SetActive(false);
        isCanInteraction = false;
    }

    public void OnInteraction()
    {
        icon.SetActive(true);

        isCanInteraction = true;

    }

}
