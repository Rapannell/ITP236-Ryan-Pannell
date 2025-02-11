namespace State_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //List portion
            State newState = new State("CA", "California", "Sacramento", 39538223);
            List<State> stateList = State.StateList;
            stateList.Add(newState);
            Console.WriteLine("States in the list:");
            foreach (var state in stateList)
            {
                Console.WriteLine($"Code: {state.Code}, Name: {state.Name}, Capitol: {state.Capitol}, Population: {state.Population}");
            }
            Console.WriteLine("");
            stateList.Remove(newState);

            //State dictionary portion
            SortedDictionary<string, string> statesDictionary = State.StatesDictionary;
            statesDictionary[newState.Code] = newState.Name;
            Console.WriteLine("States in the dictionary:");
            foreach (var kvp in statesDictionary)
            {
                Console.WriteLine($"Code: {kvp.Key}, Name: {kvp.Value}");
            }
            Console.WriteLine("");
            statesDictionary.Remove(newState.Code);

            //Sorted list portion
            SortedList<string, State> sortedStates = State.SortedStates;
            sortedStates[newState.Code] = newState;
            Console.WriteLine("States in the sorted list:");
            foreach (var kvp in sortedStates)
            { 
                Console.WriteLine($"Code: {kvp.Key}, Name: {kvp.Value.Name}, Capitol: {kvp.Value.Capitol}, Population: {kvp.Value.Population}");
            }
            Console.WriteLine("");
            statesDictionary.Remove(newState.Code);

            //State populations portion
            List<int> statePops = State.StatePops;
            statePops.Add(newState.Population);
            int totalPopulation = statePops.Sum();
            Console.WriteLine($"Total population of all states: {totalPopulation}");

            Console.ReadKey();
        }
    }
}
