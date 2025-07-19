using RuntimeNodeEditor.Node.Connection.Lines;
using RuntimeNodeEditor.Node.Serialization;
using RuntimeNodeEditor.Node;
using RuntimeNodeEditor.Input;
using RuntimeNodeEditor.UI.Canvas.Node.Factory;
using RuntimeNodeEditor.UI.Canvas.Node.Factory.ProjectData;
using UnityEngine;
using System;
using System.IO;

namespace RNE.Template.Save
{
    internal partial class SaveSystem
    {
        [SerializeField]
        private NodesList _nodesList;

        [SerializeField]
        private ConnectionLines nodeConnections;

        [SerializeField]
        private NodeExecution nodeExecution;

        [SerializeField]
        private RuntimeNodeEditor.UI.Tooltip.Window.Window onOpenWindow;

        public void Load()
        {
            Node[] nodes = nodesObject.GetComponentsInChildren<Node>();

            if (nodes.Length > 0)
            {
                onOpenWindow.Create();

                return;
            }

            string path = _iOSelection.SelectSingleFile(SaveExtension);

            if (path == null)
            {
                Debug.LogWarning("Save file was not opened!");

                return;
            }

            _saveDirectory = path;

            int position = 0;
            byte[] data = File.ReadAllBytes(_saveDirectory);

            LoadZoom(ref position, data);
            LoadPan(ref position, data);

            LoadNodes(ref position, data);
            nodes = nodesObject.GetComponentsInChildren<Node>();

            if (nodes.Length == 0)
                return;

            LoadConnections(ref position, nodes, data);

            nodeExecution.Execute(nodes);
        }

        private void LoadZoom(ref int position, byte[] data)
        {
            Zoom.Load(BitConverter.ToSingle(data, position));
            position += 4;
        }

        private void LoadPan(ref int position, byte[] data)
        {
            Pan.LoadWorldPan(
                BitConverter.ToSingle(data, position), 
                BitConverter.ToSingle(data, position + 4));
            position += 8;
            
            Pan.LoadViewportPan(
                BitConverter.ToSingle(data, position), 
                BitConverter.ToSingle(data, position + 4));
            position += 8;
        }

        private void LoadNodes(ref int position, byte[] data)
        {
            int numOfNodes = BitConverter.ToInt32(data, position);
            position += 4;

            if (numOfNodes == 0)
                return;

            for (int i = 0; i < numOfNodes; i++)
            {
                LoadNode(ref position, data);
            }
        }

        private void LoadNode(ref int position, byte[] data)
        {
            int length = data[position];
            position++;

            if (length > 1)
            {
                NodesGroup group = _nodesList.NodesGroup;

                for (int i = 0; i < length - 1; i++)
                {
                    group = _nodesList.NodesGroup.GetGroup(data[position]);
                    position++;
                }

                GameObject nodeObject = Instantiate(group.GetNode(data[position]), nodesObject.transform);
                position++;

                nodeObject.GetComponent<RectTransform>().localPosition = 
                    new Vector3(
                        BitConverter.ToSingle(data, position), 
                        BitConverter.ToSingle(data, position + 4), 
                        0);
                position += 8;

                OnLoad.Load(
                    nodeObject.GetComponent<Node>(),
                    new LoadData(data, ref position)
                    );
            }
        }

        private void LoadConnections(ref int position, Node[] nodes, byte[] data)
        {
            int connectionArrayLength = BitConverter.ToInt32(data, position);
            position += 4;

            for (int i = 0; i < connectionArrayLength; i++)
            {
                LoadNodeConnection(ref position, nodes, data);
            }
            nodeConnections.UpdateLinesOnLoad();
        }

        private void LoadNodeConnection(ref int position, Node[] nodes, byte[] data)
        {
            nodeConnections.Load(
                nodes[BitConverter.ToInt32(data, position)].Inputs[data[position + 4]],
                nodes[BitConverter.ToInt32(data, position + 5)].Outputs[data[position + 9]]);

            position += 10;
        }
    }
}
