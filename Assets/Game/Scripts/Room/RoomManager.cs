using Cinemachine;
using System.Collections;
using UnityEngine;
public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineConfiner m_confiner;

    private Room[] m_rooms;
    private Transform m_playerTransform;
    private Transform playerTransform
    {

        get
        {
            return m_playerTransform;
        }
    }

    public void Init(Transform playerTr)
    {
        m_rooms = new Room[transform.childCount];
        m_playerTransform = playerTr;

        for (int i = 0; i < m_rooms.Length; i++)
        {
            m_rooms[i] = transform.GetChild(i).GetComponent<Room>();
        }
    }

    public void NextRoom(int index)
    {
        StartCoroutine(RoomMoveCoroutine(index));
    }

    private IEnumerator RoomMoveCoroutine(int index)
    {
        GameManager.instance.stageManager.player.isControl = false;

        GameManager.instance.stageManager.player.rig2D.velocity = Vector2.zero;


        UIManager.instance.produtionView.Toggle(true);
        UIManager.instance.produtionView.fade.FadeOut();

        while (!UIManager.instance.produtionView.fade.fadeProcessed)
            yield return null;

        playerTransform.position = (Vector3)m_rooms[index].startPoint;
        m_confiner.m_BoundingShape2D = m_rooms[index].cameraConfiner;

        yield return new WaitForSeconds(1.0f);

        UIManager.instance.produtionView.fade.FadeIn();

        yield return new WaitForSeconds(0.5f);
        GameManager.instance.stageManager.player.isControl = true;
    }

}
