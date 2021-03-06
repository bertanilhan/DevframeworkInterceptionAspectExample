﻿using System;
using System.Linq;
using Castle.DynamicProxy;
using FluentValidation;
using Notsis.Core.CrossCuttingConcerns.Validation.FluentValidation;
using Notsis.Core.Utilities.Interceptors;

namespace Notsis.Core.Aspects.Autofac.Validation
{
    public class ValidationInterception:MethodInterception
    {
        private readonly Type _validationType;

        public ValidationInterception(Type validatortype)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatortype))
            {
                throw new Exception("Wrong Validation Type");
            }

            _validationType = validatortype;
        }

        protected override void OnBefore(IInvocation invocation)
        {

            //var validator =(IValidator) ServiceTool.ServiceProvider.GetService(_validationType);
            var validator = (IValidator) Activator.CreateInstance(_validationType);
            var entityType = _validationType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
