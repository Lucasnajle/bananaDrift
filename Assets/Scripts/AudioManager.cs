using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using System.Collections;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource[] audioSources;

    public AudioClip[] audios;

    private const int MUSIC_CHANNEL = 0;
    private const int ENGINE_CHANNEL = 1;
    private const float DEFAULT_VOLUME_MUSIC = .05f;
    private const float DEFAULT_VOLUME = .1f;
    private int currentChannelIndex = 2;

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        PlayMusic();
        PlayEngine();
    }

    void Update()
    {
        
    }

    public void PlayMusic()
    {
        
        audioSources[MUSIC_CHANNEL].volume = DEFAULT_VOLUME_MUSIC;
        audioSources[MUSIC_CHANNEL].loop = true;
        audioSources[MUSIC_CHANNEL].Play();
    }
    public void PlayEngine()
    {
        audioSources[ENGINE_CHANNEL].volume = DEFAULT_VOLUME_MUSIC;
        audioSources[ENGINE_CHANNEL].loop = true;
        audioSources[ENGINE_CHANNEL].pitch = .5f;
        StartCoroutine(StartEngine());
        audioSources[ENGINE_CHANNEL].Play();
    }

    public IEnumerator StartEngine()
    {
        yield return new WaitForSeconds(1);
        float enginePitch = .5f;

        DOTween.To(() => enginePitch, x => enginePitch = x, 1f, 1f).OnUpdate(() =>{
            audioSources[ENGINE_CHANNEL].pitch = enginePitch;
        });
        
    }
    public void PlayAudio(int SFXIdx, float volume = DEFAULT_VOLUME, bool loop = false)
    {
        audioSources[currentChannelIndex].clip = audios[SFXIdx];
        audioSources[currentChannelIndex].loop = loop;
        audioSources[currentChannelIndex].volume = volume;
        audioSources[currentChannelIndex].pitch = 1 + Random.Range(-.05f,.05f);
        currentChannelIndex++;
        if (currentChannelIndex >= audioSources.Length)
        {
            currentChannelIndex = 2;
        }

    }
}
