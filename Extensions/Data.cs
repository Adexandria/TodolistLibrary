namespace TasksLibrary.Extensions
{
    public class Data
    {
        public Data()
        {
        }
    }
    public class Data<T> : Data
    {
        public Data(T data)
        {
            Datas = data;
        }
        public T Datas { get; set; }
    }
}
