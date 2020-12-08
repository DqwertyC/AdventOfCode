using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day07 : ASolution
    {

        ushort initialValue;
        ushort finalValue;

        public Day07() : base(07, 2015, "")
        {
            foreach (string line in Input.SplitByNewline())
            {
                BitWire.AddWire(line);
            }

            initialValue = BitWire.ReadValue("a");

            BitWire.RemoveWire("b");
            BitWire.AddWire("" + initialValue + " -> b");

            finalValue = BitWire.ReadValue("a");
        }

        protected override string SolvePartOne()
        {
            return initialValue.ToString();
        }

        protected override string SolvePartTwo()
        {
            return finalValue.ToString();
        }

        private class BitWire
        {
            private static Dictionary<string, BitWire> allWires;

            public string wireName;
            public ushort wireValue;

            bool evaluated = false;

            ushort leftOperand;
            ushort rightOperand;
            bool leftEvaled;
            bool rightEvaled;
            string leftParent;
            string rightParent;
            ConnectionType bitOperator;

            public static void AddWire(string definition)
            {
                new BitWire(definition);
            }

            public static ushort ReadValue(string wireName)
            {
                return allWires[wireName].evaluate();
            }

            public static void RemoveWire(string name)
            {
                allWires.Remove(name);

                foreach (var wire in allWires)
                {
                    wire.Value.evaluated = false;
                    if (wire.Value.leftParent != null) wire.Value.leftEvaled = false;
                    if (wire.Value.rightParent != null) wire.Value.rightEvaled = false;
                }
            }

            private BitWire(string definition)
            {
                if (null == allWires) allWires = new Dictionary<string, BitWire>();

                string[] splitCommand = definition.Split(' ');

                wireName = splitCommand[splitCommand.Length - 1];

                if (3 == splitCommand.Length)
                {
                    bitOperator = ConnectionType.EQUAL;

                    rightEvaled = true;

                    if (!UInt16.TryParse(splitCommand[0], out leftOperand))
                    {
                        leftParent = splitCommand[0];
                    }
                    else
                    {
                        leftEvaled = true;
                    }
                }
                else if (4 == splitCommand.Length)
                {
                    bitOperator = ConnectionType.INVERT;

                    rightEvaled = true;

                    if (!UInt16.TryParse(splitCommand[1], out leftOperand))
                    {
                        leftParent = splitCommand[1];
                    }
                    else
                    {
                        leftOperand = (ushort)~leftOperand;
                        leftEvaled = true;
                    }
                }
                else
                {
                    if (splitCommand[1].StartsWith("AND"))
                    {
                        bitOperator = ConnectionType.AND;
                    }
                    else if (splitCommand[1].StartsWith("OR"))
                    {
                        bitOperator = ConnectionType.OR;
                    }
                    else if (splitCommand[1].StartsWith("LSHIFT"))
                    {
                        bitOperator = ConnectionType.LSHIFT;
                    }
                    else if (splitCommand[1].StartsWith("RSHIFT"))
                    {
                        bitOperator = ConnectionType.RSHIFT;
                    }

                    if (!UInt16.TryParse(splitCommand[0], out leftOperand))
                    {
                        leftParent = splitCommand[0];
                    }
                    else
                    {
                        leftEvaled = true;
                    }

                    if (!UInt16.TryParse(splitCommand[2], out rightOperand))
                    {
                        rightParent = splitCommand[2];
                    }
                    else
                    {
                        rightEvaled = true;
                    }
                }

                allWires[wireName] = this;

            }

            public ushort evaluate()
            {
                if (!evaluated)
                {

                    if (!leftEvaled)
                    {
                        leftOperand = allWires[leftParent].evaluate();
                        leftEvaled = true;
                    }

                    if (!rightEvaled)
                    {
                        rightOperand = allWires[rightParent].evaluate();
                        rightEvaled = true;
                    }

                    switch (bitOperator)
                    {
                        case ConnectionType.EQUAL:
                            wireValue = leftOperand;
                            break;
                        case ConnectionType.INVERT:
                            wireValue = (ushort)(~leftOperand);
                            break;
                        case ConnectionType.LSHIFT:
                            wireValue = (ushort)(leftOperand << rightOperand);
                            break;
                        case ConnectionType.RSHIFT:
                            wireValue = (ushort)(leftOperand >> rightOperand);
                            break;
                        case ConnectionType.AND:
                            wireValue = (ushort)(leftOperand & rightOperand);
                            break;
                        case ConnectionType.OR:
                            wireValue = (ushort)(leftOperand | rightOperand);
                            break;
                    }

                    evaluated = true;
                }

                return wireValue;
            }

            enum ConnectionType
            {
                EQUAL,
                INVERT,
                LSHIFT,
                RSHIFT,
                AND,
                OR
            };
        }
    }
}
