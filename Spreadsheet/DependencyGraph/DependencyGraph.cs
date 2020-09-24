// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)
// Completed by @author Camille van Ginkel

using System.Collections.Generic;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        Dictionary<string, HashSet<string>> dependents;  //Maps each node as a key to its set of dependents
        Dictionary<string, HashSet<string>> dependees;   //Maps each node as a key to its set of dependees
        int size;  //tracks the number of dependency's added to this object


        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            dependents = new Dictionary<string, HashSet<string>>();
            dependees = new Dictionary<string, HashSet<string>>();
            size = 0; // number of ordered pairss

        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return size; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            /*
             * From Discussion Alternative Way to Implement Method:
             * 
             * get {
             *          return GetDependees(s).Count(); //O(N) --because Count() is a method, not necesarily better
             * }
             */
            
            get {
                if (this.dependees.ContainsKey(s))
                {
                    return dependees[s].Count;  //O(1)
                }
                return 0;
                }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            if (dependents.ContainsKey(s))
            {
                return dependents[s].Count > 0;
            }
            return false;
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            if (dependees.ContainsKey(s))
            {
                return dependees[s].Count > 0;
            }
            return false;
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependents.ContainsKey(s))
            {
                return dependents[s];
            }
            else
            {
                return new HashSet<string>();
            }
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependees.ContainsKey(s))
            {
                return dependees[s];
            }
            else
            {
                return new HashSet<string>();
            }
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            if (dependents.ContainsKey(s) && dependees.ContainsKey(s) && dependents.ContainsKey(t) && dependees.ContainsKey(t))
            {  //if both nodes already exist

                if (!dependents[s].Contains(t))  //if t dependss on it, than the dependency already exisits
                {
                    dependents[s].Add(t);
                    dependees[t].Add(s);
                    size++;
                }
            }
            //if node s already exists, but node t does not
            else if (dependents.ContainsKey(s) && dependees.ContainsKey(s) && !dependents.ContainsKey(t) && !dependees.ContainsKey(t))
            {
                dependents[s].Add(t);

                HashSet<string> tdependees = new HashSet<string>();
                tdependees.Add(s);
                dependees.Add(t, tdependees);
                HashSet<string> tdependents = new HashSet<string>();
                dependents.Add(t, tdependents);
                size++;
            }
            //if node t already exists, but node s does not
            else if (!dependents.ContainsKey(s) && !dependees.ContainsKey(s) && dependents.ContainsKey(t) && dependees.ContainsKey(t))
            {
                dependees[t].Add(s);

                HashSet<string> sdependees = new HashSet<string>();
                dependees.Add(s, sdependees);
                HashSet<string> sdependents = new HashSet<string>();
                sdependents.Add(t);
                dependents.Add(s, sdependents);
                size++;
            }
            //if niether node s nor node t already exist
            else //if (!dependents.ContainsKey(s) && !dependees.ContainsKey(s) && !dependents.ContainsKey(t) && !dependees.ContainsKey(t))
            {

                if (s.Equals(t))
                {
                    HashSet<string> sdependents = new HashSet<string>();
                    sdependents.Add(t);
                    dependents.Add(s, sdependents);
                    HashSet<string> sdependees = new HashSet<string>();
                    sdependees.Add(t);
                    dependees.Add(s, sdependees);
                }
                else
                {
                    HashSet<string> sdependents = new HashSet<string>();
                    sdependents.Add(t);
                    dependents.Add(s, sdependents);
                    HashSet<string> sdependees = new HashSet<string>();
                    dependees.Add(s, sdependees);

                    HashSet<string> tdependents = new HashSet<string>();
                    dependents.Add(t, tdependents);
                    HashSet<string> tdependees = new HashSet<string>();
                    tdependees.Add(s);
                    dependees.Add(t, tdependees);
                }
                size++;
            }

        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            if (dependents.ContainsKey(s)  && dependents.ContainsKey(t))
            {
                dependents[s].Remove(t);
                dependees[t].Remove(s);
                size--;
            }
            
            
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            /**
             * From Discussion, Better way to Implement Method:
             * 
             * foreach (string r in GetDependents(s))
             *      RemoveDependency(s,r);   //Possible Error: Enumeration was modified, can't continue
             * foreach (string t in newDependents)
             *      AddDependency (s,t);
             */

            if (!dependents.ContainsKey(s))
            {
                dependents.Add(s, new HashSet<string>());
                dependees.Add(s, new HashSet<string>());
            }
            foreach (string t in dependents[s])  //for each node that was originally dependent of s
            {
                dependees[t].Remove(s);  //remove s from being its dependee
            }
            dependents[s].Clear(); 
            foreach (string d in newDependents)  //add each node to the node s dependent set
            {
                if (dependees.ContainsKey(d))
                {
                    dependees[d].Add(s);
                    dependents[s].Add(d);
                }
                else
                {
                    dependents[s].Add(d);
                    HashSet<string> ddependees = new HashSet<string>();
                    ddependees.Add(s);
                    dependees.Add(d, ddependees);
                    HashSet<string> ddependents = new HashSet<string>();
                    dependents.Add(d, ddependents);
                }

            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            if (!dependees.ContainsKey(s))
            {
                dependees.Add(s, new HashSet<string>());
                dependents.Add(s, new HashSet<string>());
                size++;
            }
            
            foreach (string t in dependees[s])
            {
                dependents[t].Remove(s);
            }
            dependees[s].Clear();
            foreach( string d in newDependees)
            {
                if (dependees.ContainsKey(d))
                {
                    dependees[s].Add(d);
                    dependents[d].Add(s);
                }
                else
                {
                    HashSet<string> ddependees = new HashSet<string>();
                    dependees.Add(d, ddependees);
                    HashSet<string> ddependents = new HashSet<string>();
                    ddependents.Add(s);
                    dependents.Add(d, ddependents);
                    dependees[s].Add(d);
                }
            }
        }

    }

}
