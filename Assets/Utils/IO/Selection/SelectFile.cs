using System;
using SFB;
using TMPro;
using UnityEngine;

namespace Utils.IO.Selection
{
    [Serializable]
    public class SelectFile : IOSelection
    {
        [SerializeField]
        private TMP_Text text;

        public void Select()
        {
            ExtensionFilter[] filters = {
                new("All", "*"),
                new("Text", "txt")
            };

            text.text = SelectFile(filters);
        }
    }
}
