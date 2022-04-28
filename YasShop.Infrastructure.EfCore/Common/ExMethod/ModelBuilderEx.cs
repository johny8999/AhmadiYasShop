using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YasShop.Infrastructure.EfCore.Contracts;

namespace YasShop.Infrastructure.EfCore.Common.ExMethod
{
    public static class ModelBuilderEx
    {
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo ApplyGenericMethod = typeof(ModelBuilder).GetMethods().First(a => a.Name == nameof(ModelBuilder.ApplyConfiguration));
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes()).Where(a => a.IsClass && a.IsPublic);

            foreach (var type in types)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo ApplyCreateMethod = ApplyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
                        ApplyCreateMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }

        public static void RegisterAllEntities<Basetype>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(a => a.IsClass && a.IsPublic && typeof(Basetype).IsAssignableFrom(a));


            foreach (var type in types)
            {
                modelBuilder.Entity(type);
            }
        }

    }
}
