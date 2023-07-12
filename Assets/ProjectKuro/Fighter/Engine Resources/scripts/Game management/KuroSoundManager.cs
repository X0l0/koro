using UnityEngine;

[System.Serializable]

public class Sound//makes pseudo class that represents a sound file. holds a name, audio source file, and a clip
{
    public string name;//sounds have a name
    public AudioClip clip;//an audio sample of course
    [SerializeField]
    public float Volume = 0.3f;//a adjustable volume

    private AudioSource source;//a source where they come from

    public void SetSource (AudioSource _source)//set source function
    {
        source = _source;//sets source for audio
        source.clip = clip;//sets clip
        source.volume = Volume;//sets volume
    }

    public void Play()//plays source.
    {
        source.Play();
    }

}
public class KuroSoundManager : MonoBehaviour
{

    [SerializeField]
    Sound[] sounds;//this holds the list of sounds.


    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)//this goes through all sounds in the list and instantiates them?
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);//creates game object for each sound
            _go.transform.SetParent(this.transform); //sets these as children objects
            sounds[i].SetSource (_go.AddComponent<AudioSource>());//sets source as newly created sound object.

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
        //Debug.LogWarning("AudioManager: Sound not found in list : " + _name);
    }
}
