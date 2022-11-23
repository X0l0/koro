using UnityEngine;

[System.Serializable]

public class Sound//makes pseudo class that represents a sound file. holds a name, audio source file, and a clip
{
    public string name;
    public AudioClip clip;
    [SerializeField]
    public float Volume = 0.3f;

    private AudioSource source;

    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = Volume;
    }

    public void Play()
    {
        source.Play();
    }

}
public class SoundManager : MonoBehaviour
{
    //public static SoundManager instance;//singleton, take away.


    [SerializeField]
    Sound[] sounds;//this holds the list of sounds.

    //private void Awake()
    //{
        //if (instance != null)
        //{
        //    Debug.LogWarning("More than one SoundManager in the scene stupid head.");
        //}
        //else
        //{
        //instance = this;

        //}
    //}

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)//this goes through all sounds in the list and instantiates them?
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource (_go.AddComponent<AudioSource>());



        }

       
    }
    public void PlaySound(string _name)//this is whats called by other scripts
    {
        for (int i = 0; i < sounds.Length; i++)//this goes through all the sounds
        {
            if (sounds[i].name == _name)//if the name given matches one in the list
            {
                sounds[i].Play();//it will then play that sound in the list.
                return;
            }
        }
        //no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list : " + _name);
    }
}
