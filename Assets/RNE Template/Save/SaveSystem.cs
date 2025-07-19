using RuntimeNodeEditor.Input;
using RuntimeNodeEditor.UI.Canvas.Node.Save;
using Utils.IO;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace RNE.Template.Save
{
    internal partial class SaveSystem : MonoBehaviour
    {
        private readonly IOSelection _iOSelection = new();
        private string _saveDirectory;

        [Header("Nodes")]
        [SerializeField]
        private GameObject nodesObject;

        private const string SaveExtension = "data";
        private readonly List<byte> _data = new();

        public void Save()
        {
            if (_saveDirectory == null)
            {
                SaveAs();
            }

            WriteData();
        }

        public void SaveAs()
        {
            _saveDirectory = _iOSelection.SelectSavePath("Save", SaveExtension);

            WriteData();
        }

        private void WriteData()
        {
            _data.Clear();
            
            _data.AddRange(Zoom.Save());
            _data.AddRange(Pan.Save());
            _data.AddRange(OnSave.Save(nodesObject));

            File.WriteAllBytes(_saveDirectory, _data.ToArray());
        }
    }
}
