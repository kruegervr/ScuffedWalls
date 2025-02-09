﻿using ModChart;
using System;
using System.Text.Json;

namespace ScuffedWalls.Functions
{
    [ScuffedFunction("PointDefinition")] 
    class PointDefinition : SFunction
    {
        public void Run()
        {
            string name = GetParam("name", "unimplemented_pointdefinition", p => p);
            object[][] points = GetParam("points", null, p => JsonSerializer.Deserialize<object[][]>($"[{p}]"));
            
            InstanceWorkspace.PointDefinitions.Add(new BeatMap.CustomData.PointDefinition()
            {
                _name = name,
                _points = points
            });

            ConsoleOut("PointDefinition", 1, Time, "PointDefinition");
        }
    }
    [ScuffedFunction("AnimateTrack")]
    class CustomEventAnimateTrack : SFunction
    {
        public Variable Repeat;
        public Variable Beat;
        public void SetParameters()
        {
            Repeat = new Variable { Name = "repeat", Data = "1" };
            Beat = new Variable { Name = "time", Data = Time.ToString() };
            Parameters = Parameters.AddVariables(new Variable[] { Repeat, Beat });
        }
        public void Run()
        {
            SetParameters();
            int repeatcount = GetParam("repeat", 1, p => int.Parse(p));
            float repeatTime = GetParam("repeataddtime", 0, p => float.Parse(p));
            for (float i = 0; i < repeatcount; i++)
            {
                InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
                {
                    _time = Time + (i * repeatTime),
                    _type = "AnimateTrack",
                    _data = Parameters.CustomEventsDataParse()
                });
                Repeat.Data = i.ToString();
                Beat.Data = (Time + (i * repeatTime)).ToString();
            }
            ConsoleOut("AnimateTrack", repeatcount, Time, "CustomEvent");
        }
    }
    [ScuffedFunction("AssignPathAnimation")]
    class CustomEventAssignpath : SFunction
    {
        public Variable Repeat;
        public Variable Beat;
        public void SetParameters()
        {
            Repeat = new Variable { Name = "repeat", Data = "1" };
            Beat = new Variable { Name = "time", Data = Time.ToString() };
            Parameters = Parameters.AddVariables(new Variable[] { Repeat, Beat });
        }
        public void Run()
        {
            SetParameters();
            int repeatcount = GetParam("repeat", 1, p => int.Parse(p));
            float repeatTime = GetParam("repeataddtime", 0, p => float.Parse(p));
            for (float i = 0; i < repeatcount; i++)
            {
                InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
                {
                    _time = Time + (i * repeatTime),
                    _type = "AssignPathAnimation",
                    _data = Parameters.CustomEventsDataParse()
                });
                Repeat.Data = i.ToString();
                Beat.Data = (Time + (i * repeatTime)).ToString();
            }
            ConsoleOut("AssignPathAnimation", repeatcount, Time, "CustomEvent");
        }
    }
    [ScuffedFunction("AssignPlayerToTrack")]
    public class CustomEventPlayerTrack : SFunction
    {
        public void Run()
        {
            InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
            {
                _time = Time,
                _type = "AssignPlayerToTrack",
                _data = Parameters.CustomEventsDataParse()
            });
            ConsoleOut("AssignPlayerToTrack", 1, Time, "CustomEvent");
        }
    }
    
    [ScuffedFunction("ParentTrack")]
    public class CustomEventParent : SFunction
    {
        public void Run()
        {
            InstanceWorkspace.CustomEvents.Add(new BeatMap.CustomData.CustomEvents()
            {
                _time = Time,
                _type = "AssignTrackParent",
                _data = Parameters.CustomEventsDataParse()
            });
            ConsoleOut("AssignTrackParent", 1, Time, "CustomEvent");
        }
    }


    
}
