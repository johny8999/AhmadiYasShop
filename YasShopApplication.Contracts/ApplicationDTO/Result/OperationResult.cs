using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YasShop.Application.Contracts.ApplicationDTO.Result
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public OperationResult Successed()
        {
            IsSuccess = true;
            Message = "Operation Was Successed";
            return this;
        }

        public OperationResult Successed(string _Message)
        {
            IsSuccess = true;
            Message = _Message;
            return this;
        }

        public OperationResult Successed(int _Code, string _Message)
        {
            IsSuccess = true;
            Message = _Message;
            Code = _Code;
            return this;
        }

        public OperationResult Failed(string _Message)
        {
            IsSuccess = false;
            Message = _Message;
            return this;
        }

        public OperationResult Failed(int _Code, string _Message)
        {
            IsSuccess = false;
            Message = _Message;
            Code = _Code;
            return this;
        }

    }
}
