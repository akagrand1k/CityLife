using System;
using System.Collections.Generic;

namespace CityLife.Authenticate.Extends.OperationResult
{
    public class OperationDetails
    {

        List<string> error;
        List<string> state;
        public OperationDetails()
        {
            error = new List<string>();
            state = new List<string>();
        }

        public int CountErrors { get { return error.Count; } }
        public int CountState { get { return error.Count; } }

        public void AddError(string value)
        {
            error.Add(value);
        }

        public void AddState(string value)
        {
            state.Add(value);
            
        }


        public IEnumerable<string> GetErrors()
        {
            foreach (var item in error)
            {
                yield return item;
            }
        }

        public IEnumerable<string> GetStates()
        {
            foreach (var item in state)
            {
                yield return item;
            }
        }


        public void Clear()
        {
            error.Clear();
            state.Clear();
        }

      
    }
}
