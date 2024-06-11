using DataAccess.Concrete.EntityFramework;
using Business.Abstract;
using Entity.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ConfigurationAppManager : IConfigurationAppService
    {
        IConfigurationDal _service;
        System.Timers.Timer _refreshTimer;

        public ConfigurationAppManager(string applicationName, IConfigurationDal service, double refreshTimerIntervalInMs)
        {
           
            _service = service;

            _refreshTimer = new System.Timers.Timer(refreshTimerIntervalInMs);
            _refreshTimer.Elapsed += (sender, e) => RefreshConfigurations(applicationName);
            _refreshTimer.Start();
            RefreshConfigurations(applicationName);
        }

        public void RefreshConfigurations(string applicationName)
        {
            //_service.GetConfigurations(applicationName);
        }
        public List<t_Configurations> GetAllConfigurations()
        {
            return _service.GetAllConfigurations();
        }
        public T GetValue<T>(string key)
        {
            return _service.GetValue<T>(key);
        }

        public void AddConfiguration(t_Configurations config)
        {
            _service.AddConfiguration(config);
        }

        public void UpdateConfiguration(t_Configurations config)
        {
            _service.UpdateConfiguration(config);
        }

        public void DeleteConfiguration(int id)
        {
            _service.DeleteConfiguration(id);
        }
    }
}
