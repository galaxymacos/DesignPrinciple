using System.Collections.Generic;

namespace DependencyInversionPrinciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    public class Relationships
    {
        private List<(Person,Relationship,Person)> _relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent,Relationship.Parent,child));
            _relations.Add((child,Relationship.Child,parent));
        }
    }
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Chris"};
            var child2 = new Person {Name = "Mary"};
            var relationship = new Relationships();
            relationship.AddParentAndChild(parent,child1);
            relationship.AddParentAndChild(parent,child2);
        }
    }
}