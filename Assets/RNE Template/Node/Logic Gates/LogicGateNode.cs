using RNE.Template.Node.Pointer;
using RNE.Template.Node.Pointer.Value;

namespace RNE.Template.Node
{
    public class LogicGateNode : RuntimeNodeEditor.Node.Node
    {
        public readonly string[] Gates =
        {
            "AND",
            "OR",
            "NAND",
            "NOR",
            "XOR",
            "XNOR",
        };

        protected override void CodeToExecute()
        {
            ExecuteInputConnection(0);
            ExecuteInputConnection(1);

            bool a = false;
            if (Inputs[0].ConnectedOutputPointer)
            {
                a = PointerValue.GetBool(Inputs[0]);
                Elements.SetBoolean(Elements.buttons[0], a);
            }
            else
            {
                a = Elements.buttons[0].isOn;
            }

            bool b = false;
            if (Inputs[1].ConnectedOutputPointer)
            {
                b = PointerValue.GetBool(Inputs[1]);
                Elements.SetBoolean(Elements.buttons[1], b);
            }
            else
            {
                b = Elements.buttons[1].isOn;
            }

            bool result = CalcualteGate(a, b);

            Outputs[0].GetComponent<BoolOutputPointer>().Value = result;
            Elements.SetBoolean(Elements.buttons[2], result);
        }

        private bool CalcualteGate(bool a, bool b)
        {
            return Elements.dropdowns[0].value switch
            {
                0 => AND(a, b),
                1 => OR(a, b),
                2 => NAND(a, b),
                3 => NOR(a, b),
                4 => XOR(a, b),
                5 => XNOR(a, b),
                _ => false
            };
        }

        protected override void CodeToReset()
        {
            Outputs[0].GetComponent<BoolOutputPointer>().Reset();
        }

        private bool AND(bool a, bool b) => a && b;
        private bool OR(bool a, bool b) => a || b;
        private bool NAND(bool a, bool b) => !(a && b);
        private bool NOR(bool a, bool b) => !(a || b);
        private bool XOR(bool a, bool b) => a != b;
        private bool XNOR(bool a, bool b) => a == b;
    }
}
