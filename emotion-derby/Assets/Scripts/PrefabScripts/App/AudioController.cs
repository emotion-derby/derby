using System.Collections.Generic;
using UnityEngine;
using Common;
using System.Linq;

namespace Prefabs.App
{
  public class AudioController : Common.SingletonMonoBehaviour<AudioController>
  {
    [SerializeField] private List<AudioClip> _mansAudioClips;
    [SerializeField] private List<AudioClip> _seAudioClips;
    [SerializeField] private List<AudioClip> _voicesClips;

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
      this._audio.PlayOneShot(_mansAudioClips[Random.Range(0, 3)]);
    }

    public void PlayAudio(AUDIO_NAME name)
    {
      List<AudioClip> list = this._mansAudioClips.Concat(this._seAudioClips).Concat(this._voicesClips).ToList();
      this._audio.PlayOneShot(list.Find(_ => _.name == name.GetStringValue()));
    }
  }
}
