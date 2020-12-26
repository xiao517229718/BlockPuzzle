using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class AudioComponet : BaseComponet
    {
        public AudioManager _audioManager;
        private void Awake()
        {
            _audioManager = (AudioManager)GameModleManager.GetGameModel(typeof(AudioManager));
        }
    }
}