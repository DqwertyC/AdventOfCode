using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2015
{

    class Day12 : ASolution
    {
        int SumOne;
        int SumTwo;


        public Day12() : base(12, 2015, "")
        {
            var jsonObj = JsonConvert.DeserializeObject<JToken>(Input);

            SumOne = AddInts(Input);
            SumTwo = ProcessToken(jsonObj);
        }

        protected override string SolvePartOne()
        {
            return SumOne.ToString();
        }

        protected override string SolvePartTwo()
        {
            return SumTwo.ToString();
        }

        private int AddInts(string s)
        {
            int sum = 0;
            int index = 0;

            while (index < s.Length)
            {
                while (index < s.Length && !IsNumber(s[index]))
                {
                    index++;
                }

                if (index < s.Length)
                {
                    int length = 1;

                    while (IsNumber(s[index + length])) length++;

                    sum += Int32.Parse((s.Substring(index, length)));

                    index += length;
                }
            }

            return sum;
        }

        private static bool IsNumber(char c)
        {
            return c == '-' || (c >= '0' && c <= '9');
        }

        private static int ProcessToken(JToken token)
        {
            switch (token)
            {
                case JArray array:
                    return ProcessArray(array);
                case JObject jObject:
                    return ProcessObject(jObject);
                case JValue value:
                    return ProcessValue(value);
            }

            return 0;
        }

        private static int ProcessArray(JArray array)
        {
            return array.Children().Sum(ProcessToken);
        }

        private static int ProcessValue(JValue value)
        {
            return int.TryParse(value.Value.ToString(), out var elem) ? elem : 0;
        }

        private static int ProcessObject(JObject jObject)
        {
            foreach (var jProperty in jObject.Properties())
            {
                if (!(jProperty.Value is JValue jValue))
                    continue;

                if (jValue.Value.ToString() == "red")
                    return 0;
            }

            return jObject.Properties().Sum(jProperty => ProcessToken(jProperty.Value));
        }
    }
}
