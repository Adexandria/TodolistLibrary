using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.Models;

namespace TasksLibrary.NHibernate.Mappings
{
    public class ClassMapping<T> : ClassMap<T> where T : BaseClass
    {
        public ClassMapping()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
        }
    }
}
