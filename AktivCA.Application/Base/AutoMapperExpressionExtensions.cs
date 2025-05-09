﻿using System.Linq.Expressions;
using AutoMapper;

namespace AktivCA.Application.Base
{
    public static class AutoMapperExpressionExtensions
    {
        public static IMappingExpression<TDestination, TMember> Ignore<TDestination, TMember, TResult>(this IMappingExpression<TDestination, TMember> mappingExpression, Expression<Func<TMember, TResult>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, opts => opts.Ignore());
        }
    }
}
