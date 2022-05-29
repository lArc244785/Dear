using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "WeaponDatas/WeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private int m_damage;

    public int damage
    {
        get
        {
            return m_damage;
        }
    }

}
