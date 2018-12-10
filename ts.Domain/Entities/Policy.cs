using System;
using System.Collections.Generic;

namespace ts.Domain.Entities
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Server> Servers { get; set; }
    }

    public class Server
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Site> Sites { get; set; }
    }

    public class Policy
    {
        public int Id { get; set; }
        public PolicyType Type { get; set; }
        public string Name { get; set; }
    }

    public enum PolicyType
    {
        NotSet = 0,
        Restart,
        Install,
        Alert
    }
}