﻿using System;
using System.Collections.Generic;
using System.Linq;
using GuildWars2;

namespace Test
{
    static class Program
    {
        static void Main()
        {
            var watcher = new GwWatcher();

            // choose a world to watch
            watcher.World = GwWatcher.Worlds.First(w => w.Name == "Blackgate");

            // what to watch for
            watcher.NotifyFilter = GwNotifyFilter.All;

            // start watching
            watcher.EnableRaisingEvents = true;

            // how often to poll for changes
            watcher.PollFrequency = new TimeSpan(0, 0, 5);

            // called when an event's status changes
            watcher.EventStatusChanged += (sender, ev) =>
                Console.WriteLine("{0}: {1}", ev.Name, ev.Status);

            // called when the score of a WvW map changes
            watcher.WvWScoreChanged += (sender, map) =>
            {
                var redScore = map.Score[GwMatchTeam.Red];
                var blueScore = map.Score[GwMatchTeam.Blue];
                var greenScore = map.Score[GwMatchTeam.Green];
                Console.WriteLine("Score changed on {0}: R={1}, B={2}, G={3}", map.Type, redScore, blueScore, greenScore);
            };

            // called when a WvW objective changes
            watcher.WvWObjectiveChanged += (sender, objective) => 
                Console.WriteLine("{0}: {1} now belongs to {2}", objective.Map.Type, objective.Name, objective.Owner);
        }
    }
}
