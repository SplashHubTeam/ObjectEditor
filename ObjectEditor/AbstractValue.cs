using System;
using System.Reflection;

namespace ObjectEditor
{
    public class AbstractValue
    {
        /// <summary>
        /// Указатель на объект родителя, чтобы избежать рекурсивного редактирования одного и того же объекта, либо цепочки объектов.
        /// </summary>
        internal object Parent { get; set; }

        public virtual object Value { get; set; }
        public virtual Type Type { get; private set; }
        public virtual PropertyInfo Info { get { return null; } }
    }
}
