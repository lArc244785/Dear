using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class SpineController : MonoBehaviour
{
    [SerializeField]
    private SkeletonAnimation m_skeletonAnimation;
    public SkeletonAnimation skeletonAnimation
    {
        get
        {
            return m_skeletonAnimation;
        }
    }
    private AnimationReferenceAsset[] m_aniClips;
    public AnimationReferenceAsset[] anuClips
    {
        get
        {
            return m_aniClips;
        }
    }
    
    public void SetStartState(int clipNum = 0)
    {
        m_skeletonAnimation.state.SetAnimation(0, anuClips[clipNum], true);
    }
    public void NoloopAni(int currentClipNum,int nextClipNum)
    {

    }




}
