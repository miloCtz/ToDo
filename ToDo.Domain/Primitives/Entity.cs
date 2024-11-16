namespace ToDo.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Entity()
        {            
        }

        public int Id { get; set; }

        public static bool operator ==(Entity? left, Entity? right) =>
            left is not null && right is not null && left.Equals(right);

        public static bool operator !=(Entity? left, Entity? right) =>
            !(left == right);

        public bool Equals(Entity? other)
        {
            if (other is null ||
               other.GetType() != GetType())
            {
                return false;
            }

            return other.Id == Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null ||
                obj.GetType() != GetType() ||
                obj is not Entity entity)
            {
                return false;
            }

            return entity.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode() * 30;
    }
}
