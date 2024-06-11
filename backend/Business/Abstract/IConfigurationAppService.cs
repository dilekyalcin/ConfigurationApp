using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IConfigurationAppService
    {
        void RefreshConfigurations(string applicationName);
        T GetValue<T>(string key);
        void AddConfiguration(t_Configurations config);
        void UpdateConfiguration(t_Configurations config);
        void DeleteConfiguration(int id);
        List<t_Configurations> GetAllConfigurations();

    }
}
