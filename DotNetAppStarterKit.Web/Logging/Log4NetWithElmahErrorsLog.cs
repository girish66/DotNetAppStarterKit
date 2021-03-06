﻿// /*
// Copyright (c) 2013 Andrew Newton (http://www.nootn.com.au)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// */

using System;
using System.Web;
using Elmah;
using ApplicationException = Elmah.ApplicationException;

namespace DotNetAppStarterKit.Web.Logging
{
    /// <summary>
    ///     A log that writes to the log4net infrastructure.
    /// </summary>
    /// <typeparam name="T"> The type of the client of the log. </typeparam>
    public class Log4NetWithElmahErrorsLog<T> : Log4NetLog<T>
    {
        protected readonly HttpContextBase Context;

        public Log4NetWithElmahErrorsLog(HttpContextBase context)
        {
            Context = context;
        }

        public override void Error(Func<string> message)
        {
            base.Error(message);
            ErrorLog.GetDefault(Context.ApplicationInstance.Context).Log(new Error(new ApplicationException(message())));
        }

        public override void Error(string message, Exception exception)
        {
            base.Error(message, exception);
            ErrorLog.GetDefault(Context.ApplicationInstance.Context).Log(new Error(new ApplicationException(message, exception)));
        }

        public override void Error(string message, params object[] formatArgs)
        {
            base.Error(message, formatArgs);
            ErrorLog.GetDefault(Context.ApplicationInstance.Context).Log(
                new Error(new ApplicationException(string.Format(message, formatArgs))));
        }
    }
}