using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Prefabs.App
{
  public class AudioController : Common.SingletonMonoBehaviour<AudioController>
  {
    private AudioSource _audio;

    public enum AUDIO_NAME {
      [StringValue("amai")]
      AMAI,
      [StringValue("button")]
      BUTTON,
      [StringValue("fugoukaku")]
      FUGOUKAKU,
      [StringValue("goukaku")]
      GOUKAKU,
      [StringValue("kansei_big")]
      KANSEI_BIG,
      [StringValue("kansei_middle")]
      KANSEI_MIDDLE,
      [StringValue("kodutumi")]
      KODUTUMI,
      [StringValue("kurae")]
      KURAE,
      [StringValue("tou")]
      TOU,
      [StringValue("muti")]
      MUTI,
    }

    private void Start()
    {
      this._audio = this.GetComponent<AudioSource>();
    }

    public void PlayRandomManVoice()
    {
      List<AUDIO_NAME> voices = new List<AUDIO_NAME>()
      {
        AUDIO_NAME.AMAI,
        AUDIO_NAME.KURAE,
        AUDIO_NAME.TOU,
      };
      this.PlayAudio(voices[Random.Range(0, 3)]);
    }

    public void PlayAudio(AUDIO_NAME name)
    {
      AudioClip clip = Resources.Load($"Sounds/{name.GetStringValue()}") as AudioClip;
      //List<AudioClip> list = this._mansAudioClips.Concat(this._seAudioClips).Concat(this._voicesClips).ToList();
      //this._audio.PlayOneShot(list.Find(_ => _.name == name.GetStringValue()));
      this._audio.PlayOneShot(clip);
    }
  }
}
