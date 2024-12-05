using System;
using TMPro;
using UnityEngine;

namespace Utils.IO.Selection
{
    [Serializable]
    public class SelectFolder : IOSelection
    {
        [SerializeField]
        private TMP_Text text;

        public void Select()
        {
            text.text = SelectFolder();
        }
    }
}