using System;
using MasterData.Core.Domain.Inheritance;

namespace MasterData.Core.Domain.MasterData
{
    public abstract class Product : MasterDataObject
    {
        public virtual string Level { get { return GetUnproxiedType().Name; } }
        public abstract Product Parent { get; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual Inheritable<string> GetName()
        {
            return Inherit(p => p.Name);
        }

        public virtual Inheritable<string> GetDescription()
        {
            return Inherit(p => p.Description);
        }

        private Inheritable<T> Inherit<T>(Func<Product, T> property)
        {
            return new Inheritable<T>(Level, property(this), () => FromParent(property));
        }

        private Inheritable<T> FromParent<T>(Func<Product, T> property)
        {
            return Parent == null ? null : Parent.Inherit(property);
        }
    }
}