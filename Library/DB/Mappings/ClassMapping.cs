using FluentNHibernate.Mapping;
using TasksLibrary.Models;

namespace TasksLibrary.DB.Mappings
{
    public class ClassMapping<T> : ClassMap<T> where T : IEntity
    {
        public ClassMapping()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
        }
    }
}
