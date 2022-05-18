using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogeImageManager : MonoBehaviour
{
    [SerializeField]
    private ImageDatas imageDatas;

    private Dictionary<string, Sprite> m_dialogeImageDictionary = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> dialogeImageDictionary
    {
        get
        {
            return m_dialogeImageDictionary;
        }
    }

    public void Init()
    {
        foreach(ImageData data in imageDatas.datas)
        {
            dialogeImageDictionary.Add(data.id, data.img);
        }
    }


}
