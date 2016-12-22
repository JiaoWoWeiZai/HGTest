using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CrHgWcfService.Model
{
    [DataContract]
    public class Param
    {
        public Param(string name, string value, int row = 1)
        {
            Name = name;
            Value = value;
            Row = row;
        }



        public int Row { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}