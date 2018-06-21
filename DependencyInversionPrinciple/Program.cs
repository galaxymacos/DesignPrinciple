using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildOf(string name);
    }

    public class Person
    {
        public string Name;
    }

    public class Relationships:IRelationshipBrowser
    {
        private List<(Person,Relationship,Person)> _relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relations.Add((parent,Relationship.Parent,child));
            _relations.Add((child,Relationship.Child,parent));
        }

        public List<(Person, Relationship, Person)> Relations => _relations;
        public IEnumerable<Person> FindAllChildOf(string name)
        {
            foreach (var child in _relations.Where(x=>x.Item1.Name == name &&x.Item2==Relationship.Parent))
            {
                yield return child.Item3;
            }
        }
    }
    
    internal class Research
    {
        // In this way, the Research class is directly dependent on the relataionships class, which means that
        // it must know how Relationships class store those data ( in this case: tuple ), In other words, the base class 
        // once the Relationships change the way it stores data,the higher module will stop working too
//        public Research(Relationships relationships)
//        {
//            var relations = relationships.Relations;
//            foreach (var child in relations.Where(x=>x.Item1.Name == "John"&&x.Item2==Relationship.Parent))
//            {
//                Console.WriteLine(child.Item3.Name);
//            }
//        }

        public Research(IRelationshipBrowser relationships)
        {
            foreach (var child in relationships.FindAllChildOf("John"))
            {
                Console.WriteLine("John has a child: "+child.Name);
            }
        }
        public static void Main(string[] args)
        {
            var parent = new Person {Name = "John"};
            var child1 = new Person {Name = "Chris"};
            var child2 = new Person {Name = "Mary"};
            var relationship = new Relationships();
            relationship.AddParentAndChild(parent,child1);
            relationship.AddParentAndChild(parent,child2);

            new Research(relationship);
        }
    }
}