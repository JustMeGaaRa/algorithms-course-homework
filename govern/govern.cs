﻿using System;
using System.IO;
using System.Linq;
using Common.Algorithms;
using Common.DataStructures;

namespace govern
{
    public class Govern
    {
        private static void Main(string[] args)
        {
            var govern = new Govern();
            string inputFileName;
            string outputFileName;

            if (args == null || args.Length == 0)
            {
                inputFileName = "govern.in";
                outputFileName = "govern.out";
            }
            else if (args.Length == 2)
            {
                inputFileName = args[0];
                outputFileName = args[1];
            }
            else
            {
                Console.WriteLine("Command line parameters violation!");
                return;
            }

            govern.Run(inputFileName, outputFileName);
        }

        public void Run(string inputFileName, string outputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var graph = new Graph();
            graph.Parse(lines, true);
            var result = graph.Tarjan();
            File.WriteAllLines(outputFileName, result.Select(x => x.Label));
        }
    }
}
