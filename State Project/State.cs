using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace State_Project
{
   
    #region States
    public class State
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Capitol { get; set; }
        public int Population { get; set; }
        public State(string code, string name, string capitol, int population)
        {
            Code = code;
            Name = name;
            Capitol = capitol;
            Population = population;
        }
        private static State[] states = {
            new State("VA", "Virginia", "Richmond", 8535519),
            new State("AL", "Alabama", "Montgomery", 4903185),
            new State("MS", "Mississippi", "Jackson", 2976149),
            new State("IL", "Illinois", "Springfield", 12671821),
            new State("TX", "Texas", "Austin", 28995881),
            new State("NC", "North Carolina", "Raleigh", 10488084),
            new State("UT", "Utah", "Salt Lake City", 3205958),
            new State("PA", "Pennsylvania", "Harrisburg", 12801989)
        };
        public static State[] States
        {
            get
            {
                return states;
            }
        }
        /// <summary>
        /// Return a Dictionary of State Names index by the State Code
        /// </summary>
        public static List<State> StateList
        {
            get
            {
                return new List<State>(states);
            }
        }
        public static SortedDictionary<string, string> StatesDictionary
        {
            get
            {
                var dictionary = new SortedDictionary<string, string>();
                foreach (var state in states)
                {
                    dictionary[state.Code] = state.Name;
                }
                return dictionary;
            }
        }
        public static SortedList<string, State> SortedStates
        {
            get
            {
                var sortedList = new SortedList<string, State>();
                foreach (var state in states)
                {
                    sortedList[state.Code] = state;
                }
                return sortedList;
            }
        }
        public static List<int> StatePops
        {
            get
            {
                var populations = new List<int>();
                foreach (var state in states)
                { 
                    populations.Add(state.Population);
                }
                return populations;
            }
        }
        public static int[] Populations
        {
            get
            {
                int len = states.Length;
                int[] pops = new int[len];
                for (int i = 0; i < len; i++)
                {
                    pops[i] = states[i].Population;
                }
                return pops;
            }
        }
    }
    #endregion
}
