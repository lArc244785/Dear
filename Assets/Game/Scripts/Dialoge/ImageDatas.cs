using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "imageData", menuName = "imageDatas/imageData")]
public class ImageDatas : ScriptableObject
{
    [SerializeField]
    private ImageData[] m_datas;
   public ImageData[] datas
    {
        get
        {
            return m_datas;
        }
    }
}
