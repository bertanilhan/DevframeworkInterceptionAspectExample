﻿using System;
using Castle.DynamicProxy;

namespace Notsis.Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation)
        {
        }
        protected virtual void OnAfter(IInvocation invocation)
        {
        }
        protected virtual void OnException(IInvocation invocation)
        {
        }
        protected virtual void OnSuccess(IInvocation invocation)
        {
        }
        public override void Intercept(IInvocation invocation)
        {
            var isSucces = true;
            this.OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                isSucces = false;
                this.OnException(invocation);
                throw;
            }
            finally
            {
                if (isSucces)
                {
                    this.OnSuccess(invocation);
                }
            }
            this.OnAfter(invocation);
        }
    }
}