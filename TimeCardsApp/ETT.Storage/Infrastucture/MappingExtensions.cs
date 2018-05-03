using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Storage.Infrastucture
{
    public static class MappingExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllOtherMembers(opt => opt.Ignore());
            return expression;
        }
    }
}
