namespace CarReader.Application.Common
{
    public class DataSource<T>
        where T : class
    {
        public IReadOnlyCollection<T> Data { get; } = new List<T>();

        public string ErrorMessage { get; } = string.Empty;

        public bool IsOk => Data != null && string.IsNullOrWhiteSpace(ErrorMessage);

        private DataSource(IReadOnlyCollection<T> data)
        {
            this.Data = data;
        }

        private DataSource(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public static DataSource<T> CreateSuccess(IReadOnlyCollection<T> data) => new DataSource<T>(data);

        public static DataSource<T> CreateError(string errorMessage) => new DataSource<T>(errorMessage);
    }
}
