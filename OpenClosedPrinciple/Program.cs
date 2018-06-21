using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenClosedPrinciple
{
    enum Color
    {
        Blue,Yellow,Green
    }

    enum Size
    {
        Small,Medium,Large
    }
    
    class Product
    {
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }

        public Color Color { get; set; }
        public string Name { get; set; }
        public Size Size { get; set; }
    }
    
    class ColorSpecification:ISpecification<Product>
    {
        private readonly Color _color;

        public ColorSpecification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }
    
    class SizeSpecification:ISpecification<Product>
    {
        private readonly Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == _size;
        }
    }
    
    class AndSpecification<T>:ISpecification<T>
    {
        private readonly List<ISpecification<T>> _specifications;

        public AndSpecification(params ISpecification<T>[] specifications)
        {
            _specifications = specifications.ToList();
        }
        public bool IsSatisfied(T t)
        {
            foreach (ISpecification<T> specification in _specifications)
            {
                if (!specification.IsSatisfied(t))
                    return false;
            }

            return true;
        }
    }


    interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    
    interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specs);
    }
    
    class ProductFilter:IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specs)
        {
            foreach (Product product in items)
            {
                if (specs.IsSatisfied(product))
                    yield return product;
            }
        }
    }

    internal class Program
    
    {
        public static void Main(string[] args)
        {
            Product house = new Product("Tree",Color.Green,Size.Large);
            Product tree = new Product("Tree",Color.Green,Size.Medium);
            Product mouse = new Product("Mouse",Color.Yellow,Size.Small);
            Product ball = new Product("ball",Color.Yellow,Size.Large);
            List<Product> products = new List<Product>
            {
                house,
                tree,
                mouse,
                ball
            };
            var specYellow = new ColorSpecification(Color.Yellow);
            var specLarge = new SizeSpecification(Size.Small);
            var specMultiple = new AndSpecification<Product>(new ColorSpecification(Color.Yellow),specLarge);
            ProductFilter productFilter = new ProductFilter();
            foreach (Product yellowItem in productFilter.Filter(products, specMultiple))
            {
                Console.WriteLine(yellowItem.Name);
            }
        }
    }
}