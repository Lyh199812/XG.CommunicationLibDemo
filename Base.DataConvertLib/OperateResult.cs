using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DataConvertLib
{
    public class OperateResult<T> : OperateResult
    {
        public T Content { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public OperateResult(string message)
            : base(message)
        {
        }

        public OperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message, T content)
            : base(isSuccess, errorCode, message)
        {
            Content = content;
        }
    }
    public class OperateResult<T1, T2> : OperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public OperateResult(string message)
            : base(message)
        {
        }

        public OperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
        }
    }
    public class OperateResult<T1, T2, T3> : OperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public OperateResult(string message)
            : base(message)
        {
        }

        public OperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
        }
    }
    public class OperateResult<T1, T2, T3, T4> : OperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public T4 Content4 { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public OperateResult(string message)
            : base(message)
        {
        }

        public OperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
            Content4 = content4;
        }
    }
    public class OperateResult<T1, T2, T3, T4, T5> : OperateResult
    {
        public T1 Content1 { get; set; }

        public T2 Content2 { get; set; }

        public T3 Content3 { get; set; }

        public T4 Content4 { get; set; }

        public T5 Content5 { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
            : base(isSuccess)
        {
        }

        public OperateResult(string message)
            : base(message)
        {
        }

        public OperateResult(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
            : base(isSuccess, errorCode, message)
        {
        }

        public OperateResult(bool isSuccess, int errorCode, string message, T1 content1, T2 content2, T3 content3, T4 content4, T5 content5)
            : base(isSuccess, errorCode, message)
        {
            Content1 = content1;
            Content2 = content2;
            Content3 = content3;
            Content4 = content4;
            Content5 = content5;
        }
    }
    [Description("操作结果类")]
    public class OperateResult
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = "UnKnownError";


        public int ErrorCode { get; set; } = 99999;


        public OperateResult()
        {
        }

        public OperateResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public OperateResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public OperateResult(bool isSuccess, int errorCode, string message)
        {
            IsSuccess = isSuccess;
            ErrorCode = errorCode;
            Message = message;
        }

        public OperateResult(string message)
        {
            Message = message;
        }

        public OperateResult(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public static OperateResult CreateSuccessResult()
        {
            return new OperateResult
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success"
            };
        }

        public static OperateResult CreateFailResult(string message)
        {
            return new OperateResult
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult CreateFailResult()
        {
            return new OperateResult
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = "UnKnownError"
            };
        }

        public static OperateResult<T> CreateSuccessResult<T>(T value)
        {
            return new OperateResult<T>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content = value
            };
        }

        public static OperateResult<T> CreateFailResult<T>(OperateResult result)
        {
            return new OperateResult<T>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T> CreateFailResult<T>(string message)
        {
            return new OperateResult<T>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult<T1, T2> CreateSuccessResult<T1, T2>(T1 value1, T2 value2)
        {
            return new OperateResult<T1, T2>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2
            };
        }

        public static OperateResult<T1, T2> CreateFailResult<T1, T2>(OperateResult result)
        {
            return new OperateResult<T1, T2>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2> CreateFailResult<T1, T2>(string message)
        {
            return new OperateResult<T1, T2>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult<T1, T2, T3> CreateSuccessResult<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
        {
            return new OperateResult<T1, T2, T3>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3
            };
        }

        public static OperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3> CreateFailResult<T1, T2, T3>(string message)
        {
            return new OperateResult<T1, T2, T3>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult<T1, T2, T3, T4> CreateSuccessResult<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
        {
            return new OperateResult<T1, T2, T3, T4>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4
            };
        }

        public static OperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4> CreateFailResult<T1, T2, T3, T4>(string message)
        {
            return new OperateResult<T1, T2, T3, T4>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5> CreateSuccessResult<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
        {
            return new OperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = true,
                ErrorCode = 0,
                Message = "Success",
                Content1 = value1,
                Content2 = value2,
                Content3 = value3,
                Content4 = value4,
                Content5 = value5
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(OperateResult result)
        {
            return new OperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = false,
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        }

        public static OperateResult<T1, T2, T3, T4, T5> CreateFailResult<T1, T2, T3, T4, T5>(string message)
        {
            return new OperateResult<T1, T2, T3, T4, T5>
            {
                IsSuccess = false,
                ErrorCode = 99999,
                Message = message
            };
        }

        public static OperateResult<T1> CopyOperateResult<T1, T2>(OperateResult<T2> result)
        {
            OperateResult<T1> operateResult = new OperateResult<T1>();
            operateResult.IsSuccess = result.IsSuccess;
            operateResult.ErrorCode = result.ErrorCode;
            operateResult.Message = result.Message;
            operateResult.Content = (T1)Convert.ChangeType(result.Content, typeof(T1));
            return operateResult;
        }
    }
}
