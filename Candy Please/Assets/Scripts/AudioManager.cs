using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [Header("All audios file in game")]
    [SerializeField] private List<AudioData> m_audiosData = new List<AudioData>();

    #region Singleton
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = Instance == null ? this : Instance;
    }
    #endregion

    public AudioClip GetSound(string ID) 
    { 
        if (m_audiosData.Exists(x => x.audioID == ID))
            return m_audiosData.Find(x => x.audioID == ID).audioClip;
        else
            return null;
    }
}

[System.Serializable]
public class AudioData 
{ 
    public string audioID;
    public AudioClip audioClip;
}
