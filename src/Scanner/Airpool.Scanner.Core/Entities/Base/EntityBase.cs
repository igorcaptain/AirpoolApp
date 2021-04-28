using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airpool.Scanner.Core.Entities.Base
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; protected set; }

        int? _requestedHashCode;

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityBase<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            var item = obj as EntityBase<TId>;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            return Equals(left, null) ? Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !(left == right);
        }
    }
}
