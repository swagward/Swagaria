using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TerrariaClone.Runtime.UI
{
    public class TutorialInProgress : MonoBehaviour
    {
        [SerializeField] private TMP_Text unfinished;

        public void ButtonPressed()
        {
            unfinished.color = Color.red;
            
            Invoke(nameof(HideText), 3f);
        }

        private void HideText()
        {
            unfinished.color = Color.clear;
        }
    }
}
