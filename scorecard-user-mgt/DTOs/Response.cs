﻿using System.Net;

namespace scorecard_user_mgt.DTOs
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
    }
}
