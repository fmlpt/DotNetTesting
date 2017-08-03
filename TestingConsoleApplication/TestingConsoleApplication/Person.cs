using System;
using System.Collections;
using System.Collections.Generic;

namespace TestingConsoleApplication
{
    public class Person
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Address}";
        }
    }

    public class PersonComparerByAddress : IComparer<Person>
    {
        public int Compare(Person left, Person right)
        {
            // Pos: left > right
            // Neg: left < right
            // Eq: left = right
            var leftToken = left.Address.Split(' ')[0];
            var righToken = right.Address.Split(' ')[0];

            int leftNumber;
            var isLeftNumeric = int.TryParse(leftToken, out leftNumber);

            int rightNumber;
            var isRightNumeric = int.TryParse(righToken, out rightNumber);

            if(isLeftNumeric && isRightNumeric)
            {
                return leftNumber - rightNumber;
            }

            return leftToken.CompareTo(righToken);
        }
    }
}