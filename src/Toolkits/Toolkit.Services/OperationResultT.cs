namespace Toolkit.Services
{
    public class OperationResult<T> : OperationResult
        where T : class
    {
        public OperationResult()
        {
        }

        public OperationResult(T dataModel)
            : base()
        {
            Entity = dataModel;
        }

        public OperationResult(T dataModel, bool success)
            : base(success)
        {
            Entity = dataModel;
        }

        public new T Entity
        {
            get => base.Entity as T;

            set => base.Entity = value;
        }
    }
}