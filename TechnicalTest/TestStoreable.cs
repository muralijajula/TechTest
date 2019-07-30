using System;

namespace Interview
{
    public class TestStoreable : IStoreable
    {
        public string Name { get; set; }
        public IComparable Id { get; set; }
    }
}