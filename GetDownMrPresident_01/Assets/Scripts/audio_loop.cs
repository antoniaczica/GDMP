using UnityEngine;
using System.Collections;

//[RequireCompomnent(typeof(AudioSource))]
public class audio_loop : MonoBehaviour 
{
	public AudioClip introClip;
	public AudioClip loopClip;

	private AudioSource music; 
	void Start () 
	{
		music = GetComponent<AudioSource> ();
		music.loop = false;
		//StartCoroutine (playMusic());
		music.clip = introClip;
		music.Play ();
	}

	void Update ()
	{
		if (!music.isPlaying) 
		{
			music.clip = loopClip;
			music.Play ();
			music.loop = true;
		}
	}

	/*IEnumerator playMusic ()
	{
		music.clip = introClip;

		yield return new WaitForSeconds (1);
		//yield return null; // 
		if (music.isPlaying) {
			yield return null;
			print ("music playing");
			
		}
			//yield return new WaitForSeconds (music.clip.length);
		music.clip = loopClip;
		music.Play ();
		music.loop = true;
	}*/
}
