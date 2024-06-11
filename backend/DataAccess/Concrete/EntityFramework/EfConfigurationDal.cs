using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfConfigurationDal : IConfigurationDal
    {

        //public Dictionary<string, (object Value, Type Type)> GetConfigurations(string applicationName)
        //{
        //    using (var context = new ConfigurationContext())
        //    {
        //        var configurations = new Dictionary<string, (object, Type)>();
        //        var configs = context.t_Configurations
        //                              .Where(c => c.ApplicationName == applicationName && c.IsActive)
        //                              .ToList();

        //        foreach (var config in configs)
        //        {
        //            var type = Type.GetType(config.Type);
        //            var value = Convert.ChangeType(config.Value, type);
        //            configurations[config.Name] = (value, type);
        //        }

        //        return configurations;
        //    }
        //}
        public List<t_Configurations> GetAllConfigurations()
        {
            using (var context = new ConfigurationContext())
            {
                var configs = context.t_Configurations.Select(p => new t_Configurations()
                {
                    Id = p.Id,
                    Name = p.Name,
                    ApplicationName = p.ApplicationName,
                    IsActive = p.IsActive, 
                    Type = p.Type,
                    Value = p.Value
                });

                return configs.ToList();
            }
        }


        public T GetValue<T>(string key)
        {
            using (var context = new ConfigurationContext())
            {
                var config = context.t_Configurations.FirstOrDefault(c => c.Name == key);
                if (config != null)
                {
                    return (T)Convert.ChangeType(config.Value, typeof(T));
                }
                else
                {
                    throw new KeyNotFoundException("Configuration key not found.");
                }
            }
        }

  

        public void AddConfiguration(t_Configurations config)
        {
            using (var context = new ConfigurationContext())
            {
                context.t_Configurations.Add(config);
                context.SaveChanges();
            }
        }

        public void UpdateConfiguration(t_Configurations config)
        {
            using (var context = new ConfigurationContext())
            {
                context.t_Configurations.Update(config);
                context.SaveChanges();
            }
        }

        public void DeleteConfiguration(int id)
        {
            using (var context = new ConfigurationContext())
            {
                var config = context.t_Configurations.FirstOrDefault(c => c.Id == id);
                if (config != null)
                {
                    context.t_Configurations.Remove(config);
                    context.SaveChanges();
                }
            }
        }
    }
}
