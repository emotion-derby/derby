using System.Collections.Generic;
using UnityEngine;
using Common;
using Cysharp.Threading.Tasks;

namespace Prefabs.App
{
  public class AudioController : Common.SingletonMonoBehaviour<AudioController>
  {
    private const string SOUND_PATH_ROOT = "Sounds/";
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
      this.PlayOneShotAudio(voices[Random.Range(0, 3)]).Forget();
    }

    public async UniTask PlayOneShotAudio(AUDIO_NAME name)
    {
      AudioClip clip = Resources.Load<AudioClip>($"{SOUND_PATH_ROOT}{name.GetStringValue()}");
      this._audio.PlayOneShot(clip);
      await UniTask.WaitWhile(() =>
      {
        return this._audio.isPlaying;
      });
    }

    public async UniTask PlayAudio(AUDIO_NAME name)
    {
      AudioClip clip = Resources.Load<AudioClip>($"{SOUND_PATH_ROOT}{name.GetStringValue()}");
      this._audio.clip = clip;
      this._audio.Play();
      await UniTask.WaitWhile(() =>
      {
        return this._audio.isPlaying;
      });
    }
  }
}
