﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AdventOfCode2021
{
    public static class InputHelper
    {
        public static IList<string> ReadOutEachLine(string fileNameWithOutTxt)
        {
            // make sure to set the file to be content
            // https://stackoverflow.com/questions/13762338/read-files-from-a-folder-present-in-project
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"Inputs\\{fileNameWithOutTxt}.txt");
            var data = File.ReadAllLines(path);
            return data;
        }

        public static bool TryGetInt(this IDictionary<string, string> self, string keyToFind, out int parsedValue)
        {
            if (self.TryGetValue(keyToFind, out var valueAsString) && int.TryParse(valueAsString, out var valueAsint))
            {
                parsedValue = valueAsint;
                return true;
            }
            parsedValue = 0;
            return false;
        }

    }
}
