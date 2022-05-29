using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : SingleToon<ImageManager>
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

    private void Awake()
    {
        Init();
    }


    protected override bool Init()
    {
        bool isOverlap =  base.Init();
        if (!isOverlap)
            return false;

        foreach (ImageData data in imageDatas.datas)
        {
            dialogeImageDictionary.Add(data.id, data.img);
        }

        return isOverlap;
    }


}
