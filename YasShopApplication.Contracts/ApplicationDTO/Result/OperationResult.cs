namespace YasShop.Application.Contracts.ApplicationDTO.Result
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public OperationResult Succeeded()
        {
            return Succeeded("Operation Was Succeeded");
        }

        public OperationResult Succeeded(string _Message)
        {
            return Succeeded(0, "Operation Was Succeeded");
        }

        public OperationResult Succeeded(int _Code, string _Message)
        {
            IsSuccess = true;
            Message = _Message;
            Code = _Code;
            return this;
        }

        public OperationResult Failed(string _Message)
        {
            return Failed(0, _Message);
        }

        public OperationResult Failed(int _Code, string _Message)
        {
            IsSuccess = false;
            Message = _Message;
            Code = _Code;
            return this;
        }
    }
    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }

        public OperationResult<T> Succeeded(T _Data)
        {
            return Succeeded("Operation Was Succeeded", Data);
        }
        public OperationResult<T> Succeeded(string _Message, T _Data)
        {
            return Succeeded(0, _Message, _Data);
        }
        public OperationResult<T> Succeeded(int _Code, string _Message, T _Data)
        {
            Data = _Data;
            Message = _Message;
            IsSuccess = true;
            Code = _Code;

            return this;
        }
        public OperationResult<T> Failed(string _Message)
        {
            return Failed(0, _Message);
        }
        public OperationResult<T> Failed(int _Code, string _Message)
        {
            IsSuccess = false;
            Message = _Message;
            Code = _Code;
            return this;
        }
    }
}