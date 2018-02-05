namespace Lifestyle.Planning.Domain
{
    using System;
    using Lifestyle.Shared;

    public abstract class Name : Primitive<string>
    {
        protected Name(string name) : base(name)
        {
            var maxLength = 100;

            if (name.Length == 0)
                throw new ArgumentException("Name can't be empty.");

            if (name.Length > maxLength)
                throw new ArgumentException($"Name length exceeds {maxLength} characters.");
        }
    }
}
