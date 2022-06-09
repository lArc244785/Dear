using UnityEngine;

public class GroundInfo : MonoBehaviour
{
    public enum Type
    {
        Forest, Fectory
    }

    [SerializeField]
    private Type m_type;

    public Type type
    {
        get
        {
            return m_type;
        }
    }

}
