using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IConfigurationDal 
    {
        //Dictionary<string, (object Value, Type Type)> GetConfigurations(string applicationName);
        T GetValue<T>(string key);
        void AddConfiguration(t_Configurations config);
        void UpdateConfiguration(t_Configurations config);
        void DeleteConfiguration(int id);
        public List<t_Configurations> GetAllConfigurations();
    }
}
