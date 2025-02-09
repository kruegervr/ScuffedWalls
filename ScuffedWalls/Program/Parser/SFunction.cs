﻿using System;
using System.Linq;

namespace ScuffedWalls
{
    public class SFunction
    {
        public Workspace InstanceWorkspace;
        public Parameter[] Parameters;
        public float Time;

        public void InstantiateSFunction(Parameter[] parameters, Workspace instance, float time)
        {
            Parameters = parameters;
            InstanceWorkspace = instance;
            Time = time;
        }
        public void ConsoleOut(string Type, int Amount, float Beat, string Purpose)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string s = string.Empty;
            if (Amount != 1) s = "s";
            ScuffedLogger.ScuffedWorkspace.FunctionParser.Log($"Added {Purpose} at beat {Beat} ({Amount} {Type}{s})");
            Console.ResetColor();
        }
        public T GetParam<T>(string Name, T DefaultValue, Func<string,T> Converter)
        {
            var filteredparams = Parameters.Where(p => p.Name.ToLower() == Name.ToLower());
            if (filteredparams != null && filteredparams.Any())
            {
                return Converter(filteredparams.First().Data);
            }
            else return DefaultValue;
        }
    }



}
