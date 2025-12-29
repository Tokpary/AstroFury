using System.Collections;
using System.Collections.Generic;
using Patterns.ObjectPool.Components;
using UnityEngine;

public class SfxPlayer : AObjectPoolManager<SfxSound>
{

    public void PlaySfx(AudioClip audioClip)
    {
        SfxSound soundSource = CreatePoolElement(elementPrototype, initialNumberOfElements, allowAddNewElements);

        if (soundSource != null)
        {
            soundSource.transform.SetParent(transform);
            soundSource.PlaySfx(audioClip);
        }
    }

}
