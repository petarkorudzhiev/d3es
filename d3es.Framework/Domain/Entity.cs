using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d3es.Framework.Domain
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Guid Id { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Entity);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public bool Equals(Entity other)
        {
            if (ReferenceEquals(null, other))
                return false;

            return other.Id.Equals(this.Id);
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(null, left) && ReferenceEquals(null, right)) return true;
            if (ReferenceEquals(null, left))
                return false;
            else
                return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
