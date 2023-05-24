using FluentNHibernate.Mapping;
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
