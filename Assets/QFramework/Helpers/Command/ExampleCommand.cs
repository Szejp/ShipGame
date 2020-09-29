using System;

namespace QFramework.Helpers.Command
{
    [Serializable]
    public abstract class Command : ICommand
    {
        public abstract void Fire();
    }
}